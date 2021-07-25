using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StatelessNativeService
{
    public class InjectedService
    {
        private ILogger<InjectedService> _logger;
        public InjectedService(ILogger<InjectedService> logger)
        {
            _logger = logger;
        }
        
        public void DoSomthing()
        {
            _logger.LogInformation("Main logger writes ");
            var position = new { Latitude = 25, Longitude = 134 };
            var elapsedMs = 34;

            _logger.LogInformation("Processed {@Position} in {Elapsed:000} ms.", position, elapsedMs);
            Trace.TraceInformation("InjectedService I did something");
        }
    }
}
