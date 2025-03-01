# Stage 1: Build the .NET Console App
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY Receiver.csproj .
RUN dotnet restore

# Copy the source code and build the project
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime (Smaller Image)
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Define the entry point for the application
ENTRYPOINT ["dotnet", "Receiver.dll"]