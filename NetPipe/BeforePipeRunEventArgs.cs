using System;
using System.Collections.Generic;

namespace NetPipe
{
    public class BeforePipeRunEventArgs : PipeEventArgs
    {
        public bool Skip { get; set; }

        public BeforePipeRunEventArgs(IPipe pipe, IDictionary<string, object> load) : base(pipe, load)
        {
        }
    }
}