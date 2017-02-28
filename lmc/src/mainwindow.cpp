/****************************************************************************
**
** This file is part of LAN Messenger.
** 
** Copyright (c) 2010 - 2011 Dilip Radhakrishnan.
** 
** Contact:  dilipvrk@gmail.com
** 
** LAN Messenger is free software: you can redistribute it and/or modify
** it under the terms of the GNU General Public License as published by
** the Free Software Foundation, either version 3 of the License, or
** (at your option) any later version.
**
** LAN Messenger is distributed in the hope that it will be useful,
** but WITHOUT ANY WARRANTY; without even the implied warranty of
** MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
** GNU General Public License for more details.
**
** You should have received a copy of the GNU General Public License
** along with LAN Messenger.  If not, see <http://www.gnu.org/licenses/>.
**
****************************************************************************/


#include <QDesktopServices>
#include <QTimer>
#include <QUrl>
#include "mainwindow.h"

lmcMainWindow::lmcMainWindow(QWidget *parent, Qt::WFlags flags) : QWidget(parent, flags) {
	ui.setupUi(this);

	connect(ui.tvUserList, SIGNAL(itemActivated(QTreeWidgetItem*, int)), 
		this, SLOT(tvUserList_itemActivated(QTreeWidgetItem*, int)));
    connect(ui.tvUserList, SIGNAL(itemContextMenu(QTreeWidgetItem*, QPoint&)),
        this, SLOT(tvUserList_itemContextMenu(QTreeWidgetItem*, QPoint&)));
	connect(ui.tvUserList, SIGNAL(itemDragDropped(QTreeWidgetItem*)),
		this, SLOT(tvUserList_itemDragDropped(QTreeWidgetItem*)));
	connect(ui.tvUserList, SIGNAL(currentItemChanged(QTreeWidgetItem*,QTreeWidgetItem*)),
		this, SLOT(tvUserList_currentItemChanged(QTreeWidgetItem*,QTreeWidgetItem*)));
}

lmcMainWindow::~lmcMainWindow(void) {
}

void lmcMainWindow::init(User* pLocalUser, QList<Group>* pGroupList, bool connected) {
	setWindowIcon(QIcon(IDR_APPICON));

	this->pLocalUser = pLocalUser;

	createMainMenu();
	createToolBar();
	createStatusMenu();
	createAvatarMenu();

	createTrayMenu();
	createTrayIcon();
	connectionStateChanged(connected);

	createGroupMenu();
	createUserMenu();

	ui.lblDividerTop->setBackgroundRole(QPalette::Highlight);
	ui.lblDividerTop->setAutoFillBackground(true);

	ui.tvUserList->setIconSize(QSize(32, 32));
	ui.tvUserList->header()->setMovable(false);
	ui.tvUserList->header()->setStretchLastSection(false);
	ui.tvUserList->header()->setResizeMode(0, QHeaderView::Stretch);
	ui.tvUserList->header()->setResizeMode(1, QHeaderView::Fixed);
	ui.tvUserList->header()->resizeSection(1, 38);
	ui.btnStatus->setIconSize(QSize(20, 20));
	int index = Helper::statusIndexFromCode(pLocalUser->status);
	//	if status is not recognized, default to available
	index = qMax(index, 0);
	ui.btnStatus->setIcon(QIcon(QPixmap(statusPic[index], "PNG")));
	statusGroup->actions()[index]->setChecked(true);
	ui.lblStatus->setText(statusGroup->checkedAction()->text());
	nAvatar = pLocalUser->avatar;

	pSoundPlayer = new lmcSoundPlayer();
	pSettings = new lmcSettings();
	restoreGeometry(pSettings->value(IDS_WINDOWMAIN).toByteArray());
	//	get saved settings
	settingsChanged(true);
	setUIText();

	initGroups(pGroupList);
}

void lmcMainWindow::start() {
	//	if no avatar is set, select a random avatar (useful when running for the first time)
	if(nAvatar > AVT_COUNT) {
		qsrand((uint)QTime::currentTime().msec());
		nAvatar = qrand() % AVT_COUNT;
	}
	// This method should only be called from here, otherwise an MT_Notify message is sent
	// and the program will connect to the network before start() is called.
	setAvatar();
	pTrayIcon->setVisible(showSysTray);
	if(pSettings->value(IDS_AUTOSHOW, IDS_AUTOSHOW_VAL).toBool())
		show();
}

void lmcMainWindow::restore(void) {
	//	if window is minimized it, restore it to previous state
	if(windowState().testFlag(Qt::WindowMinimized))
		setWindowState(windowState() & ~Qt::WindowMinimized);
	setWindowState(windowState() | Qt::WindowActive);
	raise();	// make main window the top most window of the application
	show();
	activateWindow();	// bring window to foreground
}

void lmcMainWindow::stop(void) {
	pSettings->setValue(IDS_WINDOWMAIN, saveGeometry());
	pSettings->setValue(IDS_MINIMIZEMSG, showMinimizeMsg);

	pSettings->beginWriteArray(IDS_GROUPEXPHDR);
	for(int index = 0; index < ui.tvUserList->topLevelItemCount(); index++) {
		pSettings->setArrayIndex(index);
		pSettings->setValue(IDS_GROUP, ui.tvUserList->topLevelItem(index)->isExpanded());
	}
	pSettings->endArray();

	pTrayIcon->hide();

	//	delete all temp files from cache
	QDir cacheDir(StdLocation::cacheDir());
	if(!cacheDir.exists())
		return;
	QString filter = "*.tmp";
	QDir::Filters filters = QDir::Files | QDir::Readable;
	QDir::SortFlags sort = QDir::Name;
	QStringList fileNames = cacheDir.entryList(QStringList() << filter, filters, sort);
	foreach (QString fileName, fileNames) {
		QString filePath = cacheDir.absoluteFilePath(fileName);
		QFile::remove(filePath);
	}
}

