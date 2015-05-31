#FHNW ecnf
#Script for automatic NuGet
#Written by Philipp Steiner
#NuGet.exe has to be in the same folder

$path = "C:\Users\philipp\Source\Repos\ecnf\RoutePlannerLib\RoutePlannerLib.csproj"
$packageName = "RoutePlannerLib_PS"
$apiKey = "1f4e1024-f0fb-4a2c-8bdc-2ae515fef28d"

#####################################DO NOT EDIT###########################################
Write-Host "path: $path"
Write-Host "libname: $libname"
Write-Host "urlSymbol: $urlSymbol"
Write-Host "apiKey: $apiKey"
Write-Host "starting package creation......"


#Ask for Version
$version = Read-Host "Set assembly version to be published as x.x.x.x"

$pfn = "$packageName."
$pfn += $version
$pfn += ".nupkg"

$sfn = "$packageName."
$sfn += $version
$sfn += ".symbols.nupkg"

Write-Host "PackageName: $pfn SymbolName: $sfn"
#SetApiKey for the staging environment of nuget
.\NuGet.exe SetApiKey $apiKey
#Creating MetaData
.\NuGet.exe spec $path
#Pack the RoutePlannerLib
.\NuGet.exe Pack $path -Version $version
#Pack the RoutePlannerLib Symbols
.\NuGet.exe Pack $path -Symbols -Version $version
#Deploy the RoutePlannerLib
.\NuGet.exe Push $pfn
#Deploy the symbols
.\NuGet.exe Push $sfn
#END
Read-Host -Prompt "Press Enter to exit"