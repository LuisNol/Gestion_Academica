FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080  # 👈 CAMBIA AQUÍ A 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Parcial3_sumaran.csproj", "./"]
RUN dotnet restore "./Parcial3_sumaran.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Parcial3_sumaran.csproj" -c Release -o /app/build
RUN dotnet publish "Parcial3_sumaran.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "Parcial3_sumaran.dll"]
