FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CleannetCodeBot.Statistics/CleannetCodeBot.Statistics.csproj", "CleannetCodeBot.Statistics/"]
RUN dotnet restore "CleannetCodeBot.Statistics/CleannetCodeBot.Statistics.csproj"
COPY . .
WORKDIR "/src/CleannetCodeBot.Statistics"
RUN dotnet build "CleannetCodeBot.Statistics.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleannetCodeBot.Statistics.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleannetCodeBot.Statistics.dll"]
