FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build

RUN curl --silent --location https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --yes nodejs

WORKDIR /src
COPY . .
RUN echo $(ls)

RUN dotnet restore "./PathCase.MVC/PathCase.MVC.csproj"

RUN dotnet publish "PathCase.MVC/PathCase.MVC.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim

# Expose port 80 to your local machine so you can access the app.
EXPOSE 80
EXPOSE 443

# Copy the published app to this new runtime-only container.
COPY --from=build /app/publish .

# To run the app, run `dotnet sample-app.dll`, which we just copied over.
ENTRYPOINT ["dotnet", "PathCase.MVC.dll"]