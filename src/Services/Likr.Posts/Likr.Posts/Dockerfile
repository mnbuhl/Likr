#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Likr.Posts/Likr.Posts/Likr.Posts.csproj", "src/Services/Likr.Posts/Likr.Posts/"]
COPY ["src/Services/Likr.Posts/Likr.Commands/Likr.Commands.csproj", "src/Services/Likr.Posts/Likr.Commands/"]
RUN dotnet restore "src/Services/Likr.Posts/Likr.Posts/Likr.Posts.csproj"
COPY . .
WORKDIR "/src/src/Services/Likr.Posts/Likr.Posts"
RUN dotnet build "Likr.Posts.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Likr.Posts.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Likr.Posts.dll"]
