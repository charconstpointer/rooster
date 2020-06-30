FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5010
EXPOSE 5010

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /src
COPY [".", "app/"]
RUN dotnet restore "app/Rooster.Worker/Rooster.Worker.csproj"
COPY . .
WORKDIR "/src/Rooster.Worker"
RUN dotnet build "Rooster.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rooster.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN sed 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf > /etc/ssl/openssl.cnf.changed && mv /etc/ssl/openssl.cnf.changed /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet", "Rooster.Worker.dll"]
