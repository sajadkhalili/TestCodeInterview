

# تنظیم مسیر پروژه (دستگاه خودتان را وارد کنید)

$curDir = $PSScriptRoot;
$solutionDir = (Split-Path $PSScriptRoot -Parent);

# variables
$projectFile="$solutionDir/SSF.Surveys.EndPoint.Api/SSF.Surveys.EndPoint.Api.csproj";


# تنظیم مسیر فایل nswag.json

$nswagExe = "${Env:ProgramFiles(x86)}/Rico Suter/NSwagStudio/Net90/dotnet-nswag.exe";

$namespace = "Surveys.Provider.Api";
$nswagFile = "$curDir/Generated/nswag.json";
$outBaseFile = "$curDir/Generated/api.cs";
$noBuild = $true;

$variables="/variables:namespace=$namespace,apiBaseFile=$outBaseFile,projectFile=$projectFile,,noBuild=$noBuild";
& "$nswagExe" run $nswagFile $variables;