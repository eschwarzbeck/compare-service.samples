param(
    [ValidateScript({[System.IO.Path]::IsPathRooted($_)})]
    [string]$WebsitePath,
    [ValidateScript({[System.IO.Path]::IsPathRooted($_)})]
    [string]$SamplePath
)


function UpdateLineInFile($filePath, $Lines,$pattern,$updatedline){
    $matches=Select-String -Path $filePath -Pattern $pattern | Select-Object LineNumber
    # Replace
    ForEach ($match in $matches ) { 
        $Lines[$match.LineNumber-1] =$updatedline
    }
}

function ReplaceRegex($filePath,$Lines,$pattern,$replacementString){

    $matches=Select-String -Path $filePath -Pattern $pattern | Select-Object LineNumber

    ForEach ($match in $matches ) { 
        $Lines[$match.LineNumber-1]= $Lines[$match.LineNumber-1] -replace $pattern,$replacementString
    }

}


function UpdateParameter($filePath,$oldValue, $newValue){

    #Read file
    $fileLines = Get-Content -Path $filePath
    #Update parameter value
    ReplaceRegex -filePath:$filePath -Lines:$fileLines -pattern:$oldValue -replacementString:$newValue
    #Write file
    Set-Content -Path $filePath -Value $fileLines
}

#$rootDir
$rootDir= Get-Location | select -ExpandProperty Path

if(-Not $WebsitePath){
    $WebsitePath="$rootDir\Websites"
}

if(-Not $SamplePath){
    $SamplePath="$rootDir\Samples"
}


# Create websites folders
#TODO DELETE IF EXISTS
mkdir "$WebsitePath\Basic"
mkdir "$WebsitePath\Advanced"
mkdir "$WebsitePath\ConfigPage"
mkdir "$WebsitePath\WebClient"
mkdir "$WebsitePath\WebAdmin"



### Create websites
$windir="C:\Windows"

### Create Apppool 
& $windir\system32\inetsrv\appcmd.exe add apppool /name:Samples /managedRuntimeVersion:v4.0 /managedPipelineMode:Integrated

### Create website: Basic sample			8085
& $windir\system32\inetsrv\appcmd.exe add sites /name:"Basic sample" /id:300 /bindings:"http://*:8085" /physicalPath:"$WebsitePath\Basic"
& $windir\system32\inetsrv\appcmd.exe set app "Basic sample/" /applicationPool:"Samples"

### Create website: Advanced sample			8086
& $windir\system32\inetsrv\appcmd.exe add sites /name:"Advanced sample" /id:301 /bindings:"http://*:8086" /physicalPath:"$WebsitePath\Advanced"
& $windir\system32\inetsrv\appcmd.exe set app "Advanced sample/" /applicationPool:"Samples"

### Create website: Config Page sample		8087
& $windir\system32\inetsrv\appcmd.exe add sites /name:"Config Page sample" /id:302 /bindings:"http://*:8087" /physicalPath:"$WebsitePath\ConfigPage"
& $windir\system32\inetsrv\appcmd.exe set app "Config Page sample/" /applicationPool:"Samples"

### Create website: WebAdmin				8088
& $windir\system32\inetsrv\appcmd.exe add sites /name:"WebAdmin" /id:304 /bindings:"http://*:8088" /physicalPath:"$WebsitePath\WebAdmin"
& $windir\system32\inetsrv\appcmd.exe set app "WebAdmin/" /applicationPool:"Samples"
 # Disable anonymous authentication and enable Windows Authentication
& $windir\system32\inetsrv\appcmd.exe set config "WebAdmin/" -section:system.webServer/security/authentication/anonymousAuthentication /enabled:"False" /commit:apphost
& $windir\system32\inetsrv\appcmd.exe set config "WebAdmin/" -section:system.webServer/security/authentication/windowsAuthentication /enabled:"True" /commit:apphost

### Create website: WebClient				80 (or 8084)
& $windir\system32\inetsrv\appcmd.exe add sites /name:"WebClient" /id:303 /bindings:"http://*:8084" /physicalPath:"$WebsitePath\WebClient"
& $windir\system32\inetsrv\appcmd.exe set app "WebClient/" /applicationPool:"Samples"
 # Disable anonymous authentication and enable Windows Authentication
& $windir\system32\inetsrv\appcmd.exe set config "WebClient/" -section:system.webServer/security/authentication/anonymousAuthentication /enabled:"False" /commit:apphost
& $windir\system32\inetsrv\appcmd.exe set config "WebClient/" -section:system.webServer/security/authentication/windowsAuthentication /enabled:"True" /commit:apphost


#appcmd set config "MySite/MyApp" -section:system.webServer/security/authentication/anonymousAuthentication /enabled:"True" /commit:apphost

# Enable Windows Authentication (True to Enable, False to Disable)
# appcmd.exe set config "MySite/MyApp" -section:system.webServer/security/authentication/windowsAuthentication /enabled:"True" /commit:apphost

### Update .SetParameters file and deploy samples
#Basic sample
$setParametersFile="$SamplePath\BasicWebSample\Workshare.Samples.BasicWebSample.SetParameters.xml"
UpdateParameter -filePath:$setParametersFile -oldValue:"Default Web Site/BasicWebSample" -newValue:"Basic sample"
& $SamplePath\BasicWebSample\Workshare.Samples.BasicWebSample.deploy.cmd /Y

# Advanced sample
$setParametersFile="$SamplePath\AdvWebSample\Workshare.Samples.AdvWebSample.SetParameters.xml"
UpdateParameter -filePath:$setParametersFile -oldValue:"Default Web Site/AdvWebSample" -newValue:"Advanced sample"
& $SamplePath\AdvWebSample\Workshare.Samples.AdvWebSample.deploy.cmd /Y

# Config Page sample
$setParametersFile="$SamplePath\ConfigPageSample\Workshare.Samples.ConfigPageSample.SetParameters.xml"
UpdateParameter -filePath:$setParametersFile -oldValue:"Default Web Site" -newValue:"Config Page sample"
& $SamplePath\ConfigPageSample\Workshare.Samples.ConfigPageSample.deploy.cmd /Y

# WebAdmin
$setParametersFile="$SamplePath\WebAdmin\CompareServer.WebAdmin.SetParameters.xml"
UpdateParameter -filePath:$setParametersFile -oldValue:"Default Web Site" -newValue:"WebAdmin"
& $SamplePath\WebAdmin\CompareServer.WebAdmin.deploy.cmd /Y

# WebClient
$setParametersFile="$SamplePath\WebClient\CompareServer.WebClient.SetParameters.xml"
UpdateParameter -filePath:$setParametersFile -oldValue:"Default Web Site" -newValue:"WebClient"
& $SamplePath\WebClient\CompareServer.WebClient.deploy.cmd /Y