void lmcMainWindow::addUser(User* pUser) {
	if(!pUser)
		return;

	int index = Helper::statusIndexFromCode(pUser->status);

	lmcUserTreeWidgetUserItem *pItem = new lmcUserTreeWidgetUserItem();
	pItem->setData(0, IdRole, pUser->id);
	pItem->setData(0, TypeRole, "User");
	pItem->setData(0, StatusRole, index);
	pItem->setText(0, pUser->name);
	pItem->setSizeHint(0, QSize(0, 36));
	
	if(index != -1)
		pItem->setIcon(0, QIcon(QPixmap(statusPic[index], "PNG")));

	QTreeWidgetItem* pGroupItem = getGroupItem(&pUser->group);
	pGroupItem->addChild(pItem);
	pGroupItem->sortChildren(0, Qt::AscendingOrder);

	// this should be called after item has been added to tree
	setUserAvatar(&pUser->id);

	if(isHidden() || !isActiveWindow()) {
		QString msg = tr("%1 is online.");
		showTrayMessage(TM_Status, msg.arg(pItem->text(0)));
		pSoundPlayer->play(SE_UserOnline);
	}

	sendAvatar(&pUser->id);
}

void lmcMainWindow::updateUser(User* pUser) {
	if(!pUser)
		return;

	QTreeWidgetItem* pItem = getUserItem(&pUser->id);
	if(pItem) {
		updateStatusImage(pItem, &pUser->status);
		pItem->setData(0, StatusRole, Helper::statusIndexFromCode(pUser->status));
		pItem->setText(0, pUser->name);
		QTreeWidgetItem* pGroupItem = pItem->parent();
		pGroupItem->sortChildren(0, Qt::AscendingOrder);
	}
}

void lmcMainWindow::removeUser(QString* lpszUserId) {
	QTreeWidgetItem* pItem = getUserItem(lpszUserId);
	if(!pItem)
		return;
		
	QTreeWidgetItem* pGroup = pItem->parent();
	pGroup->removeChild(pItem);

	if(isHidden() || !isActiveWindow()) {
		QString msg = tr("%1 is offline.");
		showTrayMessage(TM_Status, msg.arg(pItem->text(0)));
		pSoundPlayer->play(SE_UserOffline);
	}
}

void lmcMainWindow::receiveMessage(MessageType type, QString* lpszUserId, XmlMessage* pMessage) {
	QDir cacheDir;
	QString fileName;
	QString oldName;
	QString tempName;
	QString filePath;
	QString oldPath;
	QString tempPath;
	XmlMessage reply;
	int fileOp;
	int fileMode;

	switch(type) {
	case MT_Avatar:
		fileOp = Helper::indexOf(FileOpNames, FO_Max, pMessage->data(XN_FILEOP));
		fileMode = Helper::indexOf(FileModeNames, FM_Max, pMessage->data(XN_MODE));
		if(fileOp == FO_Request) {
			cacheDir = QDir(StdLocation::cacheDir());
			fileName = "avt_" + * lpszUserId + "_part.png";
			filePath = cacheDir.absoluteFilePath(fileName);
			reply.addData(XN_MODE, FileModeNames[FM_Receive]);
			reply.addData(XN_FILETYPE, FileTypeNames[FT_Avatar]);
			reply.addData(XN_FILEOP, FileOpNames[FO_Accept]);
			reply.addData(XN_FILEID, pMessage->data(XN_FILEID));
			reply.addData(XN_FILEPATH, filePath);
			reply.addData(XN_FILENAME, fileName);
			reply.addData(XN_FILESIZE, pMessage->data(XN_FILESIZE));
			sendMessage(MT_Avatar, lpszUserId, &reply);
		} else if(fileOp == FO_Complete && fileMode == FM_Receive) {
			cacheDir = QDir(StdLocation::cacheDir());
			fileName = "avt_" + *lpszUserId + ".png";
			filePath = cacheDir.absoluteFilePath(fileName);
			QFile::remove(filePath);
			oldName = "avt_" + * lpszUserId + "_part.png";
			oldPath = cacheDir.absoluteFilePath(oldName);
			QFile::rename(oldPath, filePath);
			setUserAvatar(lpszUserId);
			tempName = Helper::getUuid() + ".tmp";
			tempPath = cacheDir.absoluteFilePath(tempName);
			QFile::copy(filePath, tempPath);
			sendMessage(MT_LocalAvatar, lpszUserId, &tempPath);
		}
		break;
	default:
		break;
	}
}

void lmcMainWindow::connectionStateChanged(bool connected) {
	if(connected) {
		pTrayIcon->setToolTip(lmcStrings::appName());
		showTrayMessage(TM_Connection, tr("You are online."));
	} else {
		QString msg = tr("%1 - Not Connected");
		pTrayIcon->setToolTip(msg.arg(lmcStrings::appName()));
		showTrayMessage(TM_Connection, tr("You are no longer connected."), lmcStrings::appName(), TMI_Warning);
	}
	bConnected = connected;
}

