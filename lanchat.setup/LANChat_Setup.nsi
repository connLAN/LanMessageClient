; LAN Messenger Installation Script
; Copyright (C) 2010 LAN Media


;------------------------------------------------------------------------------
;Includes

  !include MUI2.nsh
  !include nsDialogs.nsh

  
;------------------------------------------------------------------------------
;Constants
  
  !define ProductName "LAN Messenger"
  !define CompanyName "LAN Media"
  !define ProductVersion "1.0.8.8"
  !define ProductUrl "http://www.lanmedia.com"
  !define CompanyRegKey "SOFTWARE\${CompanyName}"
  !define AppRegKey "${CompanyRegKey}\${ProductName}"
  !define RunKey "SOFTWARE\Microsoft\Windows\CurrentVersion\Run"
  !define UninstKey "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${ProductName}"
  !define AppExec "lanmsngr.exe"
  !define Uninstaller "uninst.exe"
  !define CompanyDataDir "$LOCALAPPDATA\${CompanyName}"
  !define AppDataDir "${CompanyDataDir}\${ProductName}"
  !define StartMenuDir "$SMPROGRAMS\${ProductName}"
  !define DotNetUrl "http://download.microsoft.com/download/7/B/6/7B629E05-399A-4A92-B5BC-484C74B5124B/dotNetFx40_Client_setup.exe"


;------------------------------------------------------------------------------
;General

  ;Name and file
  Name "${ProductName}"
  BrandingText $BrandText
  OutFile "lanmsngr-${ProductVersion}.exe"

  ;Default installation folder
  InstallDir "$PROGRAMFILES\${ProductName}"

  ;Get installation folder from registry if available
  InstallDirRegKey HKLM "${AppRegKey}" "InstallDir"

  ;Request application privileges for Windows Vista and above
  RequestExecutionLevel admin

  ;Show details in the installation/uninstallation pages by default
  ShowInstDetails show
  ShowUninstDetails show
  
;------------------------------------------------------------------------------
;Interface Settings

  !define MUI_ICON "images\setup.ico"
  !define MUI_UNICON "images\setup.ico"
  !define MUI_HEADERIMAGE
  !define MUI_HEADERIMAGE_BITMAP "images\header-r.bmp"
  !define MUI_HEADERIMAGE_UNBITMAP "images\header-r.bmp"
  !define MUI_HEADERIMAGE_RIGHT
  !define MUI_WELCOMEFINISHPAGE_BITMAP "images\banner.bmp"
  !define MUI_UNWELCOMEFINISHPAGE_BITMAP "images\banner.bmp"
  !define MUI_ABORTWARNING


;------------------------------------------------------------------------------
;Pages

  !define MUI_WELCOMEPAGE_TITLE_3LINES
  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_LICENSE "eula\eula.rtf"
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  !define MUI_FINISHPAGE_TITLE_3LINES
  !define MUI_FINISHPAGE_RUN "$INSTDIR\${AppExec}"
  !define MUI_FINISHPAGE_RUN_TEXT "Start ${ProductName}"
  !define MUI_PAGE_CUSTOMFUNCTION_SHOW FinishPageShow
  !insertmacro MUI_PAGE_FINISH


  !define MUI_WELCOMEPAGE_TITLE_3LINES
  !define MUI_PAGE_CUSTOMFUNCTION_LEAVE un.WelcomePageLeave
  !insertmacro MUI_UNPAGE_WELCOME
  UninstPage custom un.OptionsPageShow un.OptionsPageLeave
  !insertmacro MUI_UNPAGE_INSTFILES
  !insertmacro MUI_UNPAGE_FINISH


;------------------------------------------------------------------------------
;Languages

  ;Define Uninstall confirmation constant for fetching strings from language file.
  !insertmacro MUI_SET MUI_UNCONFIRMPAGE ""
  !insertmacro MUI_LANGUAGE "English"


