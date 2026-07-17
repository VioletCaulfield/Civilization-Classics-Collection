; ====================================================
; Civilization Classics Collection Installer
;
; Developed by Violet Caulfield
;
; This installer packages all three supported games,
; required compatibility components, documentation,
; launcher and supporting files into a single install.
;
; If you're reading this to learn Inno Setup, I hope
; some of it is useful to youuuuu. :)
; ====================================================


; Remember to adjust your directories to wherever you clone the repo to as appropriate.

[Setup]

; ----------------------------------------------------
; Basic installer information.
; This is where the installer name, output filename
; and general installer behaviour is configured.
; ----------------------------------------------------

AppName=Civilization Classic Collection
AppVersion=0.6
DefaultDirName={sd}\Games\CivClassicCollection
DefaultGroupName=Civilization Classic Collection
OutputDir=Output
OutputBaseFilename=CivClassicCollection_Installer_v0.6
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
AlwaysRestart=no
RestartIfNeededByRun=no

WizardImageFile=C:\Users\user\Desktop\CivClassicCollectionSource\Assets\wizard.bmp
WizardSmallImageFile=C:\Users\user\Desktop\CivClassicCollectionSource\Assets\small.bmp
SetupIconFile=C:\Users\user\Desktop\CivClassicCollectionSource\Assets\appicon.ico

InfoBeforeFile=C:\Users\user\Desktop\CivClassicCollectionSource\Assets\README.txt
AppPublisher=Violet Caulfield
UninstallDisplayIcon={app}\uninstall.ico

[Dirs]

; ----------------------------------------------------
; Create the shared folders used by the Collection.
; Some of these may remain empty until the user starts
; importing mods or creating saves.
; ----------------------------------------------------

Name: "{app}\Documentation"
Name: "{app}\Mods"
Name: "{app}\Music"
Name: "{app}\Saves"
Name: "{app}\Temp"

[Files]

; ----------------------------------------------------
; Required Runtime Installers
; ----------------------------------------------------
Source: "windowsdesktop-runtime-10.0.9-win-x64.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall

Source: "windowsdesktop-runtime-10.0.9-win-x86.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall

Source: "VC_redist.x86.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall

; ----------------------------------------------------
; LAV Filters
; ----------------------------------------------------
Source: "LAVFilters-0.81-Installer.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall

; ----------------------------------------------------
; Civilization Game Files
;
; Installs the complete game directories.
; Files are copied recursively to preserve the
; original directory structure.
; ----------------------------------------------------

; ----------------------------------------------------
; Civilization
; ----------------------------------------------------
Source: "Civ1Win_InstallerSource\Civ1\*"; DestDir: "{app}\Civ1"; Flags: recursesubdirs createallsubdirs ignoreversion

; ----------------------------------------------------
; Civilization II Multiplayer Gold Edition
; ----------------------------------------------------
Source: "Civ2MGE_InstallerSource\Civ2\*"; DestDir: "{app}\Civ2MGE"; Flags: recursesubdirs createallsubdirs ignoreversion

; ----------------------------------------------------
; Civilization II Test of Time
; ----------------------------------------------------
Source: "Civ2ToT_InstallerSource\Civ2ToT\*"; DestDir: "{app}\Civ2ToT"; Flags: recursesubdirs createallsubdirs ignoreversion

; ----------------------------------------------------
; Shared Documentation
; ----------------------------------------------------
Source: "Documentation\*"; DestDir: "{app}\Documentation"; Flags: recursesubdirs createallsubdirs ignoreversion

; ----------------------------------------------------
; Shared Codecs
; ----------------------------------------------------
Source: "Codecs\*"; DestDir: "{sys}"; Flags: ignoreversion

; ----------------------------------------------------
; Shared Mods
; ----------------------------------------------------
Source: "Mods\*"; DestDir: "{app}\Mods"; Flags: recursesubdirs createallsubdirs ignoreversion

; ----------------------------------------------------
; Collection Music
; ----------------------------------------------------
Source: "Music\*"; DestDir: "{app}\Music"; Flags: recursesubdirs createallsubdirs ignoreversion

; ----------------------------------------------------
; Shared Saves
; ----------------------------------------------------
Source: "Saves\*"; DestDir: "{app}\Saves"; Flags: recursesubdirs createallsubdirs ignoreversion

; ----------------------------------------------------
; Collection Launcher Files
; ----------------------------------------------------
Source: "CivClassicLauncher.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "CivClassicLauncher.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "CivClassicLauncher.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "CivClassicLauncher.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "SharpCompress.dll"; DestDir: "{app}"; Flags: ignoreversion

