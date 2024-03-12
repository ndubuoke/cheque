FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0.201-nanoserver-1809 AS build
WORKDIR /src

COPY ["/src/API/API.csproj", "/src/API/"]
COPY ["/src/Application/Application.csproj", "/src/Application/"]
COPY ["/src/Domain/Domain.csproj", "/src/Domain/"]
COPY ["/src/Infrastructure/Infrastructure.csproj", "/src/Infrastructure/"]

RUN dotnet restore "/src/API/API.csproj"

COPY . .

WORKDIR /src/API
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "API.dll"]