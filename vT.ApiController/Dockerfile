FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 12083
ENV ASPNETCORE_URLS=http://+:12083

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["vT.ApiController/vT.ApiController.csproj", "vT.ApiController/"]
COPY ["vT.ApiDomains/vT.ApiDomains.csproj", "vT.ApiDomains/"]
RUN dotnet restore "vT.ApiController/vT.ApiController.csproj"
COPY . .
WORKDIR "/src/vT.ApiController"
RUN dotnet build "vT.ApiController.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "vT.ApiController.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vT.ApiController.dll"]
