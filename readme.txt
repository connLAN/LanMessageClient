mueddib@openldap.org

who are you , thank you.
----------------------------------------------------

root@linux.com

--------------------------------
whoami

-----

1.0.8.8
=======

Added panel that displays remote user's actions.
Improved the way status messages are displayed in conversation area.
Added option to turn emoticons on/off in chat window.
Added option to show as Away when computer is idle for a set time.
Additional user status and status related options added.
Bug fixes.

1.0.8.7
=======
Added support for more theme customizations. New "Office" theme added.
Added more options in message and history settings.
Added new screen for sending broadcast messages. Recipients can now be selected.
Updated file transfer dialog to show more details while transferring files.
File transfer dialog shows true icons for files in the list.
Minor improvements to UI.
Bug fixes.

1.0.8.6
=======
Added a presence area in the main window for better user interaction.
Updated UI layout and features.
Reset version numbering for reflecting development progress more accurately.
Bug fixes.

1.0.8.5
=======
Fixed keyboard focus working incorrectly.
Detects change in network connectivity and behaves appropriately.
Detects if local IP address has changed while application is running and notifies correctly.

1.0.8.4
=======
Fixed unnecessary status messages popping up in conversation.
Improved formatting of chat history in history box.
Fixed application crashing when using system theme on Windows XP.
Minor improvements to UI.

1.0.8.3
=======
Improved and more responsive control validation.
Added support for additional command line switches.
Verifies that all modules are present before loading. Improves stability.
Revamped installer with more functions.
Improvements to UI.

1.0.8.2
=======
History is now saved in an encrypted binary database to protect privacy.
Old history will automatically be converted to new format when application first runs.
Added version support for configuration file.

1.0.8.1
=======
Ironed out minor UI glitches.

1.0.8.0
=======
Changed the application settings subsystem. Now the settings file is located in a friendly path and only one instance will be present. No more junk files left when application is uninstalled/upgraded.

1.0.7.9
=======
Added Offline status. Users can select this if they do not want to be visible to others.

1.0.7.8
=======
Message box now supports emoticons. Users can select an emoticon from the emoticon menu and the image will be inserted in the message box. This avoids having to define emoticon text for all emoticons.
Emoticon menu updated for accomodating more emoticons and different image sizes.
Application now supports unicode messages, so users can message in any language supported by unicode.
Added a new set of cool emoticons.
Due to the changes in message structure, this version is not backward compatible.

1.0.7.7
=======
Minor UI tweaks and funcions in various windows.
Fixed corruption of sounds loaded from resource.

1.0.7.6
=======
Overhauled the emoticon system. Now emoticons can be added or removed by editing the resource file. No code change needed.
Workaround for sound corruption when played asynchronously from resource.

1.0.7.5
=======
Completely reworked the theme system. Users can now create their own themes and use it in the application. Theme files are xml files which should be present in the Themes folder in application data folder.
Disabling themes has the same effect as selecting "System" theme in earlier versions.

1.0.7.4
=======
Compartmentalized code and resources for smaller executable file size. All resources (pictures, sounds etc.) now included inside the resources.dll bundled with the application.

1.0.7.3
=======
Application now uses AES encryption to send messages. 256 bit key is used. File transfer still uses plain format.

1.0.7.2
=======
Fixed the datagram drop issue. Application will now retry if a message is dropped.
Added options in the settings window to customize retry behavior.
Tweaked the way application behaves when user changes the tab/window option.
Application continues to run in the background when user info dialog is displayed.

1.0.7.1
=======
This is a hotfix containing workaround for incorrect reporting of user status when datagrams are dropped. Detection of a user who has timed out is disabled in this release.

1.0.7.0
=======
Conversations can now be opened in windows or tabs based on user preference. Opened conversations can be switched back and forth between tab and window.
Program plays sounds when certain events occur.
Alerts and sounds options added to settings window.
Window flashing now works properly.
Bug fixes.

1.0.6.8
=======
Fixed quirk with expand/collapse of groups in user list.
Fixed incorrect menu selection of status when program starts.

1.0.6.7
=======
Fixed incorrect grouping of messages in history viewer when sorted by time.
Code reliabilty improvements.
User status is saved.

1.0.6.6
=======
Added broadcast messaging feature. A user can send a network wide broadcast which will be received by all online users.
Files can be sent by dragging and dropping them into the chat window.
Some helpful information in the options window.

1.0.6.5
=======
Bug fixes.
Added option to turn system tray notifications on or off.

