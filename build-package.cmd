@echo off
SET project=%1
IF "%project%" == "" GOTO error

SET currentDir=%~dp0

SET license=https://github.com/VladimirLevchuk/Operations/blob/master/LICENSE
SET releaseNotes=https://github.com/VladimirLevchuk/Operations/blob/master/readme.md#releaseNotes

PUSHD %project%
rem CALL nuget pack -Build -Symbols -Properties Configuration=Release;license="%license%";releaseNotes="%releaseNotes%" -OutputDirectory %currentDir% -includereferencedprojects 
CALL nuget pack -Symbols -Properties Configuration=Release;license="%license%";releaseNotes="%releaseNotes%" -OutputDirectory %currentDir% -includereferencedprojects 
POPD

GOTO fin

:error
ECHO Usage: 
ECHO 	build-package {MyProjectName} 
ECHO for example:
ECHO 	%~n0 Operations
:fin