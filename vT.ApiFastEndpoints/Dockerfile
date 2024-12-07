FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 12084
ENV ASPNETCORE_URLS=http://+:12084

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["vT.ApiFastEndpoints/vT.ApiFastEndpoints.csproj", "vT.ApiFastEndpoints/"]
COPY ["vT.ApiDomains/vT.ApiDomains.csproj", "vT.ApiDomains/"]
RUN dotnet restore "vT.ApiFastEndpoints/vT.ApiFastEndpoints.csproj"
COPY . .
WORKDIR "/src/vT.ApiFastEndpoints"
RUN dotnet build "vT.ApiFastEndpoints.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "vT.ApiFastEndpoints.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vT.ApiFastEndpoints.dll"]