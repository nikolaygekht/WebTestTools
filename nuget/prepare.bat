msbuild ../webviewtest.sln /p:Config=Release
msbuild nuget.proj -t:Prepare
msbuild nuget.proj -t:NuSpec,NuPack
