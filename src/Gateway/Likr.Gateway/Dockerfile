#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Gateway/Likr.Gateway/Likr.Gateway.csproj", "src/Gateway/Likr.Gateway/"]
RUN dotnet restore "src/Gateway/Likr.Gateway/Likr.Gateway.csproj"
COPY . .
WORKDIR "/src/src/Gateway/Likr.Gateway"
RUN dotnet build "Likr.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Likr.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Likr.Gateway.dll"]
