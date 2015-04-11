@echo off
".nuget\nuget.exe" "install" "FAKE" "-OutputDirectory" "tools" "-ExcludeVersion" "-version" "3.17.9"

:Build

SET TARGET="Build"

IF NOT [%1]==[] (set TARGET="%1")
"tools\FAKE\tools\Fake.exe" "build.fsx" "target=%TARGET%" "buildMode=Release" %*
