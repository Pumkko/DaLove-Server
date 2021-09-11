#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Dalove-Server/Dalove-Server.csproj", "Dalove-Server/"]
RUN dotnet restore "Dalove-Server/Dalove-Server.csproj"
COPY . .
WORKDIR "/src/Dalove-Server"
RUN dotnet build "Dalove-Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dalove-Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dalove-Server.dll"]