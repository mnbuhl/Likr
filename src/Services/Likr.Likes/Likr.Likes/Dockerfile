#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Likr.Likes/Likr.Likes/Likr.Likes.csproj", "src/Services/Likr.Likes/Likr.Likes/"]
COPY ["src/Services/Likr.Likes/Likr.Commands/Likr.Commands.csproj", "src/Services/Likr.Likes/Likr.Commands/"]
RUN dotnet restore "src/Services/Likr.Likes/Likr.Likes/Likr.Likes.csproj"
COPY . .
WORKDIR "/src/src/Services/Likr.Likes/Likr.Likes"
RUN dotnet build "Likr.Likes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Likr.Likes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Likr.Likes.dll"]