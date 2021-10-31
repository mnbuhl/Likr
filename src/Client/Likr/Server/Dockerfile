#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:alpine AS node
WORKDIR /src
COPY . .
RUN npm install
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Client/Likr/Server/Likr.Server.csproj", "src/Client/Likr/Server/"]
COPY ["src/Client/Likr/Client/Likr.Client.csproj", "src/Client/Likr/Client/"]
RUN dotnet restore "src/Client/Likr/Server/Likr.Server.csproj"
COPY . .
WORKDIR "/src/src/Client/Likr/Server"
RUN dotnet build "Likr.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Likr.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Likr.Server.dll"]
