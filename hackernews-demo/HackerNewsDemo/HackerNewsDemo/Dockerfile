#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HackerNewsDemo/HackerNewsDemo.csproj", "HackerNewsDemo/"]
RUN dotnet restore "HackerNewsDemo/HackerNewsDemo.csproj"
COPY . .
WORKDIR "/src/HackerNewsDemo"
RUN dotnet build "HackerNewsDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HackerNewsDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HackerNewsDemo.dll"]