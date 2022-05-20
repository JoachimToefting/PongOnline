#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PongOnline/PongOnline.csproj", "PongOnline/"]
RUN dotnet restore "PongOnline/PongOnline.csproj"
COPY . .
WORKDIR "/src/PongOnline"
RUN dotnet build "PongOnline.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PongOnline.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PongOnline.dll"]