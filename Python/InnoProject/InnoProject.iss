; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "UniGayo"
#define MyAppVersion "1.5"
#define MyAppPublisher "Rumpus Animation"
#define MyAppURL "www.rumpusanimation.com/unigayo"
#define MyAppExeName "unigayo.exe"
#define SourcePath "D:\GitProjects\unigaya"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{09E1EBD8-AD91-4E6C-AE50-A875A5C2832A}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}     
AllowNoIcons=yes
LicenseFile={#SourcePath}\Python\InnoProject\License.rtf
InfoBeforeFile={#SourcePath}\Python\InnoProject\InformationBefore.rtf
InfoAfterFile={#SourcePath}\Python\InnoProject\InformationAfter.rtf
OutputDir={#SourcePath}\Builds\SetupWindows
OutputBaseFilename=unigayo-setup
SetupIconFile={#SourcePath}\Python\papagayo.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "{#SourcePath}\Python\dist\unigayo.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourcePath}\Python\dist\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
;Source:

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

;[Code]


[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

