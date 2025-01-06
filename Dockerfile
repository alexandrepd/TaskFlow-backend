# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and restore dependencies
COPY *.sln .
COPY TaskFlow.API/TaskFlow.API.csproj TaskFlow.API/
COPY TaskFlow.Application/TaskFlow.Application.csproj TaskFlow.Application/
COPY TaskFlow.Domain/TaskFlow.Domain.csproj TaskFlow.Domain/
COPY TaskFlow.Infrastructure/TaskFlow.Infrastructure.csproj TaskFlow.Infrastructure/
RUN dotnet restore

# Build the app
COPY . .
RUN dotnet publish TaskFlow.API/TaskFlow.API.csproj -c Release -o /app/out

# Use a lightweight runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Definir variáveis de ambiente
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5000

# Expose the API port
# EXPOSE 5000
# EXPOSE 5001
ENTRYPOINT ["dotnet", "TaskFlow.API.dll"]