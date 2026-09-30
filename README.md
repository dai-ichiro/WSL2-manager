### Building

PowerShell
```powershell
# Debug build
&"C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" WinForm.csproj -p:Configuration=Debug

# Release build
&"C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" WinForm.csproj -p:Configuration=Release
```
