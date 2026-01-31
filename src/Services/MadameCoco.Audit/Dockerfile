FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/MadameCoco.Audit/MadameCoco.Audit.csproj", "src/Services/MadameCoco.Audit/"]
COPY ["src/Core/MadameCoco.Shared/MadameCoco.Shared.csproj", "src/Core/MadameCoco.Shared/"]
RUN dotnet restore "src/Services/MadameCoco.Audit/MadameCoco.Audit.csproj"
COPY . .
WORKDIR "/src/src/Services/MadameCoco.Audit"
RUN dotnet build "MadameCoco.Audit.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MadameCoco.Audit.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MadameCoco.Audit.dll"]
