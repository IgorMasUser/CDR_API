FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CDR_API/CDR_API.csproj", "CDR_API/"]
RUN dotnet restore "CDR_API/CDR_API.csproj"
COPY . .
WORKDIR "/src/CDR_API"
RUN dotnet build "CDR_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CDR_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CDR_API.dll"]