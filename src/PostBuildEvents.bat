REM	PostBuildEvents.bat is called with the following parameters
REM     %1 is ProjectName
REM     %2 is ProjectDir

REM	/S (Copies directories and subdirectories except empty ones)
REM /Y (Suppresses prompting to confirm you want to overwrite an existing destination file.)

SET WEBSITE_PATH={YOUR-WEBSITE-LOCATION}
SET WEBSITE_VERSION={WEBSITE-VERSION-V8-OR-V9}

IF /I "%WEBSITE_VERSION%" == "V8" (
    REM Copy the compiled code (*.dll) to website folder
    XCOPY /Y "%~2bin\debug\net472\%~1.dll" "%WEBSITE_PATH%\bin\"

    REM Copy the debug files (*.pdb)
    XCOPY /Y "%~2bin\debug\net472\%~1.pdb" "%WEBSITE_PATH%\bin\"
) ELSE (
    REM Copy the compiled code (*.dll) to website folder
    XCOPY /Y "%~2bin\debug\net50\%~1.dll" "%WEBSITE_PATH%\bin\"

    REM Copy the debug files (*.pdb)
    XCOPY /Y "%~2bin\debug\net50\%~1.pdb" "%WEBSITE_PATH%\bin\"
)

REM Copy package front-end files
XCOPY /S /Y "%~2Web\UI\*.*" "%WEBSITE_PATH%"
