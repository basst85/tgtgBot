# tgtgBot
ToGoodToGo IFTTT bot written in C# on .NET Core platform

[![Travis-ci Build Status](https://travis-ci.org/basst85/tgtgBot.svg?branch=master)](https://travis-ci.org/basst85/tgtgBot)
[![CircleCI Build Status](https://circleci.com/gh/basst85/tgtgBot/tree/master.svg?style=svg)](https://circleci.com/gh/basst85/tgtgBot/tree/master)

#### Work in progress. It only returns nearby shops/items as JSON
______

## Installation and usage
- Set your ToGoodToGo username and password in ``` src\appsettings.json```.
- Set your location (latitude and longitude) in ``src\appsettings.json``.
- Set the search radius (in KM) in ``src\appsettings.json``.

Build the application with dotnet:
```bash
dotnet restore .\src\tgtgBot.csproj
dotnet publish .\src\tgtgBot.csproj -c release -o .\release --self-contained false --no-restore
```

Run the application:
```bash
dotnet .\release\tgtgBot.dll
```

You can also use the Dockerfile to build and run it via Docker:
```bash
docker build --pull -t tgtgbot .
docker run --rm tgtgbot 
```
