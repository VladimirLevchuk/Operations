@echo off
SET project=%1
IF "%project%" == "" GOTO error

SET currentDir=%~dp0

SET projectUrl=https://github.com/VladimirLevchuk/Operations
SET license=%projectUrl%/blob/master/LICENSE
SET releaseNotes=%projectUrl%/blob/master/readme.md#releaseNotes


PUSHD %project%
CALL nuget pack -Build -Symbols -Properties Configuration=Release;license="%license%";releaseNotes="%releaseNotes%";projectUrl=%projectUrl% -OutputDirectory %currentDir% -includereferencedprojects 
rem CALL nuget pack -Symbols -Properties Configuration=Release;license="%license%";releaseNotes="%releaseNotes%" -OutputDirectory %currentDir% -includereferencedprojects 
POPD

GOTO fin

:error
ECHO Usage: 
ECHO 	build-package {MyProjectName} 
ECHO for example:
ECHO 	%~n0 Operations
:fin