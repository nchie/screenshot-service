# BASE
FROM mcr.microsoft.com/dotnet/core/runtime:2.2-bionic AS base
WORKDIR /app
RUN apt-get update  \
 && apt-get install -y chromium-browser 
# RUN adduser worker
RUN useradd -ms /bin/bash worker

# BUILD
FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ScreenshotWorker.csproj ScreenshotWorker/
RUN dotnet restore "ScreenshotWorker/ScreenshotWorker.csproj"
COPY appsettings.json ScreenshotWorker/
COPY src ScreenshotWorker/
WORKDIR /src/ScreenshotWorker
RUN dotnet build "ScreenshotWorker.csproj" -c Release -o /app

# PUBLISH
FROM build AS publish
RUN dotnet publish "ScreenshotWorker.csproj" -c Release -o /app

# FINAL
FROM base AS final
ENV PUPPETEER_EXECUTABLE_PATH=/usr/bin/chromium-browser
WORKDIR /app
# RUN chown worker /app
# USER worker
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ScreenshotWorker.dll"]