using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Pipes
{
    public interface IPipeWithInputKey : IPipe
    {
        string InputKey { get; }
    }
}