;------------------------------------------------------------------------------
;Version Information

  VIProductVersion "${ProductVersion}"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductName" "${ProductName}"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "CompanyName" "${CompanyName}"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalCopyright" "Copyright (c) 2010 ${CompanyName}"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "FileDescription" "${ProductName} Installer"
  VIAddVersionKey /LANG=${LANG_ENGLISH} "FileVersion" "${ProductVersion}"


;------------------------------------------------------------------------------
;Global variables

  Var DeleteHistory
  Var DeleteSettings
  Var BrandText
  
;------------------------------------------------------------------------------
;Installer Sections

Section
  
  DetailPrint "Installing ${ProductName}"
  Call IsDotNetInstalled
  
  DetailPrint "Copying new files"
  ;Create a directory in the user's local app data folder
  CreateDirectory "${AppDataDir}"
  
  ;Create sub directories for Logs and Themes
  CreateDirectory "${AppDataDir}\Logs"
  CreateDirectory "${AppDataDir}\Themes"
  
  ;Set output path to this new directory
  SetOutPath "${AppDataDir}\Themes"  
  ;Copy file to this directory
  File "files\theme_office.xml"

  ;Create directory in user's Documents folder storing received files
  CreateDirectory "$DOCUMENTS\Received Files"

  ;Set output path to the installation directory
  SetOutPath $INSTDIR  
  ;Copy application files to the installation directory  
  File "..\LANChat\bin\Release\${AppExec}"
  File "..\LANChat\bin\Release\lanui.dll"
  File "..\LANChat\bin\Release\lanres.dll"
  
  ;Create uninstaller
  WriteUninstaller "$INSTDIR\${Uninstaller}"

  ;Write application information into the registry
  DetailPrint "Creating Registry entries and keys..."
  WriteRegStr HKLM "${AppRegKey}" "InstallDir" $INSTDIR
  WriteRegStr HKLM "${AppRegKey}" "Version" "${ProductVersion}"
  WriteRegStr HKLM "${AppRegkey}" "FirstRun" "0"
  
  ;Write the application path into the startup application group in registry
  ;based on the value in the settings file. Calling application with /sync
  ;switch will handle this.
  ExecWait "$INSTDIR\${AppExec} /sync"

  ;Write the uninstall keys for Windows  
  WriteRegStr HKLM "${UninstKey}" "DisplayName" "${ProductName}"
  WriteRegStr HKLM "${UninstKey}" "UninstallString" "$INSTDIR\${Uninstaller}"
  WriteRegStr HKLM "${UninstKey}" "InstallLocation" "$INSTDIR"
  WriteRegStr HKLM "${UninstKey}" "DisplayIcon" "$INSTDIR\${AppExec},0"
  WriteRegStr HKLM "${UninstKey}" "DisplayVersion" "${ProductVersion}"
  WriteRegStr HKLM "${UninstKey}" "Publisher" "${CompanyName}"
  WriteRegStr HKLM "${UninstKey}" "URLInfoAbout" "${ProductUrl}"
  WriteRegStr HKLM "${UninstKey}" "URLUpdateInfo" "${ProductUrl}"
  WriteRegDWORD HKLM "${UninstKey}" "NoModify" 1
  WriteRegDWORD HKLM "${UninstKey}" "NoRepair" 1  
  
  DetailPrint "Adding Windows Firewall Exception '${ProductName}'"
  nsisFirewall::AddAuthorizedApplication "$INSTDIR\${AppExec}" "${ProductName}"
  Pop $0

  ;Create Start menu shortcuts (for all users)
  DetailPrint "Adding Start menu shortcuts"
  SetShellVarContext all
  CreateDirectory "${StartMenuDir}"
  CreateShortCut "${StartMenuDir}\${ProductName}.lnk" "$INSTDIR\${AppExec}" "" "$INSTDIR\${AppExec}" \
    0 SW_SHOWNORMAL "" "Send or receive instant messages."
  ;CreateShortCut "${StartMenuDir}\${ProductName} - Debug.lnk" "$INSTDIR\${AppExec}" "-debug" "$INSTDIR\${AppExec}" \
  ;  0 SW_SHOWNORMAL "" "Start ${ProductName} in debug mode."
  CreateShortCut "${StartMenuDir}\Uninstall ${ProductName}.lnk" "$INSTDIR\${Uninstaller}" "" "$INSTDIR\${Uninstaller}" \
    0 SW_SHOWNORMAL "" "Uninstall ${ProductName}"
  SetShellVarContext current

