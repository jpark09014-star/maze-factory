# Build dist\베타아빠미로공장.exe (needs docs\index.html from build-web.ps1). Uses the .NET Framework compiler built into Windows.
$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot
$csc = "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\csc.exe"
New-Item -ItemType Directory -Force "$root\dist" | Out-Null
$out = Join-Path $root 'dist\베타아빠미로공장.exe'
& $csc /nologo /target:winexe /optimize+ /codepage:65001 "/out:$out" "/win32icon:$root\src\app.ico" "/resource:$root\docs\index.html,index.html" /r:System.Windows.Forms.dll "$root\src\Launcher.cs"
if ($LASTEXITCODE -ne 0) { throw "csc failed" }
Copy-Item "$root\src\Jua-OFL.txt" "$root\dist\폰트라이선스-Jua-OFL.txt" -Force
"exe bytes: " + (Get-Item $out).Length
(Get-FileHash $out -Algorithm SHA256).Hash
