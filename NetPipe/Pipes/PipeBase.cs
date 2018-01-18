using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Pipes
{
    public abstract class PipeBase : IPipe
    {
        public IConnector InputConnector { get; set; }

        public IConnector OutputConnector { get; set; }
        
        public abstract void Process(IDictionary<string, object> load);
    }
}
