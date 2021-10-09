#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/Likr.Comments/Likr.Comments/Likr.Comments.csproj", "src/Services/Likr.Comments/Likr.Comments/"]
COPY ["src/Services/Likr.Comments/Likr.Commands/Likr.Commands.csproj", "src/Services/Likr.Comments/Likr.Commands/"]
RUN dotnet restore "src/Services/Likr.Comments/Likr.Comments/Likr.Comments.csproj"
COPY . .
WORKDIR "/src/src/Services/Likr.Comments/Likr.Comments"
RUN dotnet build "Likr.Comments.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Likr.Comments.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Likr.Comments.dll"]