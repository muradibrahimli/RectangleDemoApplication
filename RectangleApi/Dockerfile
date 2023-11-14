# Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RectangleApi/RectangleApi.csproj", "RectangleApi/"]
RUN dotnet restore "RectangleApi/RectangleApi.csproj"
COPY . .
WORKDIR "/src/RectangleApi"
RUN dotnet build "RectangleApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RectangleApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RectangleApi.dll"]
