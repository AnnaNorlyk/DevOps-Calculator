FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY Calculator/*.csproj ./Calculator/
COPY API/*.csproj ./API/
COPY IntegrationTest/*.csproj ./IntegrationTest/
RUN dotnet restore ./IntegrationTest/IntegrationTest.csproj
COPY . ./
RUN dotnet build ./IntegrationTest/IntegrationTest.csproj --no-restore -c Release

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS test
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "test", "IntegrationTest/IntegrationTest.csproj", "--no-build", "--logger:console"]