void lmcMainWindow::settingsChanged(bool init) {
	showSysTray = pSettings->value(IDS_SYSTRAY, IDS_SYSTRAY_VAL).toBool();
	showSysTrayMsg = pSettings->value(IDS_SYSTRAYMSG, IDS_SYSTRAYMSG_VAL).toBool();
	//	this setting should be loaded only at window init
	if(init)
		showMinimizeMsg = pSettings->value(IDS_MINIMIZEMSG, IDS_MINIMIZEMSG_VAL).toBool();
	//	this operation should not be done when window inits
	if(!init)
		pTrayIcon->setVisible(showSysTray);
	minimizeHide = pSettings->value(IDS_MINIMIZETRAY, IDS_MINIMIZETRAY_VAL).toBool();
	singleClickActivation = pSettings->value(IDS_SINGLECLICKTRAY, IDS_SINGLECLICKTRAY_VAL).toBool();
	showAlert = pSettings->value(IDS_ALERT, IDS_ALERT_VAL).toBool();
	noBusyAlert = pSettings->value(IDS_NOBUSYALERT, IDS_NOBUSYALERT_VAL).toBool();
	noDNDAlert = pSettings->value(IDS_NODNDALERT, IDS_NODNDALERT_VAL).toBool();
	pSoundPlayer->settingsChanged();
	ui.lblUserName->setText(pLocalUser->name);	// in case display name has been changed
}

void lmcMainWindow::showTrayMessage(TrayMessageType type, QString szMessage, QString szTitle, TrayMessageIcon icon) {
	if(!showSysTray || !showSysTrayMsg)
		return;

	bool showMsg = showSysTray;
	
	switch(type) {
	case TM_Status:
		if(!showAlert || (pLocalUser->status == "Busy" && noBusyAlert) || (pLocalUser->status == "NoDisturb" && noDNDAlert))
			return;
		break;
    default:
        break;
	}

	if(szTitle.isNull())
		szTitle = lmcStrings::appName();

	QSystemTrayIcon::MessageIcon trayIcon = QSystemTrayIcon::Information;
	switch(icon) {
	case TMI_Info:
		trayIcon = QSystemTrayIcon::Information;
		break;
	case TMI_Warning:
		trayIcon = QSystemTrayIcon::Warning;
		break;
	case TMI_Error:
		trayIcon = QSystemTrayIcon::Critical;
		break;
    default:
        break;
	}

	if(showMsg) {
		lastTrayMessageType = type;
		pTrayIcon->showMessage(szTitle, szMessage, trayIcon);
	}
}

QList<QTreeWidgetItem*> lmcMainWindow::getContactsList() {
	QList<QTreeWidgetItem*> contactsList;
	for(int index = 0; index < ui.tvUserList->topLevelItemCount(); index++)
		contactsList.append(ui.tvUserList->topLevelItem(index)->clone());

	return contactsList;
}

void lmcMainWindow::closeEvent(QCloseEvent* pEvent) {
	//	close main window to system tray
	pEvent->ignore();
	hide();
	showMinimizeMessage();
}

void lmcMainWindow::changeEvent(QEvent* pEvent) {
	switch(pEvent->type()) {
	case QEvent::WindowStateChange:
		if(minimizeHide) {
			QWindowStateChangeEvent* e = (QWindowStateChangeEvent*)pEvent;
			if(isMinimized() && e->oldState() != Qt::WindowMinimized) {
				QTimer::singleShot(0, this, SLOT(hide()));
				pEvent->ignore();
				showMinimizeMessage();
			}
		}
		break;
	case QEvent::LanguageChange:
		setUIText();
		break;
    default:
        break;
	}

	QWidget::changeEvent(pEvent);
}

void lmcMainWindow::sendMessage(MessageType type, QString* lpszUserId, XmlMessage* pMessage) {
	emit messageSent(type, lpszUserId, pMessage);
}

void lmcMainWindow::trayShowAction_triggered(void) {
	restore();
}

void lmcMainWindow::trayHistoryAction_triggered(void) {
	emit showHistory();
}

void lmcMainWindow::trayFileAction_triggered(void) {
	emit showTransfers();
}

void lmcMainWindow::traySettingsAction_triggered(void) {
	emit showSettings();
}

void lmcMainWindow::trayAboutAction_triggered(void) {
	emit showAbout();
}

void lmcMainWindow::trayExitAction_triggered(void) {
	emit appExiting();
}

void lmcMainWindow::statusAction_triggered(QAction* action) {
	QString status = action->data().toString();
	int index = Helper::statusIndexFromCode(status);
	if(index != -1) {
		ui.btnStatus->setIcon(QIcon(QPixmap(statusPic[index], "PNG")));
		ui.lblStatus->setText(statusGroup->checkedAction()->text());
		pLocalUser->status = statusCode[index];
		pSettings->setValue(IDS_STATUS, pLocalUser->status);

		sendMessage(MT_Status, NULL, &status);
	}
}

void lmcMainWindow::avatarAction_triggered(void) {
	setAvatar();
}

void lmcMainWindow::avatarBrowseAction_triggered(void) {
	QString dir = pSettings->value(IDS_OPENPATH, IDS_OPENPATH_VAL).toString();
	QString fileName = QFileDialog::getOpenFileName(this, tr("Select avatar picture"), dir,
		"Images (*.bmp *.gif *.jpg *.jpeg *.png *.tif *.tiff)");
	if(!fileName.isEmpty()) {
		pSettings->setValue(IDS_OPENPATH, QFileInfo(fileName).dir().absolutePath());
		setAvatar(fileName);
	}
}

void lmcMainWindow::chatRoomAction_triggered(void) {
	// Generate a thread id which is a combination of the local user id and a guid
	// This ensures that no other user will generate an identical thread id
	QString threadId = Helper::getUuid();
	threadId.prepend(pLocalUser->id);
	emit chatRoomStarting(&threadId);
}

