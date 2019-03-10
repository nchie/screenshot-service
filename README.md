# screenshot-as-a-service

There's plenty of things which I could've improved on:

1.  Configuration is far from perfect.
2.  Almost no error handling.
3.  The service currently only uses an in-memory db through EntityFrameworkCore, to actually scale this
    you'd want to use something like PostgreSQL/MySQL/MSSQL Server.



## How to

### Run in Docker (with docker-compose)

Run `docker-compose up -d`

Service will be reachable on `http://localhost/`.

### Run without Docker

You'll need RabbitMQ installed with default settings, as well as the .NET Core runtime and Chromium.

1. Point the `PUPPETEER_EXECUTABLE_PATH` env variable to your Chromium binary.
2. Start the service in `ScreenshotService` by running `dotnet run`.
3. Start the worker in `ScreenshotWorker` by running `dotnet run`.

Service will be reachable on `http://localhost:5000`.

### Use the API

Send a POST request to `/api/request` with a `application/json`-formed body like below
```
{
	"Urls": [
		"https://www.google.com/",
		"http://www.facebook.com/"
		]
}
```

See requests by sending a GET to `/api/request` or `/api/request/${guid}`. Each processed screenshot has a `path` field which shows the filename of the screenshot. The screenshots are served at `/screenshots/${filename}`.