SectionEnd


;------------------------------------------------------------------------------
;Uninstaller Section

Section "Uninstall"

  DetailPrint "Uninstalling ${ProductName}"
  
  ;Delete Start menu shortcuts
  DetailPrint "Removing Start menu shortcuts"
  SetShellVarContext all
  Delete "${StartMenuDir}\*.*"
  RMDir "${StartMenuDir}"
  SetShellVarContext current
  
  DetailPrint "Removing Windows Firewall Exception '${ProductName}'"
  nsisFirewall::RemoveAuthorizedApplication "$INSTDIR\${AppExec}"
  Pop $0
  
  ;Remove registry keys
  DetailPrint "Deleting registry entries and keys..."
  DeleteRegKey HKLM "${UninstKey}"
  DeleteRegKey HKLM "${AppRegKey}"
  DeleteRegKey /ifempty HKLM "${CompanyRegKey}"
  DeleteRegValue HKCU "${RunKey}" "${ProductName}"
  
  DetailPrint "Removing files and folders"
  ;Delete application data files and folders
  Delete "${AppDataDir}\Logs\*.*"
  RMDir "${AppDataDir}\Logs"
  Delete "${AppDataDir}\Themes\*.*"
  RMDir "${AppDataDir}\Themes"
  
  ;Delete settings and history files if selected by user
  Delete "${AppDataDir}\groups.bin"
  ExecWait "$INSTDIR\${AppExec} $DeleteHistory $DeleteSettings /quit"
  
  RMDir "${AppDataDir}"
  RMDir "${CompanyDataDir}"
  
  RMDir "$DOCUMENTS\Received Files"
  
  ;Delete application files and folder
  Delete "$INSTDIR\${AppExec}"
  Delete "$INSTDIR\lanui.dll"
  Delete "$INSTDIR\lanres.dll"
  Delete "$INSTDIR\${Uninstaller}"
  RMDir "$INSTDIR"  
  
SectionEnd


