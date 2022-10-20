FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DynamoWebApi/DynamoWebApi.csproj", "DynamoWebApi/"]
RUN dotnet restore "DynamoWebApi/DynamoWebApi.csproj"
COPY . .
WORKDIR "/src/DynamoWebApi"
RUN dotnet build "DynamoWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DynamoWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DynamoWebApi.dll"]
