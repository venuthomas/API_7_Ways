FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 12082
ENV ASPNETCORE_URLS=http://+:12082

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["vT.ApiCarter/vT.ApiCarter.csproj", "vT.ApiCarter/"]
COPY ["vT.ApiDomains/vT.ApiDomains.csproj", "vT.ApiDomains/"]
RUN dotnet restore "vT.ApiCarter/vT.ApiCarter.csproj"
COPY . .
WORKDIR "/src/vT.ApiCarter"
RUN dotnet build "vT.ApiCarter.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "vT.ApiCarter.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vT.ApiCarter.dll"]
