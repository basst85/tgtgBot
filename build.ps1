dotnet restore .\src\tgtgBot.csproj
dotnet publish .\src\tgtgBot.csproj -c release -o .\release --self-contained false --no-restore