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


#include <QDesktopWidget>
#include "historywindow.h"

lmcHistoryWindow::lmcHistoryWindow(QWidget *parent, Qt::WFlags flags) : QWidget(parent, flags) {
	ui.setupUi(this);
	QList<int> sizes;
	sizes.append(width() * 0.35);
	sizes.append(width() - width() * 0.35 - ui.splitter->handleWidth());
	ui.splitter->setSizes(sizes);
	QRect scr = QApplication::desktop()->screenGeometry();
	move(scr.center() - rect().center());

	connect(ui.tvMsgList, SIGNAL(currentItemChanged(QTreeWidgetItem*, QTreeWidgetItem*)),
		this, SLOT(tvMsgList_currentItemChanged(QTreeWidgetItem*, QTreeWidgetItem*)));
	connect(ui.btnClearHistory, SIGNAL(clicked()), this, SLOT(btnClearHistory_clicked()));
}

lmcHistoryWindow::~lmcHistoryWindow() {
}

void lmcHistoryWindow::init(void) {
	setWindowIcon(QIcon(IDR_APPICON));

	pSettings = new lmcSettings();
	restoreGeometry(pSettings->value(IDS_WINDOWHISTORY).toByteArray());
	ui.splitter->restoreState(pSettings->value(IDS_SPLITTERHISTORY).toByteArray());
	setUIText();

	displayList();
}

void lmcHistoryWindow::updateList(void) {
	displayList();
}

void lmcHistoryWindow::stop(void) {
	pSettings->setValue(IDS_WINDOWHISTORY, saveGeometry());
	pSettings->setValue(IDS_SPLITTERHISTORY, ui.splitter->saveState());
}

void lmcHistoryWindow::settingsChanged(void) {
}

void lmcHistoryWindow::changeEvent(QEvent* pEvent) {
	switch(pEvent->type()) {
	case QEvent::LanguageChange:
		setUIText();
		break;
    default:
        break;
	}

	QWidget::changeEvent(pEvent);
}

void lmcHistoryWindow::tvMsgList_currentItemChanged(QTreeWidgetItem* current, QTreeWidgetItem* previous) {
    Q_UNUSED(previous);

	if(current) {
		qint64 offset = current->data(0, DataRole).toLongLong();
		QString data = History::getMessage(offset);

		ui.txtMessageLog->setHtml(data);
	}
}

void lmcHistoryWindow::btnClearHistory_clicked(void) {
	QFile::remove(History::historyFile());
	displayList();
}

void lmcHistoryWindow::setUIText(void) {
	ui.retranslateUi(this);

	setWindowTitle(tr("Message History"));
}

void lmcHistoryWindow::displayList(void) {
	ui.txtMessageLog->clear();
	ui.tvMsgList->clear();
	msgList.clear();

	msgList = History::getList();

	for(int index = 0; index < msgList.count(); index++) {
		lmcHistoryTreeWidgetItem* pItem = new lmcHistoryTreeWidgetItem();
		pItem->setText(0, msgList[index].name);
		pItem->setText(1, msgList[index].date.toString(Qt::SystemLocaleDate));
		pItem->setData(0, DataRole, msgList[index].offset);
		pItem->setData(1, DataRole, msgList[index].date);
		pItem->setSizeHint(0, QSize(0, 20));
		ui.tvMsgList->addTopLevelItem(pItem);
	}

	ui.tvMsgList->sortByColumn(1, Qt::DescendingOrder);

	if(ui.tvMsgList->topLevelItemCount() > 0)
		ui.tvMsgList->setCurrentItem(ui.tvMsgList->topLevelItem(0));
}
