using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe
{
    public interface IConnector
    {
        event EventHandler<BeforePipeRunEventArgs> BeforePipeRun;
        event EventHandler<PipeSuccessEventArgs> PipeSuccess;
        event EventHandler<PipeErrorEventArgs> PipeError;

        IConnector RunPipes(IDictionary<string, object> load);

        void ReceivePipe(IPipe previous, IPipe next);
    }
}
