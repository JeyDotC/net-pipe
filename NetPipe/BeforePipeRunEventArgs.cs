using System;

namespace NetPipe
{
    public class BeforePipeRunEventArgs : PipeEventArgs
    {
        public bool Skip { get; set; }

        public BeforePipeRunEventArgs(IPipe pipe) : base(pipe)
        {
        }
    }
}