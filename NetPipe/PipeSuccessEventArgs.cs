using System.Collections.Generic;

namespace NetPipe
{
    public class PipeSuccessEventArgs : PipeEventArgs
    {
        public PipeSuccessEventArgs(IPipe pipe, IDictionary<string, object> load) : base(pipe, load)
        {
        }
    }
}