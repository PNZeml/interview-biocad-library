FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Interview.Biocad.Library/Interview.Biocad.Library.csproj", "Interview.Biocad.Library/"]
RUN dotnet restore "Interview.Biocad.Library/Interview.Biocad.Library.csproj"
COPY . .
WORKDIR "/src/Interview.Biocad.Library"
RUN dotnet build "Interview.Biocad.Library.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Interview.Biocad.Library.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Interview.Biocad.Library.dll"]
