# OpenTelemetry Demo

## How to run

- To create the images and start the containers, run
  the `docker compse up --build --remove-orphans --force-recreate` command from the root folder.
- To stop and remove the containers, network and images created locally, run
  the `docker compose down --rmi local` command from the root folder.
- Open the [Weather app](http://localhost:8080).
- Refresh the weather page for the weather app to send a few requests to the weather API to fetch
  weather forecasts.
- Open [Jaeger UI](http://localhost:16686)
- Goto **Find Traces** -> **Services** and select **Weather.Forecasts.API** to see the traces from the
  Weather Forecasts API.

## Resources

- [Getting started with OpenTelemetry Metrics in .NET 8. Part 2: Instrumenting the BookStore API :: my tech ramblings — A blog for writing about my techie ramblings](https://www.mytechramblings.com/posts/getting-started-with-opentelemetry-metrics-and-dotnet-part-2/)
- [Getting started — Jaeger documentation (jaegertracing.io)](https://www.jaegertracing.io/docs/1.6/getting-started/)
- [.NET Observability with OpenTelemetry - .NET | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/observability-with-otel)
