FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ArcDemo2024.Hotel.WebApi/ArcDemo2024.Hotel.WebApi.csproj", "src/ArcDemo2024.Hotel.WebApi/"]
RUN dotnet restore "src/ArcDemo2024.Hotel.WebApi/ArcDemo2024.Hotel.WebApi.csproj"
COPY . .
WORKDIR "/src/src/ArcDemo2024.Hotel.WebApi"
RUN dotnet build "ArcDemo2024.Hotel.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ArcDemo2024.Hotel.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArcDemo2024.Hotel.WebApi.dll"]