;------------------------------------------------------------------------------
;Functions

  Function .onInit
  
    StrCpy $BrandText "${ProductName} v${ProductVersion} Installer"
    Call IsAlreadyInstalled
    
  FunctionEnd
  
  
  ;Check if the application is already installed. It must be uninstalled first.
  Function IsAlreadyInstalled
    
    ;Get uninstaller path of installed version. if any
    ReadRegStr $R0 HKLM "${UninstKey}" "UninstallString"
    StrCmp $R0 "" NotInstalled
    ;Ask user whether current installation should be removed or not. If not, abort installation
    MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION \
      "${ProductName} has already been installed on your computer.$\n$\n\
      Click OK to remove the previous version and continue this installation or Cancel to quit." \
      IDOK Uninst
    Abort
    
    Uninst:
    ;Get previous install directory
    ReadRegStr $R1 HKLM "${UninstKey}" "InstallLocation"
    ClearErrors
    ExecWait "$R0 _?=$R1" $0
    ;Check exit code of uninstaller to see if uninstallation was succesful, else abort installation
    IntCmp $0 0 0 UninstCancelled UninstCancelled
    IfErrors UninstError
    ;Delete uninstaller and install directory of previous version
    Delete "$R0"
    RMDir "$R1"
    goto NotInstalled
    
    UninstError:
    
    UninstCancelled:
    ;Abort setup after showing an error message
    MessageBox MB_OK|MB_ICONSTOP "Uninstallation of the previous version was cancelled.$\n$\nSetup will now exit."
    Abort
    
    NotInstalled:
    
  FunctionEnd
  
  
  ;Microsoft .NET Framework 4.0 Client Package or greater needs to be installed for this installation to continue.
  ;Downloads and installs .Net Framework setup depending on user choice.
  Function IsDotNetInstalled
    
    ;Check if .Net 4.0 Client in present
    DetailPrint "Checking for Microsoft .Net Framework 4.0 Client Package"
    ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Client" "Install"
    IntCmp $0 1 FoundDotNet
    
    ;.Net 4.0 Client not found, ask user whether to install it
    ;If user clicked Cancel, exit setup with error message
    DetailPrint "Microsoft .Net Framework 4.0 Client Package is not installed"
    MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION  \
      "${ProductName} requires Microsoft .NET Framework 4.0 Client Package to be installed on your computer.$\n$\n\
      Click OK to install or Cancel to quit." \
      IDOK DownloadDotNet
    StrCpy $R8 1  ;1 indicates abort status
    StrCpy $R9 1  ;Go forward 1 page
    Call RelGotoPage
    
    ;Download .Net 4.0 Client web installer
    DownloadDotNet:
    DetailPrint "Download: Microsoft .Net Framework 4.0 Client Package installer"
    inetc::get /TIMEOUT=30000 /QUESTION "" ${DotNetUrl} "$TEMP\dotNetFx40_Client_setup.exe" /END
    Pop $0
    ;Check if download was succesful, else exit setup with error message
    StrCmp $0 "OK" InstallDotNet GiveUpDotNet
    
    InstallDotNet:
    ;Run the web installer to install .Net 4.0 Client
    DetailPrint "Running external installer for Microsoft .Net Framework 4.0 Client Package..."
    ExecWait "$TEMP\dotNetFx40_Client_setup.exe /norestart" $0
    ;Check the exit code of installer to see if installation was succesful, else exit setup with error message
    IntCmp $0 0 FoundDotNet
    
    GiveUpDotNet:
    ;Exit setup after showing an error message to the user
    DetailPrint "Error: Microsoft .Net Framework 4.0 Client Package installation was not succesful"
    ;MessageBox MB_OK|MB_ICONSTOP "Microsoft .NET Framework 4.0 Client Package was not installed.$\n$\nSetup will now exit."
    StrCpy $R8 1  ;1 indicates abort status
    StrCpy $R9 1  ;Go forward 1 page
    Call RelGotoPage
    
    FoundDotNet:
    DetailPrint "Microsoft .Net Framework 4.0 Client Package is installed"

  FunctionEnd
  
  
  ;Makes NSIS to go to a specified page relatively from the current page
  Function RelGotoPage
    
    IntCmp $R9 0 0 Move Move
    StrCmp $R9 "X" 0 Move
    StrCpy $R9 "120"
     
    Move:
    SendMessage $HWNDPARENT "0x408" "$R9" ""
    
  FunctionEnd  
  
  
  Function un.onInit
  
    StrCpy $BrandText "${ProductName} v${ProductVersion} Uninstaller"
    
  FunctionEnd
  
  
  ;Check if the application is currently running. If running ask for user confirmation to close app.
  Function un.IsAppRunning
  
    ;Check if application is running.
    ;Run the application executable with /inst switch. Returns 0 if not running, 1 otherwise.
    ExecWait "$INSTDIR\${AppExec} /inst" $0
    IntCmp $0 0 NotRunning
    
    MessageBox MB_OKCANCEL|MB_ICONQUESTION \
      "${ProductName} must be closed to proceed with the uninstall.$\n$\n\
      Click OK to close ${ProductName} or Cancel to quit." \
      IDOK CloseApp
    ;Set exit code to 1 (user cancel) and quit setup.
    SetErrorLevel 1
    Quit
    
    CloseApp:
    ;Run the application with /term switch to terminate the running instance.
    ExecWait "$INSTDIR\${AppExec} /term"
    
    NotRunning:
    
  FunctionEnd
  
  
