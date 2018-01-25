using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Pipes
{
    public abstract class GenericPipe<T> : PipeBase, IPipeWithInputKey
    {
        public virtual string InputKey => typeof(T).Name;

        protected abstract void Process(T parameter);

        public override void Process(IDictionary<string, object> load)
        {
            var parameter = (T)load[InputKey];

            Process(parameter);
        }
    }
}
