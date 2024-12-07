FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 12081
ENV ASPNETCORE_URLS=http://+:12081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["vT.Api_gPRC/vT.Api_gPRC.csproj", "vT.Api_gPRC/"]
COPY ["vT.ApiDomains/vT.ApiDomains.csproj", "vT.ApiDomains/"]
RUN dotnet restore "vT.Api_gPRC/vT.Api_gPRC.csproj"
COPY . .
WORKDIR "/src/vT.Api_gPRC"
RUN dotnet build "vT.Api_gPRC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "vT.Api_gPRC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vT.Api_gPRC.dll"]
