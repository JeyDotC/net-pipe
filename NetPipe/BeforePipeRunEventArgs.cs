using System;

namespace NetPipe
{
    public class BeforePipeRunEventArgs : PipeEventArgs
    {
        public BeforePipeRunEventArgs(IPipe pipe) : base(pipe)
        {
        }
    }
}