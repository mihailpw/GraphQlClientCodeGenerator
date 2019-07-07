@echo off 

set buildoutdir=out
set nugetoutdir=nuget-out

rmdir %buildoutdir% /Q /S nonemptydir
rmdir %nugetoutdir% /Q /S nonemptydir
(for %%r in (win-x64 win-x86) do (
	echo Building %%r
	dotnet publish "../../GQLCCG/GQLCCG.csproj" -o "%buildoutdir%/%%r" -c Release -r %%r --nologo /p:PublishTrimmed=true
	del "%buildoutdir%\*.pdb" /s /f /q
))

mkdir %nugetoutdir%

nuget pack GQLCCG.MSBuild.nuspec -OutputDirectory %nugetoutdir%