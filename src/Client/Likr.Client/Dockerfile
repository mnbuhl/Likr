#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Client/Likr.Client/Likr.Client.csproj", "src/Client/Likr.Client/"]
RUN dotnet restore "src/Client/Likr.Client/Likr.Client.csproj"
COPY . .
WORKDIR "/src/src/Client/Likr.Client"
RUN dotnet build "Likr.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Likr.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
FROM nginx:alpine
EXPOSE 80
COPY --from=publish /app/publish/wwwroot /usr/share/nginx/html
COPY ["src/Client/Likr.Client/nginx.conf", "/etc/nginx/nginx.conf"]
CMD ["nginx", "-g", "daemon off;"]