FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY . .

RUN dotnet restore "Presentation/SpiceRack.WebApi/SpiceRack.WebApi.csproj"
COPY . .
WORKDIR "/src/Presentation/SpiceRack.WebApi"
RUN dotnet build "SpiceRack.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/Presentation/SpiceRack.WebApi"
RUN dotnet publish "SpiceRack.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "SpiceRack.WebApi.dll"]