FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/MadameCoco.Customer/MadameCoco.Customer.csproj", "src/Services/MadameCoco.Customer/"]
COPY ["src/Core/MadameCoco.Shared/MadameCoco.Shared.csproj", "src/Core/MadameCoco.Shared/"]
RUN dotnet restore "src/Services/MadameCoco.Customer/MadameCoco.Customer.csproj"
COPY . .
WORKDIR "/src/src/Services/MadameCoco.Customer"
RUN dotnet build "MadameCoco.Customer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MadameCoco.Customer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MadameCoco.Customer.dll"]
