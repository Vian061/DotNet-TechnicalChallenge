# ==============================
# BUILD (Debug / Dev)
# ==============================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY *.sln ./
COPY HealthCare.API/*.csproj ./HealthCare.API/
COPY HealthCare.Application/*.csproj ./HealthCare.Application/
COPY HealthCare.Domain/*.csproj ./HealthCare.Domain/
COPY HealthCare.Infrastructure/*.csproj ./HealthCare.Infrastructure/

RUN dotnet restore "HealthCare.API/HealthCare.API.csproj"

COPY . .

RUN dotnet publish "HealthCare.API/HealthCare.API.csproj" \
    -c Debug \
    -o /app/publish \
    /p:UseAppHost=false

# ==============================
# RUNTIME
# ==============================
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

EXPOSE 8080

ENTRYPOINT ["dotnet", "HealthCare.API.dll"]
