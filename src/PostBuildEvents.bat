REM	PostBuildEvents.bat is called with the following parameters
REM		%1 is TargetPath
REM		%2 is TargetDir
REM     %3 is ProjectName
REM     %4 is ProjectDir

REM	/S (Copies directories and subdirectories except empty ones)
REM /Y (Suppresses prompting to confirm you want to overwrite an existing destination file.)

SET WEBSITE_PATH={YOUR-WEBSITE-LOCATION}

REM Copy the compiled code (*.dll) to website folder
XCOPY /S /Y "%1" "%WEBSITE_PATH%\bin\"

REM Copy the debug files (*.pdb)
XCOPY /S /Y "%2%3.pdb" "%WEBSITE_PATH%\bin\"

REM Copy package front-end files
XCOPY /S /Y "%4Web\UI\*.*" "%WEBSITE_PATH%"
