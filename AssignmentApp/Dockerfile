# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["AssignmentApp.csproj", "./"]
RUN dotnet restore "AssignmentApp.csproj"

COPY . ./
RUN dotnet publish "AssignmentApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Optional default environment variable
ENV ASSIGNMENT_APP_NAME="AssignmentUser"

ENTRYPOINT ["dotnet", "AssignmentApp.dll"]
