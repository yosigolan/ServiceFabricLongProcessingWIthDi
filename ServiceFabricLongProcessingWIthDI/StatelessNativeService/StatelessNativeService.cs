using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace StatelessNativeService
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class StatelessNativeService : StatelessService
    {
        public StatelessNativeService(StatelessServiceContext context)
            : base(context)
        { 
            
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.


            ExecutionCancelltionToken endExecutionToken = new ExecutionCancelltionToken();
            IHost host = BuildHost(endExecutionToken);
            Task backgroundTask = host.StartAsync();
            int iteration = 0;
            Trace.TraceInformation("Service running");
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    await host.StopAsync();
                    break;
                }
                if (endExecutionToken.IsCancellationRequested)
                {
                    await host.StopAsync();
                    break;
                }
                Trace.TraceInformation("Running itiration " + iteration);
                iteration++;
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            
            await backgroundTask;
        }


        private IHost BuildHost(ExecutionCancelltionToken endExecutionToken)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                ConfigureServices(services);
                services.AddSingleton(endExecutionToken);
                // required to allow graceful shutdown
                services.Configure<HostOptions>(opts => opts.ShutdownTimeout = TimeSpan.FromSeconds(15));
                services.AddHostedService<ServiceEntryPoint>();
            });
          
            IHost host = builder.Build();
            return host;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<InjectedService>();
        }
    }
}
