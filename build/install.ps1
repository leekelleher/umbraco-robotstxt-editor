param($installPath, $toolsPath, $package, $project)

$appPluginsFolder = $project.ProjectItems | Where-Object { $_.Name -eq "App_Plugins" }
$robotsPluginFolder = $appPluginsFolder.ProjectItems | Where-Object { $_.Name -eq "RobotsTxtEditor" }

if (!$robotsPluginFolder)
{
	$newPackageFiles = "$installPath\Content\App_Plugins\RobotsTxtEditor"

	$projFile = Get-Item ($project.FullName)
	$projDirectory = $projFile.DirectoryName
	$projectPath = Join-Path $projDirectory -ChildPath "App_Plugins"
	$projectPathExists = Test-Path $projectPath

	if ($projectPathExists) {
		Write-Host "Updating RobotsTxtEditor App_Plugin files using PS as they have been excluded from the project"
		Copy-Item $newPackageFiles $projectPath -Recurse -Force
	}
}
