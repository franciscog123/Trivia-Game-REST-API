FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TriviaGame.Api/TriviaGame.Api.csproj", "TriviaGame.Api/"]
COPY ["TriviaGame.DataAccess/TriviaGame.DataAccess.csproj", "TriviaGame.DataAccess/"]
COPY ["TriviaGame.Library/TriviaGame.Library.csproj", "TriviaGame.Library/"]
RUN dotnet restore "TriviaGame.Api/TriviaGame.Api.csproj"
COPY . .
WORKDIR "/src/TriviaGame.Api"
RUN dotnet build "TriviaGame.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TriviaGame.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TriviaGame.Api.dll"]