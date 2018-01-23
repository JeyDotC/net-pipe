using System;

namespace NetPipe
{
    public class PipeRunEventArgs : PipeEventArgs
    {
        public PipeRunEventArgs(IPipe pipe) : base(pipe)
        {
        }
    }
}