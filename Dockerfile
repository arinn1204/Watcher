FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

COPY src/main/*/*.csproj ./
RUN dotnet restore

COPY src/main/* ./
RUN dotnet build -c Release -o out

FROM mcr.microsoft.com/dotnet/core/sdk:3.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Watcher.Runner.dll"]