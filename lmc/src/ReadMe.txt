I have attached a zip file containing the translation files in .ts format which can be edited with Qt Linguist application.

The file fr_FR.ts is the translation file for French language. You can use it as an example for your translation. I have created a file for your language and included it in the zip archive. All you need to do is open it in Qt Linguist and enter the translated text.

In case there is some issue with using Qt Linguist, you could try other tools available online. I suggest Virtaal (http://translate.sourceforge.net). If all else fails, you could try opening the .ts file in a text editor and editing it since its just an XML file, although I would not recommend it.

Some strings will contain wild card patterns like %1, %2 etc. These are for cases when the complete string is not available at design time. For example, consider the string:

                    Sending 'MyPhoto.jpg' to Alice.

Here the filename (MyPhoto.jpg) and the user name (Alice) are only available at run time. So the string is coded like this:

                    Sending '%1' to %2.

The application will take care of replacing the wild cards with correct values at run time. When translating, the order and position of %1 and %2 may be different from the English string. It does not matter, all it matters is that %1 and %2 represent the same entities in both languages.

Similarly some strings will contain '&' character. This denotes the shortcut key for a control. For example, the File menu will have "&File" as its text. The letter F will be underlined in the UI. When it is translated to another language, a different appropriate letter may be used as the shortcut key.

If you have more questions, please check out http://doc.qt.nokia.com/latest/linguist-translators.html. They have explained all these much better than I did.

For right to left languages there is a special string in lmcCore module called "LAYOUT_DIRECTION" that must be translated as "RTL". This will ensure that the controls are laid out in the proper direction. For the other languages this can be left untranslated.

Note: Perhaps you already know all this, I just mentioned all this in case you did not.