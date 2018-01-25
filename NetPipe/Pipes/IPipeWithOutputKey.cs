using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Pipes
{
    public interface IPipeWithOutputKey : IPipe
    {
        string OutputKey { get; }
    }
}