1.0.6.4
=======
Users can now view information about a remote user. This feature is invoked from the context menu of the user list.

1.0.6.3
=======
Finally added the much awaited Groups feature in the user list on left pane.
Users can organize contacts into different groups. Group settings are persisted.
Minor tweaks to UI in various windows.

1.0.6.2
=======
Some more improvements to the functionality and UI of file transfer window.

1.0.6.1
=======
Improvements to the functionality and UI of file transfer window.

1.0.6.0
=======
Added file transfer feature. Users can now send or receive files over TCP.
Options for file transfer added to the settings dialog.

1.0.5.9
=======
Added extensibility for cross version compatibilty. Future updates to message protocols will be backward compatible. However this version is not backward compatible.
Added status feature. Users can now set Available, Busy or Away status.
Application now checks for the remote user's client version.

1.0.5.8
=======
When incoming messages are sent to minimized window, window flashes in the taskbar to alert the user.
History Viewer correctly remembers its window state when restored.

1.0.5.7
=======
Tweaked the way users can navigate in history viewer.
Read only text boxes hide caret. This gives better visual cue.

1.0.5.6
=======
Added a history viewer where user can view the saved conversations.

1.0.5.5
=======
Added message logging. Logs saved in App_Data of user.

1.0.5.4
=======
Added support for visual themes.

1.0.5.3
=======
Minor validation logic fixes.

1.0.5.2
=======
Detects correctly if a different user is running the application in a multi user environment and behaves appropriatley.

1.0.5.1
=======
Implemented elegant event transmission between different instances of the application.
Now the application behaves intelligently when user attempts to run more than one instance.

1.0.5.0
=======
Added an Options dialog that can be used to set persistent settings.
Several application behaviors are now user configurable.

1.0.4.1
=======
Images updates for a more unified look.

1.0.4.0
=======
Minor clean up fixes.

1.0.3.9
=======
Added an About box which can be accessed from system tray menu.

1.0.3.8
=======
Fixed potential reliability and security vulnerabilities.

1.0.3.7
=======
Added a toolbar to the chat control for easier access to font and smiley functionality.
Toolbar also has a button that lets users save the chat history to a file.
Font selection dialog now available through the toolbar instead of menu which was obscure.

1.0.3.6
=======
Added a Silent Mode which can be enabled from the system tray menu. When it is enabled, ballon tips are not displayed and window does not auto popup for new incoming message.

1.0.3.5
=======
Minor changes in window behavior when hiding and showing. Avoids flicker now.

1.0.3.4
=======
Switched to Windows style menus.
Added new images and updated existing ones for menus and controls.

1.0.3.3
=======
Unfriendly error messages are no longer displayed.

1.0.3.2
=======
Chat history is retained even if the user closes the tab page. When tab page is reopened all previous history will be displayed. History is retained until user closes application.

1.0.3.1
=======
UI is updated asynchronously instead of at regular intervals. Application is now more robust.

1.0.3.0
=======
Completely redesigned the UI. UI now sports a colorful look and additional functionality.

1.0.2.7
=======
Improved the way messages are displayed in chat history box.

1.0.2.6
=======
Further code streamlining.

1.0.2.5
=======
Code clean up and streamlining.

1.0.2.4
=======
Additional command line switches supported.
Message time is also displayed in chat history box.
Text layout in chat history box slightly changed.

1.0.2.3
=======
Added support for debug mode through command line switch.

1.0.2.2
=======
Added specialized context menu for message box.
Users can now select font, attributes, color etc using a font dialog which is available through the context menu.

1.0.2.1
=======
Changed product name from LAN Chat to LAN Messenger.

1.0.2.0
=======
Added support for emoticons!

1.0.1.2
=======
Minor UI and RTF fixes.

1.0.1.1
=======
Added additional RTF support to chat history box. Usernames, special messages etc. are displayed with preset font sizes instead of default size.

1.0.1.0
=======
Added fully functional RTF support to chat history box. Users can select their own font, color and size and they will be displayed correctly on both sides.
Message box no longer has rich text support. Instead it will have a fixed font, color and size.

1.0.0.4
=======
Added additional RTF support to chat history box. Usernames, special messages etc. are displayed with preset fonts instead of default font.

1.0.0.3
=======
Changed control fonts to match Windows system font.

1.0.0.2
=======
Added partial RTF support to chat history box. Usernames, special messages etc. are displayed with preset colors instead of default color.

1.0.0.1
=======
Changed the behavior of Enabled property of ChatControl. It is no longer greyed out when disabled.

1.0.0.0
=======
Initial release version.
