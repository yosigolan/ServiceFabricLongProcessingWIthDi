using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StatelessNativeService
{
    public class ExecutionCancelltionToken
    {
        private readonly CancellationTokenSource _tokenSource;

        public ExecutionCancelltionToken()
        {
            _tokenSource = new CancellationTokenSource();
        }

        public void RequestCancelltion()
        {
            _tokenSource.Cancel();
        }

        public bool IsCancellationRequested
        {
            get
            {
                return _tokenSource.IsCancellationRequested;
            }
        }
    }
}