void lmcMainWindow::publicChatAction_triggered(void) {
	emit showPublicChat();
}

void lmcMainWindow::refreshAction_triggered(void) {
    QString szUserId;
    QString szMessage;

	sendMessage(MT_Refresh, &szUserId, &szMessage);
}

void lmcMainWindow::helpAction_triggered(void) {
	QRect rect = geometry();
	emit showHelp(&rect);
}

void lmcMainWindow::homePageAction_triggered(void) {
	QDesktopServices::openUrl(QUrl(IDA_DOMAIN));
}

void lmcMainWindow::updateAction_triggered(void) {
	QRect rect = geometry();
	emit showUpdate(&rect);
}

void lmcMainWindow::trayIcon_activated(QSystemTrayIcon::ActivationReason reason) {
	switch(reason) {
	case QSystemTrayIcon::Trigger:
		if(singleClickActivation)
			trayShowAction_triggered();
		break;
	case QSystemTrayIcon::DoubleClick:
		if(!singleClickActivation)
			trayShowAction_triggered();
		break;
    default:
        break;
	}
}

void lmcMainWindow::trayMessage_clicked(void) {
	switch(lastTrayMessageType) {
	case TM_Status:
		trayShowAction_triggered();
		break;
	case TM_Transfer:
		emit showTransfers();
		break;
    default:
        break;
	}
}

void lmcMainWindow::tvUserList_itemActivated(QTreeWidgetItem* pItem, int column) {
    Q_UNUSED(column);
    if(pItem->data(0, TypeRole).toString().compare("User") == 0) {
        QString szUserId = pItem->data(0, IdRole).toString();
        emit chatStarting(&szUserId);
    }
}

void lmcMainWindow::tvUserList_itemContextMenu(QTreeWidgetItem* pItem, QPoint& pos) {
    if(!pItem)
        return;

    if(pItem->data(0, TypeRole).toString().compare("Group") == 0) {
        for(int index = 0; index < pGroupMenu->actions().count(); index++)
            pGroupMenu->actions()[index]->setData(pItem->data(0, IdRole));

		bool defGroup = (pItem->data(0, IdRole).toString().compare(GRP_DEFAULT_ID) == 0);
        pGroupMenu->actions()[3]->setEnabled(!defGroup);
        pGroupMenu->exec(pos);
    } else if(pItem->data(0, TypeRole).toString().compare("User") == 0) {
        for(int index = 0; index < pUserMenu->actions().count(); index++)
            pUserMenu->actions()[index]->setData(pItem->data(0, IdRole));

        pUserMenu->exec(pos);
    }
}

void lmcMainWindow::tvUserList_itemDragDropped(QTreeWidgetItem* pItem) {
    if(dynamic_cast<lmcUserTreeWidgetUserItem*>(pItem)) {
        QString szUserId = pItem->data(0, IdRole).toString();
        QString szMessage = pItem->parent()->data(0, IdRole).toString();
        sendMessage(MT_Group, &szUserId, &szMessage);
		QTreeWidgetItem* pGroupItem = pItem->parent();
		pGroupItem->sortChildren(0, Qt::AscendingOrder);
    }
	else if(dynamic_cast<lmcUserTreeWidgetGroupItem*>(pItem)) {
		int index = ui.tvUserList->indexOfTopLevelItem(pItem);
		QString groupId = pItem->data(0, IdRole).toString();
		emit groupUpdated(GO_Move, groupId, index);
	}
}

void lmcMainWindow::tvUserList_currentItemChanged(QTreeWidgetItem *pCurrent, QTreeWidgetItem *pPrevious) {
	Q_UNUSED(pPrevious);
	bool bEnabled = (pCurrent && pCurrent->data(0, TypeRole).toString().compare("User") == 0);
	toolChatAction->setEnabled(bEnabled);
	toolFileAction->setEnabled(bEnabled);
}

void lmcMainWindow::groupAddAction_triggered(void) {
	QString groupName = QInputDialog::getText(this, tr("Add New Group"), tr("Enter a name for the group"));

	if(groupName.isNull())
		return;

	if(getGroupItemByName(&groupName)) {
		QString msg = tr("A group named '%1' already exists. Please enter a different name.");
		QMessageBox::warning(this, "", msg.arg(groupName));
		return;
	}

	//generate a group id that is not assigned to any existing group
	QString groupId;
	do {
		groupId = Helper::getUuid();
	} while(getGroupItem(&groupId));
	
	emit groupUpdated(GO_New, groupId, groupName);
	lmcUserTreeWidgetGroupItem *pItem = new lmcUserTreeWidgetGroupItem();
	pItem->setData(0, IdRole, groupId);
	pItem->setData(0, TypeRole, "Group");
	pItem->setText(0, groupName);
	pItem->setSizeHint(0, QSize(0, 20));
	ui.tvUserList->addTopLevelItem(pItem);
	//	set the item as expanded after adding it to the treeview, else wont work
	pItem->setExpanded(true);
}

void lmcMainWindow::groupRenameAction_triggered(void) {
	QTreeWidgetItem* pGroupItem = ui.tvUserList->currentItem();
	QString groupId = pGroupItem->data(0, IdRole).toString();
	QString oldName = pGroupItem->data(0, Qt::DisplayRole).toString();
	QString newName = QInputDialog::getText(this, tr("Rename Group"), 
		tr("Enter a new name for the group"), QLineEdit::Normal, oldName);

	if(newName.isNull() || newName.compare(oldName) == 0)
		return;

	if(getGroupItemByName(&newName)) {
		QString msg = tr("A group named '%1' already exists. Please enter a different name.");
		QMessageBox::warning(this, "", msg.arg(newName));
		return;
	}

	emit groupUpdated(GO_Rename, groupId, newName);
	pGroupItem->setText(0, newName);
}

