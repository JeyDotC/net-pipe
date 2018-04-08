using System;
using System.Collections.Generic;

namespace NetPipe
{
    public abstract class PipeEventArgs : EventArgs
    {
        public IPipe Pipe { get; }

        public IDictionary<string, object> Load { get; }

        public PipeEventArgs(IPipe pipe, IDictionary<string, object> load)
        {
            Pipe = pipe;
            Load = load;
        }
    }
}