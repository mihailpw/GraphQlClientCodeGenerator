@echo off 
del "out\*.*" /s /f /q
(for %%r in (win-x64 win-x86 osx-x64 linux-x64) do (
	echo Building %%r
	dotnet publish "src/GQLCCG/GQLCCG.csproj" -o "out" -c Release -r %%r --nologo /p:PublishSingleFile=true
	del "out\GQLCCG.pdb" /s /f /q
	ren "out\GQLCCG.*" "gqlccg-%%r.*"
))