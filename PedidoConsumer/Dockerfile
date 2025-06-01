FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY PedidoConsumer.csproj .
RUN dotnet restore PedidoConsumer.csproj
COPY . .
RUN dotnet publish PedidoConsumer.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PedidoConsumer.dll"]
