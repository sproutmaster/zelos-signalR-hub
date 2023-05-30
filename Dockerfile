FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["zelos-hub.csproj", "."]
RUN dotnet restore "./zelos-hub.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "zelos-hub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "zelos-hub.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "zelos-hub.dll"]