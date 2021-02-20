FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PAthCase.IdentityServer.csproj", "./"]
RUN dotnet restore "PAthCase.IdentityServer.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PAthCase.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PAthCase.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PAthCase.IdentityServer.dll"]
