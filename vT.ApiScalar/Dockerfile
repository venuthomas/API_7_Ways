FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 12087
ENV ASPNETCORE_URLS=http://+:12087

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["vT.ApiScalar/vT.ApiScalar.csproj", "vT.ApiScalar/"]
COPY ["vT.ApiDomains/vT.ApiDomains.csproj", "vT.ApiDomains/"]
RUN dotnet restore "vT.ApiScalar/vT.ApiScalar.csproj"
COPY . .
WORKDIR "/src/vT.ApiScalar"
RUN dotnet build "vT.ApiScalar.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "vT.ApiScalar.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vT.ApiScalar.dll"]
