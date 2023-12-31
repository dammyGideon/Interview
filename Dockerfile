# Get sdk
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

#Copy the csproj file and restore any dependencies (via NUGET)
COPY *.csproj ./
RUN dotnet restore

#Copy the project files and build our release

COPY . ./
RUN dotnet publish -c Release -o out
#Generate runtime image

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "interview.dll"]

