#include "scripts\products.iss"
#include "scripts\products\winversion.iss"
#include "scripts\products\fileversion.iss"
#include "scripts\products\msi20.iss"
#include "scripts\products\msi31.iss"
#include "scripts\products\kb835732.iss"
#include "scripts\products\dotnetfx20.iss"
#include "scripts\products\dotnetfx20sp1.iss"
#include "scripts\products\dotnetfx20sp2.iss"


[CustomMessages]
CreateFileAssociations=Associate .crf files with CRFSuite
FileAssociations=File association:
win2000sp3_title=Windows 2000 Service Pack 3
winxpsp2_title=Windows XP Service Pack 2

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{2F29FEA1-5590-4EE3-90AA-4D3D86EDD9AE}
AppName=CRFSuite
AppVerName=CRFSuite 3.1.0
AppPublisher=CRF
AppPublisherURL=http://www.cardreaderfactory.com/
AppSupportURL=http://www.cardreaderfactory.com/
AppUpdatesURL=http://www.cardreaderfactory.com/
DefaultDirName={pf}\CRFSuite
DefaultGroupName=CRFSuite
AllowNoIcons=yes
LicenseFile=license.rtf
OutputBaseFilename=CRFSuiteSetup
SetupIconFile=.\System-Install.ico
Compression=lzma2/max
SolidCompression=yes
ChangesAssociations=yes
MinVersion=4.1,5.0
PrivilegesRequired=admin
ArchitecturesInstallIn64BitMode=x64 ia64
; WizardImageFile= ....

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}
Name: quicklaunchicon; Description: {cm:CreateQuickLaunchIcon}; GroupDescription: {cm:AdditionalIcons}
Name: fileassociation; Description: {cm:CreateFileAssociations}; GroupDescription: {cm:FileAssociations}

[Languages]
Name: en; MessagesFile: compiler:Default.isl

[Files]
Source: Output\CRFSuite.exe; DestDir: {app}; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files


[Icons]
Name: {group}\CRFSuite; Filename: {app}\CRFSuite.exe
Name: {group}\{cm:ProgramOnTheWeb,CRFSuite}; Filename: http://www.cardreaderfactory.com/; IconFilename: {app}\CRFSuite.exe
Name: {group}\{cm:UninstallProgram,CRFSuite}; Filename: {uninstallexe}
Name: {commondesktop}\CRFSuite; Filename: {app}\CRFSuite.exe; Tasks: desktopicon
Name: {userappdata}\Microsoft\Internet Explorer\Quick Launch\CRFSuite; Filename: {app}\CRFSuite.exe; Tasks: quicklaunchicon

[Run]
Filename: {app}\CRFSuite.exe; Description: {cm:LaunchProgram,CRFSuite}; Flags: nowait postinstall skipifsilent

[Registry]
Root: HKCR; Subkey: .crf; ValueType: string; ValueName: ; ValueData: CRFSuite; Flags: uninsdeletevalue; Tasks: fileassociation
Root: HKCR; Subkey: CRFSuite; ValueType: string; ValueName: ; ValueData: CRFSuite; Flags: uninsdeletekey; Tasks: fileassociation
Root: HKCR; Subkey: CRFSuite\DefaultIcon; ValueType: string; ValueName: ; ValueData: {app}\CRFSuite.exe,0; Tasks: fileassociation
Root: HKCR; Subkey: CRFSuite\shell\open\command; ValueType: string; ValueName: ; ValueData: """{app}\CRFSuite.exe"" ""%1"""; Tasks: fileassociation

[Code]
function InitializeSetup(): Boolean;
begin
	//init windows version
	initwinversion();

	//check if dotnetfx20 can be installed on this OS
	if not minwinspversion(5, 0, 3) then begin
		MsgBox(FmtMessage(CustomMessage('depinstall_missing'), [CustomMessage('win2000sp3_title')]), mbError, MB_OK);
		exit;
	end;
	if not minwinspversion(5, 1, 2) then begin
		MsgBox(FmtMessage(CustomMessage('depinstall_missing'), [CustomMessage('winxpsp2_title')]), mbError, MB_OK);
		exit;
	end;

	//if (not iis()) then exit;

	msi20('2.0');
	msi31('3.0');

	//install .netfx 2.0 sp2 if possible; if not sp1 if possible; if not .netfx 2.0
	if minwinversion(5, 1) then begin
		dotnetfx20sp2();
	end else begin
		if minwinversion(5, 0) and minwinspversion(5, 0, 4) then begin
			kb835732();
			dotnetfx20sp1();
		end else begin
			dotnetfx20();
		end;
	end;

	Result := true;
end;