void lmcMainWindow::groupDeleteAction_triggered(void) {
	QTreeWidgetItem* pGroupItem = ui.tvUserList->currentItem();
	QString groupId = pGroupItem->data(0, IdRole).toString();
	QString defGroupId = GRP_DEFAULT_ID;
	QTreeWidgetItem* pDefGroupItem = getGroupItem(&defGroupId);
	while(pGroupItem->childCount()) {
		QTreeWidgetItem* pUserItem = pGroupItem->child(0);
		pGroupItem->removeChild(pUserItem);
		pDefGroupItem->addChild(pUserItem);
        QString szUserId = pUserItem->data(0, IdRole).toString();
        QString szMessage = pUserItem->parent()->data(0, IdRole).toString();
        sendMessage(MT_Group, &szUserId, &szMessage);
	}
	pDefGroupItem->sortChildren(0, Qt::AscendingOrder);

	emit groupUpdated(GO_Delete, groupId, QVariant());
	ui.tvUserList->takeTopLevelItem(ui.tvUserList->indexOfTopLevelItem(pGroupItem));
}

void lmcMainWindow::userConversationAction_triggered(void) {
	QString userId = ui.tvUserList->currentItem()->data(0, IdRole).toString();
	emit chatStarting(&userId);
}

void lmcMainWindow::userBroadcastAction_triggered(void) {
	emit showBroadcast();
}

void lmcMainWindow::userFileAction_triggered(void) {
	QString userId = ui.tvUserList->currentItem()->data(0, IdRole).toString();
	QString dir = pSettings->value(IDS_OPENPATH, IDS_OPENPATH_VAL).toString();
	QString fileName = QFileDialog::getOpenFileName(this, QString(), dir);
	if(!fileName.isEmpty()) {
		pSettings->setValue(IDS_OPENPATH, QFileInfo(fileName).dir().absolutePath());
		sendMessage(MT_LocalFile, &userId, &fileName);
	}
}

void lmcMainWindow::userInfoAction_triggered(void) {
	QString userId = ui.tvUserList->currentItem()->data(0, IdRole).toString();
	QString message;
	sendMessage(MT_Query, &userId, &message);
}

void lmcMainWindow::createMainMenu(void) {
	pMainMenu = new QMenuBar(this);
	pFileMenu = pMainMenu->addMenu("&Messenger");
	chatRoomAction = pFileMenu->addAction("&New Chat Room", this,
		SLOT(chatRoomAction_triggered()), QKeySequence::New);
	publicChatAction = pFileMenu->addAction(QIcon(QPixmap(IDR_CHATROOM, "PNG")), "&Public Chat",
		this, SLOT(publicChatAction_triggered()));
	pFileMenu->addSeparator();
	refreshAction = pFileMenu->addAction(QIcon(QPixmap(IDR_REFRESH, "PNG")), "&Refresh contacts list", 
		this, SLOT(refreshAction_triggered()), QKeySequence::Refresh);
	pFileMenu->addSeparator();
	exitAction = pFileMenu->addAction(QIcon(QPixmap(IDR_CLOSE, "PNG")), "E&xit", 
		this, SLOT(trayExitAction_triggered()));
	pToolsMenu = pMainMenu->addMenu("&Tools");
	historyAction = pToolsMenu->addAction(QIcon(QPixmap(IDR_HISTORY, "PNG")), "&History", 
		this, SLOT(trayHistoryAction_triggered()), QKeySequence(Qt::CTRL + Qt::Key_H));
	transferAction = pToolsMenu->addAction(QIcon(QPixmap(IDR_TRANSFER, "PNG")), "File &Transfers", 
		this, SLOT(trayFileAction_triggered()), QKeySequence(Qt::CTRL + Qt::Key_J));
	pToolsMenu->addSeparator();
	settingsAction = pToolsMenu->addAction(QIcon(QPixmap(IDR_TOOLS, "PNG")), "&Preferences", 
		this, SLOT(traySettingsAction_triggered()), QKeySequence::Preferences);
	pHelpMenu = pMainMenu->addMenu("&Help");
	helpAction = pHelpMenu->addAction(QIcon(QPixmap(IDR_QUESTION, "PNG")), "&Help",
		this, SLOT(helpAction_triggered()), QKeySequence::HelpContents);
	pHelpMenu->addSeparator();
	QString text = "%1 &online";
	onlineAction = pHelpMenu->addAction(QIcon(QPixmap(IDR_WEB, "PNG")), text.arg(lmcStrings::appName()), 
		this, SLOT(homePageAction_triggered()));
	updateAction = pHelpMenu->addAction("Check for &Updates", this, SLOT(updateAction_triggered()));
	aboutAction = pHelpMenu->addAction(QIcon(QPixmap(IDR_INFO, "PNG")), "&About", this, SLOT(trayAboutAction_triggered()));

	layout()->setMenuBar(pMainMenu);
}

