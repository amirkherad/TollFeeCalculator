FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TollFeeCalculator.Presentation/TollFeeCalculator.Presentation.csproj", "TollFeeCalculator.Presentation/"]
RUN dotnet restore "TollFeeCalculator.Presentation/TollFeeCalculator.Presentation.csproj"
COPY . .
WORKDIR "/src/TollFeeCalculator.Presentation"
RUN dotnet build "TollFeeCalculator.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TollFeeCalculator.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TollFeeCalculator.Presentation.dll"]
