FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ScreenshotService/ScreenshotService.csproj", "ScreenshotService/"]
RUN dotnet restore "ScreenshotService/ScreenshotService.csproj"
COPY . .
WORKDIR "/src/ScreenshotService"
RUN dotnet build "ScreenshotService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ScreenshotService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ScreenshotService.dll"]