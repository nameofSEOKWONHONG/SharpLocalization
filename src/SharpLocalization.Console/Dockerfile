FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SharpLocalization.Console/SharpLocalization.Console.csproj", "SharpLocalization.Console/"]
RUN dotnet restore "SharpLocalization.Console/SharpLocalization.Console.csproj"
COPY . .
WORKDIR "/src/SharpLocalization.Console"
RUN dotnet build "SharpLocalization.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SharpLocalization.Console.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SharpLocalization.Console.dll"]
