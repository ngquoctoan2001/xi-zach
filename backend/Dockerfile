# syntax=docker/dockerfile:1

# ---- build: restore and publish the API ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Directory.Build.props Directory.Packages.props ./
COPY src/XiDach.Rules/XiDach.Rules.csproj src/XiDach.Rules/
COPY src/XiDach.Game/XiDach.Game.csproj src/XiDach.Game/
COPY src/XiDach.Infrastructure/XiDach.Infrastructure.csproj src/XiDach.Infrastructure/
COPY src/XiDach.Api/XiDach.Api.csproj src/XiDach.Api/
RUN dotnet restore src/XiDach.Api/XiDach.Api.csproj
COPY src/ src/
RUN dotnet publish src/XiDach.Api/XiDach.Api.csproj -c Release -o /app/publish --no-restore -p:UseAppHost=false

# ---- api: runtime image, also used by the one-shot `migrate` container ----
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS api
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=8080
COPY --from=build /app/publish .
USER $APP_UID
EXPOSE 8080
ENTRYPOINT ["dotnet", "XiDach.Api.dll"]
