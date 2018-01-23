using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Pipes.Extensions
{
    public static class DelegatePipeExtensions
    {
        public static PipeLine Pipe(this PipeLine pipeLine, Action<IDictionary<string, object>> process)
        {
            pipeLine.Pipe(new DelegatePipe(process));
            return pipeLine;
        }

        public static PipeLine Pipe<T>(this PipeLine pipeLine, Action<T> process)
        {
            pipeLine.Pipe(new DelegateGenericPipe<T>(process));
            return pipeLine;
        }

        public static PipeLine Pipe<T, TResult>(this PipeLine pipeLine, Func<T, TResult> process)
        {
            pipeLine.Pipe(new DelegateGenericPipeWithOutput<T, TResult>(process));
            return pipeLine;
        }

        public static PipeLine Pipe(this PipeLine pipeLine, string name, Action<IDictionary<string, object>> process)
        {
            pipeLine.Pipe(new DelegatePipe(process).Named(name));
            return pipeLine;
        }

        public static PipeLine Pipe<T>(this PipeLine pipeLine, string name, Action<T> process)
        {
            pipeLine.Pipe(new DelegateGenericPipe<T>(process).Named(name));
            return pipeLine;
        }

        public static PipeLine Pipe<T, TResult>(this PipeLine pipeLine, string name, Func<T, TResult> process)
        {
            pipeLine.Pipe(new DelegateGenericPipeWithOutput<T, TResult>(process).Named(name));
            return pipeLine;
        }
    }
}