; Optional launcher components.
; These are only copied if they exist in the build
; directory. Makes development builds a little easier.
Source: "AxInterop.WMPLib.dll"; DestDir: "{app}"; Flags: ignoreversion; Check: FileExists(ExpandConstant('{src}\AxInterop.WMPLib.dll'))
Source: "Interop.WMPLib.dll"; DestDir: "{app}"; Flags: ignoreversion; Check: FileExists(ExpandConstant('{src}\Interop.WMPLib.dll'))

; ----------------------------------------------------
; Icons
; ----------------------------------------------------
Source: "Assets\setup.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "Assets\uninstall.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "Assets\appicon.ico"; DestDir: "{app}"; Flags: ignoreversion

[Registry]

; ----------------------------------------------------
; Register Indeo codec (64-bit Windows)

; This will register the legacy Indeo video codec on the computer.
; The old video files relys on this for certain videos.
; Windows no longer registers these automatically.
; ----------------------------------------------------
Root: HKLM64; Subkey: "SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Drivers32"; ValueType: string; ValueName: "VIDC.IV41"; ValueData: "ir41_32.ax"; Check: IsWin64; Flags: uninsdeletevalue

; ----------------------------------------------------
; Register Indeo codec (32-bit Windows)
; ----------------------------------------------------
Root: HKLM; Subkey: "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Drivers32"; ValueType: string; ValueName: "VIDC.IV41"; ValueData: "ir41_32.ax"; Check: not IsWin64; Flags: uninsdeletevalue

[Icons]

Name: "{group}\Civilization Classic Collection"; Filename: "{app}\CivClassicLauncher.exe"; IconFilename: "{app}\appicon.ico"

Name: "{commondesktop}\Civilization Classic Collection"; Filename: "{app}\CivClassicLauncher.exe"; IconFilename: "{app}\appicon.ico"

[Run]

; ----------------------------------------------------
; Install .NET Desktop Runtime (64-bit) if required
; ----------------------------------------------------
Filename: "{tmp}\windowsdesktop-runtime-10.0.9-win-x64.exe"; Parameters: "/install /quiet /norestart"; Flags: waituntilterminated; Check: IsWin64 and NeedsDotNetRuntime64

; ----------------------------------------------------
; Install .NET Desktop Runtime (32-bit) If Required
; ----------------------------------------------------
Filename: "{tmp}\windowsdesktop-runtime-10.0.9-win-x86.exe"; Parameters: "/install /quiet /norestart"; Flags: waituntilterminated; Check: (not IsWin64) and NeedsDotNetRuntime32
; ----------------------------------------------------
; Install Visual C++ Runtime if required
; ----------------------------------------------------
Filename: "{tmp}\VC_redist.x86.exe"; Parameters: "/install /quiet /norestart"; Flags: waituntilterminated; Check: NeedsVCRuntime

; ----------------------------------------------------
; Install LAV Filters if required
; ----------------------------------------------------
Filename: "{tmp}\LAVFilters-0.81-Installer.exe"; Parameters: "/S"; Flags: waituntilterminated runhidden; Check: NeedsLAVFilters

; ----------------------------------------------------
; Register Indeo codecs
; ----------------------------------------------------
Filename: "{syswow64}\regsvr32.exe"; Parameters: "/s ""{syswow64}\ir41_32.ax"""; Flags: runhidden waituntilterminated

Filename: "{syswow64}\regsvr32.exe"; Parameters: "/s ""{syswow64}\ir41_32.dll"""; Flags: runhidden waituntilterminated

; ----------------------------------------------------
; Launch Collection Launcher
; ----------------------------------------------------
Filename: "{app}\CivClassicLauncher.exe"; Description: "Launch Civilization Classic Collection"; Flags: nowait postinstall skipifsilent


[Code]

var
  ResultCode: Integer;
  TempString: string;
    
  function GetCodecFolder(): String;
begin
  if IsWin64 then
    Result := ExpandConstant('{syswow64}')
  else
    Result := ExpandConstant('{sys}');
end;

function GetDrivers32Key(): String;
begin
  if IsWin64 then
    Result := 'SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Drivers32'
  else
    Result := 'SOFTWARE\Microsoft\Windows NT\CurrentVersion\Drivers32';
end;


function NeedsLAVFilters(): Boolean;
// Looks for an existing LAV Filters installation.
// If found we won't reinstall it.

begin
  Result :=
    not DirExists(ExpandConstant('{pf}\LAV Filters')) and
    not DirExists(ExpandConstant('{pf32}\LAV Filters'));
end;


function NeedsDotNetRuntime64(): Boolean;
// Checks whether the required .NET Desktop Runtime is already installed. If version 10.x exists we skip installing it again.
var
  Version: String;
begin
  Result := True;

  if RegQueryStringValue(
    HKLM64,
    'SOFTWARE\dotnet\Setup\InstalledVersions\x64\sharedfx\Microsoft.WindowsDesktop.App',
    'Version',
    Version
  ) then
  begin
    if Pos('10.', Version) = 1 then
      Result := False;
  end;
