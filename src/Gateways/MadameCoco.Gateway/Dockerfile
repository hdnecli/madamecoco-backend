FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Gateways/MadameCoco.Gateway/MadameCoco.Gateway.csproj", "src/Gateways/MadameCoco.Gateway/"]
RUN dotnet restore "src/Gateways/MadameCoco.Gateway/MadameCoco.Gateway.csproj"
COPY . .
WORKDIR "/src/src/Gateways/MadameCoco.Gateway"
RUN dotnet build "MadameCoco.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MadameCoco.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MadameCoco.Gateway.dll"]
