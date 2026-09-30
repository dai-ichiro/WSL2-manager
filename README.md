### Building
```bash
# Debug build
powershell.exe -Command "&'C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe' WinForm.csproj -p:Configuration=Debug"

# Release build
powershell.exe -Command "&'C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe' WinForm.csproj -p:Configuration=Release"
```
