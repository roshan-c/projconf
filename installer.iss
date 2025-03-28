#define MyAppName "ProjConf"
#define MyAppVersion "1.0.2"
#define MyAppPublisher "Roshan C."
#define MyAppExeName "projconf.exe"

[Setup]
AppId={{1234567-1234-1234-1234-123456789ABC}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\ProjConf
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputBaseFilename=projconf_setup
Compression=lzma
SolidCompression=yes
ChangesEnvironment=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "bin\Release\net9.0\win-x64\publish\projconf.exe"; DestDir: "{app}"; Flags: ignoreversion

[Registry]
Root: HKLM; Subkey: "SYSTEM\CurrentControlSet\Control\Session Manager\Environment"; ValueType: expandsz; ValueName: "Path"; ValueData: "{olddata};{app}"; Check: NeedsAddPath(ExpandConstant('{app}'))

[Code]
function NeedsAddPath(Path: string): Boolean;
var
    OrigPath: string;
begin
    if not RegQueryStringValue(HKEY_LOCAL_MACHINE,
        'SYSTEM\CurrentControlSet\Control\Session Manager\Environment',
        'Path', OrigPath)
    then begin
        Result := True;
        exit;
    end;
    Result := Pos(';' + Uppercase(Path) + ';', ';' + Uppercase(OrigPath) + ';') = 0;
end;

[Tasks]
Name: modifypath; Description: "Add to PATH"