void lmcMainWindow::createTrayMenu(void) {
	pTrayMenu = new QMenu(this);
	
	QString text = "&Show %1";
	trayShowAction = pTrayMenu->addAction(QIcon(QPixmap(IDR_MESSENGER, "PNG")), text.arg(lmcStrings::appName()), 
		this, SLOT(trayShowAction_triggered()));
	pTrayMenu->addSeparator();
	trayStatusAction = pTrayMenu->addMenu(pStatusMenu);
	trayStatusAction->setText("&Change Status");
	pTrayMenu->addSeparator();
	trayHistoryAction = pTrayMenu->addAction(QIcon(QPixmap(IDR_HISTORY, "PNG")), "&History",
		this, SLOT(trayHistoryAction_triggered()));
	trayTransferAction = pTrayMenu->addAction(QIcon(QPixmap(IDR_TRANSFER, "PNG")), "File &Transfers",
		this, SLOT(trayFileAction_triggered()));
	pTrayMenu->addSeparator();
	traySettingsAction = pTrayMenu->addAction(QIcon(QPixmap(IDR_TOOLS, "PNG")), "&Preferences",
		this, SLOT(traySettingsAction_triggered()));
	trayAboutAction = pTrayMenu->addAction(QIcon(QPixmap(IDR_INFO, "PNG")), "&About",
		this, SLOT(trayAboutAction_triggered()));
	pTrayMenu->addSeparator();
	trayExitAction = pTrayMenu->addAction(QIcon(QPixmap(IDR_CLOSE, "PNG")), "E&xit", this, SLOT(trayExitAction_triggered()));

	pTrayMenu->setDefaultAction(trayShowAction);
}

void lmcMainWindow::createTrayIcon(void) {
	pTrayIcon = new QSystemTrayIcon(this);
	pTrayIcon->setIcon(QIcon(IDR_APPICON));
	pTrayIcon->setContextMenu(pTrayMenu);
	
	connect(pTrayIcon, SIGNAL(activated(QSystemTrayIcon::ActivationReason)), 
		this, SLOT(trayIcon_activated(QSystemTrayIcon::ActivationReason)));
	connect(pTrayIcon, SIGNAL(messageClicked()), this, SLOT(trayMessage_clicked()));
}

void lmcMainWindow::createStatusMenu(void) {
	pStatusMenu = new QMenu(this);
	statusGroup = new QActionGroup(this);
	connect(statusGroup, SIGNAL(triggered(QAction*)), this, SLOT(statusAction_triggered(QAction*)));

	for(int index = 0; index < ST_COUNT; index++) {
		QAction* pAction = new QAction(QIcon(QPixmap(statusPic[index], "PNG")), lmcStrings::statusDesc()[index], this);
		pAction->setData(statusCode[index]);
		pAction->setCheckable(true);
		statusGroup->addAction(pAction);
		pStatusMenu->addAction(pAction);
	}

	ui.btnStatus->setMenu(pStatusMenu);
}

void lmcMainWindow::createAvatarMenu(void) {
	pAvatarMenu = new QMenu(this);

	lmcImagePickerAction* pAction = new lmcImagePickerAction(this, avtPic, AVT_COUNT, 48, 4, &nAvatar);
	connect(pAction, SIGNAL(triggered()), this, SLOT(avatarAction_triggered()));
	pAvatarMenu->addAction(pAction);
	pAvatarMenu->addSeparator();
	avatarBrowseAction = pAvatarMenu->addAction("&Select picture...", this, SLOT(avatarBrowseAction_triggered()));

	ui.btnAvatar->setMenu(pAvatarMenu);
}

void lmcMainWindow::createGroupMenu(void) {
	pGroupMenu = new QMenu(this);

	groupAddAction = pGroupMenu->addAction("Add &New Group", this, SLOT(groupAddAction_triggered()));
	pGroupMenu->addSeparator();
	groupRenameAction = pGroupMenu->addAction("&Rename This Group", this, SLOT(groupRenameAction_triggered()));
	groupDeleteAction = pGroupMenu->addAction("&Delete This Group", this, SLOT(groupDeleteAction_triggered()));
}

void lmcMainWindow::createUserMenu(void) {
	pUserMenu = new QMenu(this);

	userChatAction = pUserMenu->addAction("&Conversation", this, SLOT(userConversationAction_triggered()));
	userFileAction = pUserMenu->addAction("Send &File", this, SLOT(userFileAction_triggered()));
	pUserMenu->addSeparator();
	userBroadcastAction = pUserMenu->addAction("Send &Broadcast Message", this, SLOT(userBroadcastAction_triggered()));
	pUserMenu->addSeparator();
	userInfoAction = pUserMenu->addAction("Get &Information", this, SLOT(userInfoAction_triggered()));
}

void lmcMainWindow::createToolBar(void) {
	QToolBar* pToolBar = new QToolBar(ui.wgtToolBar);
	pToolBar->setIconSize(QSize(40, 20));
	ui.toolBarLayout->addWidget(pToolBar);

	pToolBar->setToolButtonStyle(Qt::ToolButtonIconOnly);
	toolChatAction = pToolBar->addAction(QIcon(QPixmap(IDR_CHAT, "PNG")), "&Conversation",
		this, SLOT(userConversationAction_triggered()));
	toolChatAction->setEnabled(false);
	toolFileAction = pToolBar->addAction(QIcon(QPixmap(IDR_FILE, "PNG")), "Send &File",
		this, SLOT(userFileAction_triggered()));
	toolFileAction->setEnabled(false);
	pToolBar->addSeparator();
	toolBroadcastAction = pToolBar->addAction(QIcon(QPixmap(IDR_BROADCASTMSG, "PNG")), "Send &Broadcast Message",
		this, SLOT(userBroadcastAction_triggered()));
	pToolBar->addSeparator();
	toolPublicChatAction = pToolBar->addAction(QIcon(QPixmap(IDR_CHATROOM, "PNG")), "&Public Chat",
		this, SLOT(publicChatAction_triggered()));

	QToolButton* pButton = (QToolButton*)pToolBar->widgetForAction(toolChatAction);
	pButton->setAutoRaise(false);
	pButton = (QToolButton*)pToolBar->widgetForAction(toolFileAction);
	pButton->setAutoRaise(false);
	pButton = (QToolButton*)pToolBar->widgetForAction(toolBroadcastAction);
	pButton->setAutoRaise(false);
	pButton = (QToolButton*)pToolBar->widgetForAction(toolPublicChatAction);
	pButton->setAutoRaise(false);
}

