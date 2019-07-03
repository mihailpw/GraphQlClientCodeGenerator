@echo off 

set outdir=out

del "%outdir%\*.*" /s /f /q
(for %%r in (win-x64 win-x86 osx-x64 linux-x64) do (
	echo Building %%r
	dotnet publish "src/GQLCCG/GQLCCG.csproj" -o "%outdir%" -c Release -r %%r --nologo /p:PublishSingleFile=true /p:PublishTrimmed=true
	del "%outdir%\GQLCCG.pdb" /s /f /q
	ren "%outdir%\GQLCCG.*" "gqlccg-%%r.*"
))
