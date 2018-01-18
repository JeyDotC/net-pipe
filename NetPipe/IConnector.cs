using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe
{
    public interface IConnector
    {
        IConnector RunPipes(IDictionary<string, object> load);

        void ReceivePipe(IPipe previous, IPipe next);
    }
}
