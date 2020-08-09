using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StatelessNativeService
{
    public class ServiceEntryPoint : BackgroundService
    {
        private InjectedService _injectedService;
        private ExecutionCancelltionToken _executionCancelltionToken;
        public ServiceEntryPoint(InjectedService injectedService, ExecutionCancelltionToken executionCancelltionToken)
        {
            _injectedService = injectedService;
            _executionCancelltionToken = executionCancelltionToken;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // initialize
            Trace.TraceInformation("Initializing");
            await Initialize();
            
            Trace.TraceInformation("Done Initializing");
            int iteration = 0;
            while (true)
            {
                Trace.TraceInformation("Running " + iteration);
                iteration++;
                if (stoppingToken.IsCancellationRequested)
                {
                    Trace.TraceInformation("SHut down requsted");
                    break;
                }
                // do your thing
                _injectedService.DoSomthing();

                // example of how to request to end the execution
                if (iteration == 15)
                {
                    Trace.TraceInformation("Requesting stop");
                    _executionCancelltionToken.RequestCancelltion();
                }
                await Task.Delay(1000);
            }
            await Finalize();
            Trace.TraceInformation("Done SHuting down");
        }

        private async Task Initialize()
        {
            // add all the initialization code here
            await Task.Delay(1000);
        }

        private async Task Finalize()
        {
            try
            {
                // add all the dispose code here
                Trace.TraceInformation("cant do awiat");
                await Task.Delay(1000);
            }
            catch (Exception e)
            {
                Trace.TraceError("Fuck");
            }
        }
    }
}
