# BASE
FROM mcr.microsoft.com/dotnet/core/runtime:2.1-bionic AS base
WORKDIR /app
RUN apt-get update  \
 && apt-get install -y chromium-browser 
# RUN adduser worker
RUN useradd -ms /bin/bash worker
#ENV PUPPETEER_EXECUTABLE_PATH 
EXPOSE 80

# BUILD
FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src


COPY ["ScreenshotService.csproj", "ScreenshotService/"]
RUN dotnet restore "ScreenshotService/ScreenshotService.csproj"
COPY . .
WORKDIR "/src/ScreenshotService"
COPY . .
RUN dotnet build "ScreenshotService.csproj" -c Release -o /app

# PUBLISH
FROM build AS publish
RUN dotnet publish "ScreenshotService.csproj" -c Release -o /app

# FINAL
FROM base AS final
ENV PUPPETEER_EXECUTABLE_PATH=/usr/bin/chromium-browser
WORKDIR /app
RUN chown worker /app
USER worker
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ScreenshotService.dll"]