end;

function NeedsDotNetRuntime32(): Boolean;
// Checks whether the required .NET Desktop Runtime is already installed. If version 10.x exists we skip installing it again.
var
  Version: String;
begin
  Result := True;

  if RegQueryStringValue(
    HKLM,
    'SOFTWARE\dotnet\Setup\InstalledVersions\x86\sharedfx\Microsoft.WindowsDesktop.App',
    'Version',
    Version
  ) then
  begin
    if Pos('10.', Version) = 1 then
      Result := False;
  end;
end;

function NeedsVCRuntime(): Boolean;
begin
  Result :=
    not FileExists(
      ExpandConstant('{sys}\vcruntime140.dll')
    );
end;


// ----------------------------------------------------
// DirectPlay is an optional legacy Windows component.
// Some of the games still expect it to be installed.
// We ask the user whether they'd like Windows to enable it after installation.
// ----------------------------------------------------

function IsDirectPlayEnabled(): Boolean;
var
  ResultCode: Integer;
  Output: TExecOutput;
  I: Integer;
begin
  Result := False;

  if ExecAndCaptureOutput(
    ExpandConstant('{cmd}'),
    '/C dism /online /Get-FeatureInfo /FeatureName:DirectPlay',
    '',
    SW_SHOWNORMAL,
    ewWaitUntilTerminated,
    ResultCode,
    Output
  ) then
  begin
    for I := 0 to GetArrayLength(Output.StdOut) - 1 do
    begin
      if Pos('State : Enabled', Output.StdOut[I]) > 0 then
      begin
        Result := True;
        Exit;
      end;
    end;
  end;
end;

procedure EnableDirectPlay();
begin
  if IsDirectPlayEnabled() then
    Exit;
  WizardForm.StatusLabel.Caption :=
    'Preparing Civilization Classics Collection...';
  WizardForm.Update();

  if MsgBox(
    'Some classic Civilization games use Microsoft''s legacy DirectPlay component.' + #13#13 +
    'Windows can enable this automatically.' + #13 +
    'This is recommended for maximum compatibility.' + #13#13 +
    'Enable DirectPlay now?',
    mbConfirmation,
    MB_YESNO
  ) = IDYES then
  begin

    WizardForm.StatusLabel.Caption :=
      'Enabling DirectPlay...';
    WizardForm.Update();

    Exec(
      ExpandConstant('{cmd}'),
      '/C dism /online /Enable-Feature /FeatureName:DirectPlay /All /NoRestart',
      '',
      SW_HIDE,
      ewWaitUntilTerminated,
      ResultCode
    );
  end;
end;


procedure CurStepChanged(CurStep: TSetupStep);

// Runs once installation has finished.
// We use this point to offer enabling DirectPlay, whilst keeping the main installation process uninterrupted.

begin
  if CurStep = ssPostInstall then
  begin
    EnableDirectPlay();
  end;
end;
  
// The Collection installs legacy codecs that some users may want to keep for other classic software.
// Because of this we ask before unregistering them.
procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if CurUninstallStep = usUninstall then
  begin
    if MsgBox(
      'Would you like to unregister the Civilization Collection Indeo codecs from Windows?' + #13#13 +
      'This is OPTIONAL.' + #13 +
      'The codec files themselves will remain installed.',
      mbConfirmation,
      MB_YESNO
    ) = IDYES then
    begin

      Exec(
        GetCodecFolder() + '\regsvr32.exe',
        '/u /s "' + GetCodecFolder() + '\ir41_32.ax"',
        '',
        SW_HIDE,
        ewWaitUntilTerminated,
        ResultCode
      );

      Exec(
        GetCodecFolder() + '\regsvr32.exe',
        '/u /s "' + GetCodecFolder() + '\ir41_32.dll"',
        '',
        SW_HIDE,
        ewWaitUntilTerminated,
        ResultCode
      );
if IsWin64 then
begin
  RegDeleteValue(
    HKLM64,
    'SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion\Drivers32',
    'VIDC.IV41'
  );
end
else
begin
  RegDeleteValue(
    HKLM,
    'SOFTWARE\Microsoft\Windows NT\CurrentVersion\Drivers32',
    'VIDC.IV41'
  );
end;
    end;

    if MsgBox(
      'Would you also like to remove all collection files, imported mods, saves, music and other user-created content?' + #13#13 +
      'Selecting YES will completely remove the Civilization Classic Collection folder.',
      mbConfirmation,
      MB_YESNO
    ) = IDYES then
    begin
      DelTree(
        ExpandConstant('{app}'),
        True,
        True,
        True
      );
    end;

  end;
end;