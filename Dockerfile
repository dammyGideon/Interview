# Stage 1: Build the .NET application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY interview.csproj .
RUN dotnet restore

COPY . .
RUN dotnet build -c Release -o /app/build

# Stage 2: Publish the .NET application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Stage 3: Create the final image
FROM mcr.microsoft.com/dotnet/runtime:7.0 AS final

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "interview.dll"]