;------------------------------------------------------------------------------
;Custom Page Functions
  
  ;Interface variables
  Var OptionsPage
  Var OptionsPage.DirectoryText
  Var OptionsPage.Directory
  Var OptionsPage.DeleteHistory
  Var OptionsPage.DeleteHistory_State
  Var OptionsPage.DeleteSettings
  Var OptionsPage.DeleteSettings_State
  Var OptionsPage.Text
  Var FinishPage.FinishText
  
  
  ;Function called when installation finish page is shown
  Function FinishPageShow
  
    IntCmp $R8 0 NotAborted
    ${NSD_SetText} $mui.FinishPage.Title "${ProductName} was not installed"
    ${NSD_SetText} $mui.FinishPage.Text "The wizard was interrupted before ${ProductName} could be completely installed.$\n$\n\
        Your system has not been modified. To install this program at a later time, please run the installation again."
        
    SendMessage $mui.FinishPage.Run ${BM_SETCHECK} ${BST_UNCHECKED} 0
    ShowWindow $mui.FinishPage.Run ${SW_HIDE}
    
    ${NSD_CreateLabel} 120u 120u 195u 10u "Click Finish to close this wizard."
    Pop $FinishPage.FinishText
    SetCtlColors $FinishPage.FinishText "" "${MUI_BGCOLOR}"
    
    SetErrorLevel 1
    
    NotAborted:
    
  FunctionEnd
  
  
  ;Function called when user clicks Next on uninstall welcome page
  Function un.WelcomePageLeave
    
    Call un.IsAppRunning

  FunctionEnd  
  
  
  ;Displays a custom uninstallation confirm page.
  ;Checkboxes are shown giving the user the option to delete history and preferences when uninstalling.
  Function un.OptionsPageShow
    
    nsDialogs::Create 1018
    Pop $OptionsPage
    GetFunctionAddress $0 un.OptionsPageLeave
    nsDialogs::OnBack $0
    
    ${If} $OptionsPage == error
      Abort
    ${EndIf}
    
    ;Set header text and sub text
    !insertmacro MUI_HEADER_TEXT_PAGE $(MUI_UNTEXT_CONFIRM_TITLE) $(MUI_UNTEXT_CONFIRM_SUBTITLE)
    
    ;Add controls to the page
    ${NSD_CreateLabel} 0 0 100% 12u "${ProductName} will be uninstalled from the following location:"
    Pop $OptionsPage.DirectoryText
    
    ${NSD_CreateText} 0 13u 100% 12u "$INSTDIR"
    Pop $OptionsPage.Directory
    SendMessage $OptionsPage.Directory ${EM_SETREADONLY} 1 0
    
    ${NSD_CreateCheckBox} 0 40u 100% 10u "Delete history"
    Pop $OptionsPage.DeleteHistory
    ;Set checked state based on previous user input
    ${NSD_SetState} $OptionsPage.DeleteHistory $OptionsPage.DeleteHistory_State
    
    ${NSD_CreateCheckBox} 0 55u 100% 10u "Delete preferences"
    Pop $OptionsPage.DeleteSettings
    ;Set checked state based on previous user input
    ${NSD_SetState} $OptionsPage.DeleteSettings $OptionsPage.DeleteSettings_State
    
    ${NSD_CreateLabel} 0 -13u 100% 12u "Click Uninstall to continue."
    Pop $OptionsPage.Text
    
    nsDialogs::Show

  FunctionEnd

  
  ;Remembers the state of checkboxes when user navigates backward or forward from the page
  Function un.OptionsPageLeave

    ${NSD_GetState} $OptionsPage.DeleteHistory $OptionsPage.DeleteHistory_State
    ${If} $OptionsPage.DeleteHistory_State == ${BST_CHECKED}
      StrCpy $DeleteHistory "/nohistory"
    ${Else}
      StrCpy $DeleteHistory ""
    ${EndIf}
    
    ${NSD_GetState} $OptionsPage.DeleteSettings $OptionsPage.DeleteSettings_State
    ${If} $OptionsPage.DeleteSettings_State == ${BST_CHECKED}
      StrCpy $DeleteSettings "/noconfig"
    ${Else}
      StrCpy $DeleteSettings ""
    ${EndIf}

  FunctionEnd