# Base image with the .NET Core SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

# Copy the .csproj and restore dependencies
COPY ["FlightOptimizerManager/FlightOptimizerManager.csproj", "FlightOptimizerManager/"]
RUN dotnet restore "./FlightOptimizerManager/FlightOptimizerManager.csproj"

# Copy the entire project and build
COPY . ./
RUN dotnet publish -c Release -o out

# Publish the application
RUN dotnet publish --configuration Release --no-build --output /app/publish

# Final image with only the runtime
FROM mcr.microsoft.com/dotnet/runtime:6.0

WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "FlightOptimizerManager.dll"]