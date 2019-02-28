# Getting started with the code base

Only code related to the package is in this repository.

After cloning the repository the suggested approach for development is:

1. Create a new Umbraco website (v8.0.0) with the starter kit on your local machine and go through the installer
   
2. In your cloned version of this repository, copy src/PostBuildEvents.bat into the 'Our.Umbraco.RobotsTxtEditor' folder

3. Open src/Our.Umbraco.RobotsTxtEditor/PostBuildEvents.bat and specify the location of your website where instructed

4. Open src/Our.Umbraco.RobotsTxtEditor.sln in Visual Studio

5. Build the solution. The post build event of the project should now copy the appropriate files from this project to the correct place in your website.  If you've already built the solution do a 'rebuild' to force the files to be copied.

NB src/Our.Umbraco.RobotsTxtEditor/PostBuildEvents.bat is already in .gitignore so you shouldn't be asked to commit the version of this file with your website location.