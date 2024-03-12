FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY ["/src/API/API.csproj", "/src/API/"]
COPY ["/src/Application/Application.csproj", "/src/Application/"]
COPY ["/src/Domain/Domain.csproj", "/src/Domain/"]
COPY ["/src/Infrastructure/Infrastructure.csproj", "/src/Infrastructure/"]

RUN dotnet restore "/src/API/API.csproj"

WORKDIR /src/API

RUN dotnet build "API.csproj" -c Release -o /app/build
RUN dotnet publish "API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

# Create a non-root user
RUN adduser --disabled-password --gecos '' appuser && \
    chown -R appuser /app
USER appuser

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "API.dll"]