void lmcMainWindow::setUIText(void) {
	ui.retranslateUi(this);

	setWindowTitle(lmcStrings::appName());

	pFileMenu->setTitle(tr("&Messenger"));
	chatRoomAction->setText(tr("&New Chat Room"));
	publicChatAction->setText(tr("&Public Chat"));
	refreshAction->setText(tr("&Refresh Contacts List"));
	exitAction->setText(tr("E&xit"));
	pToolsMenu->setTitle(tr("&Tools"));
	historyAction->setText(tr("&History"));
	transferAction->setText(tr("File &Transfers"));
	settingsAction->setText(tr("&Preferences"));
	pHelpMenu->setTitle(tr("&Help"));
	helpAction->setText(tr("&Help"));
	QString text = tr("%1 &online");
	onlineAction->setText(text.arg(lmcStrings::appName()));
	updateAction->setText(tr("Check for &Updates..."));
	aboutAction->setText(tr("&About"));
	text = tr("&Show %1");
	trayShowAction->setText(text.arg(lmcStrings::appName()));
	trayStatusAction->setText(tr("&Change Status"));
	trayHistoryAction->setText(tr("&History"));
	trayTransferAction->setText(tr("File &Transfers"));
	traySettingsAction->setText(tr("&Preferences"));
	trayAboutAction->setText(tr("&About"));
	trayExitAction->setText(tr("E&xit"));
	groupAddAction->setText(tr("Add &New Group"));
	groupRenameAction->setText(tr("&Rename This Group"));
	groupDeleteAction->setText(tr("&Delete This Group"));
	userChatAction->setText(tr("&Conversation"));
	userBroadcastAction->setText(tr("Send &Broadcast Message"));
	userFileAction->setText(tr("Send &File"));
	userInfoAction->setText(tr("Get &Information"));
	avatarBrowseAction->setText(tr("&Browse for more pictures..."));
	toolChatAction->setText(tr("&Conversation"));
	toolFileAction->setText(tr("Send &File"));
	toolBroadcastAction->setText(tr("Send &Broadcast Message"));
	toolPublicChatAction->setText(tr("&Public Chat"));

	for(int index = 0; index < statusGroup->actions().count(); index++)
		statusGroup->actions()[index]->setText(lmcStrings::statusDesc()[index]);
	
	ui.lblUserName->setText(pLocalUser->name);	// in case of retranslation
	if(statusGroup->checkedAction())
		ui.lblStatus->setText(statusGroup->checkedAction()->text());
}

void lmcMainWindow::showMinimizeMessage(void) {
	if(showMinimizeMsg) {
		QString msg = tr("%1 will continue to run in the background. Activate this icon to restore the application window.");
		showTrayMessage(TM_Minimize, msg.arg(lmcStrings::appName()));
		showMinimizeMsg = false;
	}
}

void lmcMainWindow::initGroups(QList<Group>* pGroupList) {
	for(int index = 0; index < pGroupList->count(); index++) {
		lmcUserTreeWidgetGroupItem *pItem = new lmcUserTreeWidgetGroupItem();
		pItem->setData(0, IdRole, pGroupList->value(index).id);
		pItem->setData(0, TypeRole, "Group");
		pItem->setText(0, pGroupList->value(index).name);
		pItem->setSizeHint(0, QSize(0, 20));
		ui.tvUserList->addTopLevelItem(pItem);
	}

	ui.tvUserList->expandAll();
	// size will be either number of items in group expansion list or number of top level items in
	// treeview control, whichever is less. This is to  eliminate arary out of bounds error.
	int size = qMin(pSettings->beginReadArray(IDS_GROUPEXPHDR), ui.tvUserList->topLevelItemCount());
	for(int index = 0; index < size; index++) {
		pSettings->setArrayIndex(index);
		ui.tvUserList->topLevelItem(index)->setExpanded(pSettings->value(IDS_GROUP).toBool());
	}
	pSettings->endArray();
}

void lmcMainWindow::updateStatusImage(QTreeWidgetItem* pItem, QString* lpszStatus) {
	int index = Helper::statusIndexFromCode(*lpszStatus);
	if(index != -1)
		pItem->setIcon(0, QIcon(QPixmap(statusPic[index], "PNG")));
}

