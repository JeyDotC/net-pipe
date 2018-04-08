using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Connectors
{
    public abstract class ConnectorBase : IConnector
    {
        public event EventHandler<BeforePipeRunEventArgs> BeforePipeRun;
        public event EventHandler<PipeSuccessEventArgs> PipeSuccess;
        public event EventHandler<PipeErrorEventArgs> PipeError;

        protected IConnector RunPipe(IPipe pipe, IDictionary<string, object> load)
        {
            var beforeRunPipeEventArgs = new BeforePipeRunEventArgs(pipe, load);

            BeforePipeRun?.Invoke(this, beforeRunPipeEventArgs);

            if (beforeRunPipeEventArgs.Skip)
            {
                return pipe.OutputConnector;
            }

            try
            {
                pipe.Process(load);
                PipeSuccess?.Invoke(this, new PipeSuccessEventArgs(pipe, load));
            }
            catch (Exception ex)
            {
                var errorEventArgs = new PipeErrorEventArgs(pipe, load, ex);

                PipeError?.Invoke(this, errorEventArgs);

                if (errorEventArgs.ErrorAction == ErrorAction.Rethrow)
                {
                    throw;
                }
            }

            return pipe.OutputConnector;
        }

        public abstract void ReceivePipe(IPipe previous, IPipe next);

        public abstract IConnector RunPipes(IDictionary<string, object> load);
    }
}
