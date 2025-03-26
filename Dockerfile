FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Contatos.CreateProducer/Contatos.CreateProducer.csproj", "Contatos.CreateProducer/"]
RUN dotnet restore "./Contatos.CreateProducer/Contatos.CreateProducer.csproj"
COPY . .
WORKDIR "/src/Contatos.CreateProducer"
RUN dotnet build "./Contatos.CreateProducer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Contatos.CreateProducer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contatos.CreateProducer.dll"]