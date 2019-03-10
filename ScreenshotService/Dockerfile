FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 8080

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ScreenshotService.csproj", "ScreenshotService/"]
RUN dotnet restore "ScreenshotService/ScreenshotService.csproj"
COPY appsettings.json ScreenshotService/
COPY src ScreenshotService
WORKDIR "/src/ScreenshotService"
RUN dotnet build "ScreenshotService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ScreenshotService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ScreenshotService.dll"]