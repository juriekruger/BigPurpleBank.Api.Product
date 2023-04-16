FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BigPurpleBank.Api.Product.Web/BigPurpleBank.Api.Product.Web.csproj", "BigPurpleBank.Api.Product.Web/"]
RUN dotnet restore "BigPurpleBank.Api.Product.Web/BigPurpleBank.Api.Product.Web.csproj"
COPY . .
WORKDIR "/src/BigPurpleBank.Api.Product.Web"
RUN dotnet build "BigPurpleBank.Api.Product.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BigPurpleBank.Api.Product.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BigPurpleBank.Api.Product.Web.dll"]
