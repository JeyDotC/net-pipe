using System;
using System.Collections.Generic;

namespace NetPipe
{
    public interface IPipe
    {
        IConnector InputConnector { get; set; }

        IConnector OutputConnector { get; set; }
        
        void Process(IDictionary<string, object> load);
    }
}
