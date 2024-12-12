FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FeedbackService/FeedbackService.csproj", "FeedbackService/"]
RUN dotnet restore "FeedbackService/FeedbackService.csproj"
COPY . .
WORKDIR "/src/FeedbackService"
RUN dotnet build "FeedbackService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FeedbackService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false