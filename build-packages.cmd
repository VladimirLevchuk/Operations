@echo off
rem todo: setup appveyor

SET currentDir=%~dp0

SET project=Operations
call build-package %project%

SET project=Operations.Serilog
call build-package %project%

SET project=Operations.Trackers.Profiler
call build-package %project%

SET project=Operations.Web
call build-package %project%