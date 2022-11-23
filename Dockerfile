# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY ./*.csproj .
RUN dotnet restore -r linux-musl-x64

# copy everything else and build app
COPY . .
RUN dotnet publish -c Release -o /app -r linux-musl-x64 --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
COPY db/db.sqlite ./db/
COPY --from=build /app .
ENTRYPOINT ["dotnet", "librawry.dll"]
