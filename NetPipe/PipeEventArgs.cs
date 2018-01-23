using System;

namespace NetPipe
{
    public abstract class PipeEventArgs : EventArgs
    {
        public IPipe Pipe { get; }

        public PipeEventArgs(IPipe pipe) => Pipe = pipe;
    }
}