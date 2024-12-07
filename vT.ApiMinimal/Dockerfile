# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 12086
ENV ASPNETCORE_URLS=http://+:12086

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Correct COPY paths for project files
COPY ["vT.ApiMinimal/vT.ApiMinimal.csproj", "vT.ApiMinimal/"]
COPY ["vT.ApiDomains/vT.ApiDomains.csproj", "vT.ApiDomains/"]

# Restore dependencies
RUN dotnet restore "vT.ApiMinimal/vT.ApiMinimal.csproj"

# Copy all files and build the project
COPY . .
WORKDIR "/src/vT.ApiMinimal"
RUN dotnet build "vT.ApiMinimal.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "vT.ApiMinimal.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vT.ApiMinimal.dll"]
