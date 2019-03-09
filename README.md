# screenshot-as-a-service

There's plenty of things which I could've improved on, but didn't have time:

1.  RequestController shouldn't contain logic within its endpoints
2.  ScreenshotRequestConsumer shouldn't contain the actual screenshot logic.
3.  Configuration is far from perfect.
4.  The service currently only uses an in-memory db through EntityFrameworkCore, to actually scale this
    you'd want to use something like PostgreSQL/MySQL/MSSQL Server.



## How to

### Run in Docker (with docker-compose)

Run `docker-compose up -d`

### Run without Docker

You'll need RabbitMQ installed with default settings, as well as the .NET Core runtime.

1. Start the service in `ScreenshotService` by running `dotnet run`.
2. Start the worker in `ScreenshotWorker` by running `dotnet run`.

You'll reach the service on `http://localhost:5000`, but the screenshots won't be served through http (when using Docker they're served with nginx).

### Use the API

Send a POST request to `http://localhost/api/request` with a `application/json`-formed body like below
```
{
	"Urls": [
		"https://www.google.com/",
		"http://www.facebook.com/"
		]
}
```

See all requests by sending a GET to `http://localhost/api/request`. Each processed screenshot has a `path` field which shows the filename of the screenshot. The screenshot can be found in the `screenshots/` directory, or queried with `http://localhost/screenshots/${filename}`.
