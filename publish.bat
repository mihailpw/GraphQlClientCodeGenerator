@echo off 
del "bin\*.*" /s /f /q
(for %%r in (win-x64 win-x86 win-arm osx-x64 linux-x64 linux-arm) do (
	echo Building %%r
	dotnet publish "src/GQLCCG/GQLCCG.csproj" -o "bin" -c Release -r %%r --nologo /p:PublishSingleFile=true
	del "bin\GQLCCG.pdb" /s /f /q
	ren "bin\GQLCCG.*" "gqlccg-%%r.*"
))