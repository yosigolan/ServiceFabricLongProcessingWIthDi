using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StatelessNativeService
{
    public class InjectedService
    {
        public void DoSomthing()
        {
            Trace.TraceInformation("InjectedService I did something");
        }
    }
}
