
open System
open Orleans
open Orleans.Configuration
open Orleans.Hosting
open System.Net
open OrleansDashboard

[<EntryPoint>]
let main args =
    let theTask = task {
        let host = SiloHostBuilder()
                        .Configure(fun (x : ClusterOptions) -> 
                            x.ClusterId <- "fsharp-orleans-reminders-cluster-id"
                            x.ServiceId <- "fsharp-orleans-reminders-service-id")
                        .UseLocalhostClustering()
                        .UseDashboard(fun (x : DashboardOptions) -> 
                            x.HostSelf <- true
                            x.Port <- 9090)
                        .ConfigureEndpoints(IPAddress.Loopback, 2020, 4040)
                        .Build()
                 
        do! host.StartAsync ()        
        printfn "App is now running"
        Console.ReadLine () |> ignore
    } 
    theTask.Wait()
    0
    