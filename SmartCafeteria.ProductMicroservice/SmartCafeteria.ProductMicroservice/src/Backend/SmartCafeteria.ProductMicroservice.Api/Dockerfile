FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files with correct paths
COPY ["src/Backend/SmartCafeteria.ProductMicroservice.Api/SmartCafeteria.ProductMicroservice.Api.csproj", "SmartCafeteria.ProductMicroservice.Api/"]
COPY ["src/Backend/SmartCafeteria.ProductMicroservice.DataAccess/SmartCafeteria.ProductMicroservice.DataAccess.csproj", "SmartCafeteria.ProductMicroservice.DataAccess/"]

# Restore dependencies
RUN dotnet restore "SmartCafeteria.ProductMicroservice.Api/SmartCafeteria.ProductMicroservice.Api.csproj"

# Copy all source files
COPY . .

# Set the working directory to the API project and build it
WORKDIR "/src/src/Backend/SmartCafeteria.ProductMicroservice.Api"
RUN dotnet build "SmartCafeteria.ProductMicroservice.Api.csproj" -c Release

# Publish the application
FROM build AS publish
RUN dotnet publish "SmartCafeteria.ProductMicroservice.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartCafeteria.ProductMicroservice.Api.dll"]

