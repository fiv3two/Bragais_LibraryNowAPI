FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /src
COPY . .
RUN dotnet restore "Bragais_LibraryNowAPI.csproj"
RUN dotnet publish "Bragais_LibraryNowAPI.csproj" -c Release -o /app/out

FROM base AS final
WORKDIR /app
COPY --fropm=build /app/out .
ENTRYPOINT ["dotnet", "BragaisNowAPI.dll"]
