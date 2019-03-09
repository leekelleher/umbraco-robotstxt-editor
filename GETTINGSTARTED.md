# Getting started with the code base

Only code related to the package is in this repository, so you are going to need an Umbraco website of your own to test your changes. After cloning this repository the suggested approach for development is:

1. Create a new Umbraco website (v8.0.0) with the starter kit on your local machine and go through the install process (or use any existing website, so long as it is the correct version of Umbraco)
   
2. In your cloned version of this repository, copy `src/PostBuildEvents.bat` into the `src/Our.Umbraco.RobotsTxtEditor/` folder

3. Open `src/Our.Umbraco.RobotsTxtEditor/PostBuildEvents.bat` and specify the path of your website where instructed

4. Open `src/Our.Umbraco.RobotsTxtEditor.sln` in Visual Studio and build the solution. The post build event of the project should copy the appropriate files from this project to the corresponding location in your website. If you've already built the solution do a 'rebuild' to force the files to be copied. `src/Our.Umbraco.RobotsTxtEditor/PostBuildEvents.bat` is already in .gitignore so you shouldn't be asked to commit the version of this file with your website location.

5. In order for any JavaScript files to work, you'll need to change the Client Dependency version in `config\ClientDependency.config` in your Umbraco instance. If you set 'debug' to true in your web.config file this will stop Client Dependency from caching the files in the App_Plugins folder so a simple browser refresh should reflect any changes you make.
