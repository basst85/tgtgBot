dotnet restore .\tgtgBot\tgtgBot.csproj
dotnet publish .\tgtgBot\tgtgBot.csproj -c release -o .\app --self-contained false --no-restore