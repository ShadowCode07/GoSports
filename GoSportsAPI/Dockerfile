# 1. Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore dependencies
COPY ["GoSportsAPI.csproj", "./"]
RUN dotnet restore "GoSportsAPI.csproj"

# Copy everything and build
COPY . .
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 8080 for Railway
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "GoSportsAPI.dll"]