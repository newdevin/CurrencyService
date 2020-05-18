// Learn more about F# at http://fsharp.org

open System
open System.Threading.Tasks
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.Configuration
open System.IO


type Worker(logger : ILogger<Worker>) =
    inherit BackgroundService()
    let _logger = logger
    override bs.ExecuteAsync stoppingToken =
        let f : Async<unit> = async {
            while not stoppingToken.IsCancellationRequested do
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now)
                do! Async.Sleep(1000)
        }
        // ExecuteAsync needs to return a task, hence up-cast from Task<unit> to plain Task.
        Async.StartAsTask f :> Task

let CreateHostBuilder argv : IHostBuilder =
    let builder = Host.CreateDefaultBuilder(argv)
    builder
        .UseWindowsService()
        .ConfigureAppConfiguration(fun hostingContext  config -> 
            config
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json" , false, true)
                |> ignore 
            )
        .ConfigureServices(fun hostContext services -> services.AddHostedService<Worker>() |> ignore<IServiceCollection>)

[<EntryPoint>]
let main argv =
    let hostBuilder = CreateHostBuilder argv
    hostBuilder.Build().Run()
    0 // return an integer exit code
