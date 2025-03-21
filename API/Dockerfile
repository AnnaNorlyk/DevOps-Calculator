# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5153

# Set environment
ENV ASPNETCORE_URLS=http://+:5153

# Create a non-root user for better security
RUN useradd -m appuser
USER appuser

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src


COPY API/API.csproj API/
COPY Calculator/Calculator.csproj Calculator/
RUN dotnet restore API/API.csproj


COPY API/. API/
COPY Calculator/. Calculator/

WORKDIR /src/API

RUN dotnet build "API.csproj" -c "$configuration" -o /app/build

# Publish stage
FROM build AS publish
ARG configuration=Release

RUN dotnet publish \
    "API.csproj" \
    -c "$configuration" \
    -r linux-x64 \
    --self-contained=false \
    -o /app/publish \
    /p:UseAppHost=false

# Final runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

USER appuser

ENTRYPOINT ["dotnet", "API.dll"]