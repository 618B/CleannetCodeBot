FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CleannetCodeBot.Questions/CleannetCodeBot.Questions.csproj", "CleannetCodeBot.Questions/"]
RUN dotnet restore "CleannetCodeBot.Questions/CleannetCodeBot.Questions.csproj"
COPY . .
WORKDIR "/src/CleannetCodeBot.Questions"
RUN dotnet build "CleannetCodeBot.Questions.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleannetCodeBot.Questions.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleannetCodeBot.Questions.dll"]