void lmcMainWindow::setAvatar(QString fileName) {
	//	create cache folder if it does not exist
	QDir cacheDir(StdLocation::cacheDir());
	if(!cacheDir.exists())
		cacheDir.mkdir(cacheDir.absolutePath());
	QString filePath = cacheDir.absoluteFilePath("avt_local.png");

	//	Save the image as a file in the cache folder
	QPixmap avatar;
	if(!fileName.isEmpty()) {
		//	save a backup of the image in the cache folder
		avatar = QPixmap(fileName);
		avatar = avatar.scaled(QSize(AVT_WIDTH, AVT_HEIGHT), Qt::IgnoreAspectRatio, Qt::SmoothTransformation);
		avatar.save(filePath);
		nAvatar = -1;
	} else {
		//	nAvatar = -1 means custom avatar is set, otherwise load from resource
		if(nAvatar < 0) {
			//	load avatar from image file if file exists, else load default
			if(QFile::exists(filePath))
				avatar = QPixmap(filePath);
			else
				avatar = QPixmap(AVT_DEFAULT);
		} else
			avatar = QPixmap(avtPic[nAvatar]);
	}

	avatar = avatar.scaled(QSize(AVT_WIDTH, AVT_HEIGHT));
	fileName = "avt_" + pLocalUser->id + ".png";
	filePath = cacheDir.absoluteFilePath(fileName);
	avatar.save(filePath);

	ui.btnAvatar->setIcon(QIcon(QPixmap(filePath, "PNG")));
	pLocalUser->avatar = nAvatar;

	QString tempName;
	QString tempPath;
	tempName = Helper::getUuid() + ".tmp";
	tempPath = cacheDir.absoluteFilePath(tempName);
	QFile::copy(filePath, tempPath);

	sendMessage(MT_LocalAvatar, NULL, &tempPath);
	sendAvatar(NULL);
}

QTreeWidgetItem* lmcMainWindow::getUserItem(QString* lpszUserId) {
	for(int topIndex = 0; topIndex < ui.tvUserList->topLevelItemCount(); topIndex++) {
		for(int index = 0; index < ui.tvUserList->topLevelItem(topIndex)->childCount(); index++) {
			QTreeWidgetItem* pItem = ui.tvUserList->topLevelItem(topIndex)->child(index);
			if(pItem->data(0, IdRole).toString().compare(*lpszUserId) == 0)
				return pItem;
		}
	}

	return NULL;
}

QTreeWidgetItem* lmcMainWindow::getGroupItem(QString* lpszGroupId) {
	for(int topIndex = 0; topIndex < ui.tvUserList->topLevelItemCount(); topIndex++) {
		QTreeWidgetItem* pItem = ui.tvUserList->topLevelItem(topIndex);
		if(pItem->data(0, IdRole).toString().compare(*lpszGroupId) == 0)
			return pItem;
	}

	return NULL;
}

QTreeWidgetItem* lmcMainWindow::getGroupItemByName(QString* lpszGroupName) {
	for(int topIndex = 0; topIndex < ui.tvUserList->topLevelItemCount(); topIndex++) {
		QTreeWidgetItem* pItem = ui.tvUserList->topLevelItem(topIndex);
		if(pItem->data(0, Qt::DisplayRole).toString().compare(*lpszGroupName) == 0)
			return pItem;
	}

	return NULL;
}

void lmcMainWindow::sendMessage(MessageType type, QString* lpszUserId, QString* lpszMessage) {
	XmlMessage xmlMessage;
	
	switch(type) {
	case MT_Status:
		xmlMessage.addData(XN_STATUS, *lpszMessage);
		break;
	case MT_Refresh:
		break;
	case MT_Group:
		xmlMessage.addData(XN_GROUP, *lpszMessage);
		break;
	case MT_LocalFile:
		xmlMessage.addData(XN_MODE, FileModeNames[FM_Send]);
		xmlMessage.addData(XN_FILETYPE, FileTypeNames[FT_Normal]);
		xmlMessage.addData(XN_FILEOP, FileOpNames[FO_Request]);
		xmlMessage.addData(XN_FILEPATH, *lpszMessage);
		break;
	case MT_Query:
		xmlMessage.addData(XN_QUERYOP, QueryOpNames[QO_Get]);
		break;
	case MT_LocalAvatar:
		xmlMessage.addData(XN_FILEPATH, *lpszMessage);
		break;
	default:
		break;
	}

	sendMessage(type, lpszUserId, &xmlMessage);
}

void lmcMainWindow::sendAvatar(QString* lpszUserId) {
	QDir cacheDir(StdLocation::cacheDir());
	QString fileName = "avt_" + pLocalUser->id + ".png";
	QString filePath = cacheDir.absoluteFilePath(fileName);

	if(!QFile::exists(filePath))
		return;

	QFileInfo fileInfo = QFileInfo(filePath);
	XmlMessage xmlMessage;
	xmlMessage.addData(XN_MODE, FileModeNames[FM_Send]);
	xmlMessage.addData(XN_FILETYPE, FileTypeNames[FT_Avatar]);
	xmlMessage.addData(XN_FILEOP, FileOpNames[FO_Request]);
	xmlMessage.addData(XN_FILEPATH, fileInfo.filePath());
	xmlMessage.addData(XN_FILENAME, fileInfo.fileName());
	xmlMessage.addData(XN_FILESIZE, QString::number(fileInfo.size()));

	sendMessage(MT_Avatar, lpszUserId, &xmlMessage);
}

void lmcMainWindow::setUserAvatar(QString* lpszUserId) {
	QTreeWidgetItem* pUserItem = getUserItem(lpszUserId);
	if(!pUserItem)
		return;

	QDir cacheDir(StdLocation::cacheDir());
	QString fileName = "avt_" + *lpszUserId + ".png";
	QString filePath = cacheDir.absoluteFilePath(fileName);
	QPixmap avatar;
	if(!QFile::exists(filePath)) {
		avatar.load(AVT_DEFAULT);
		avatar = avatar.scaled(QSize(AVT_WIDTH, AVT_HEIGHT));
		avatar.save(filePath);
	}

	avatar.load(filePath);
	avatar = avatar.scaled(QSize(32, 32), Qt::IgnoreAspectRatio, Qt::SmoothTransformation);

	pUserItem->setIcon(1, QIcon(avatar));
}
