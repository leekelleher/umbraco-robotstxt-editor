REM	PostBuildEvents.bat is called with the following parameters
REM		%1 is TargetPath
REM		%2 is TargetDir
REM     %3 is ProjectName
REM     %4 is ProjectDir

REM	/S (Copies directories and subdirectories except empty ones)
REM /Y (Suppresses prompting to confirm you want to overwrite an existing destination file.)

REM Copy bin folder to website folder
xcopy /s /y "%1" "{YOUR-WEBSITE-LOCATION}\bin\"

REM Copy pdf
xcopy /s /y "%2%3.pdb" "{YOUR-WEBSITE-LOCATION}\bin\"

REM Will be other stuff to copy over too