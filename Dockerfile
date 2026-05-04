
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["BuildingStore.Web.csproj", "./"]
RUN dotnet restore "BuildingStore.Web.csproj"

COPY . .

RUN dotnet publish "BuildingStore.Web.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

HEALTHCHECK --interval=30s --timeout=3s \
  CMD curl -f http://localhost:8080/ || exit 1

ENTRYPOINT ["dotnet", "BuildingStore.Web.dll"]