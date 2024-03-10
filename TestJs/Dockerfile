FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN ls -la
RUN dotnet restore "TestJs.csproj"
RUN dotnet build "TestJs.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM --platform=linux/amd64 build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TestJs.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM --platform=linux/amd64 base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestJs.dll"]
