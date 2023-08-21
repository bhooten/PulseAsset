FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PulseAsset/PulseAsset.csproj", "PulseAsset/"]
RUN dotnet restore "PulseAsset/PulseAsset.csproj"
COPY . .
WORKDIR "/src/PulseAsset"
RUN dotnet build "PulseAsset.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PulseAsset.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PulseAsset.dll"]
