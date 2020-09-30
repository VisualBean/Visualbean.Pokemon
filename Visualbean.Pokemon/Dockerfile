#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Visualbean.Pokemon/Visualbean.Pokemon.csproj", "Visualbean.Pokemon/"]
RUN dotnet restore "Visualbean.Pokemon/Visualbean.Pokemon.csproj"
COPY . .
WORKDIR "/src/Visualbean.Pokemon"
RUN dotnet build "Visualbean.Pokemon.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Visualbean.Pokemon.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Visualbean.Pokemon.dll"]