FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["The Social Network/The Social Network.csproj", "The Social Network/"]
RUN dotnet restore "The Social Network/The Social Network.csproj"
COPY . .
WORKDIR "/src/The Social Network"
RUN dotnet build "The Social Network.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "The Social Network.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "The Social Network.dll"]
