nuget restore ../webviewtest.sln
msbuild ../webviewtest.sln /t:Clean /p:Configuration=Release
msbuild ../webviewtest.sln /p:Configuration=Release
msbuild nuget.proj -t:Prepare
msbuild nuget.proj -t:NuSpec,NuPack
