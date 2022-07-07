#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/OnlineStore.CatalogService.WebApi/OnlineStore.CatalogService.WebApi.csproj", "src/OnlineStore.CatalogService.WebApi/"]
RUN dotnet restore "src/OnlineStore.CatalogService.WebApi/OnlineStore.CatalogService.WebApi.csproj"
COPY . .
WORKDIR "/src/src/OnlineStore.CatalogService.WebApi"
RUN dotnet build "OnlineStore.CatalogService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineStore.CatalogService.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineStore.CatalogService.WebApi.dll"]