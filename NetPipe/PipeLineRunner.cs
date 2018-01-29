using System;
using System.Collections.Generic;

namespace NetPipe
{
    public class PipeLineRunner
    {
        private readonly IConnector _startConnector;
        private readonly IEnumerable<IConnector> _connectors;

        public void BeforePipeRun(EventHandler<BeforePipeRunEventArgs> beforePipeRun)
        {
            foreach (var connector in _connectors)
            {
                connector.BeforePipeRun += beforePipeRun;
            }
        }
        public void PipeSuccess(EventHandler<PipeSuccessEventArgs> pipeSuccess)
        {
            foreach (var connector in _connectors)
            {
                connector.PipeSuccess += pipeSuccess;
            }
        }
        public void PipeError(EventHandler<PipeErrorEventArgs> pipeError)
        {
            foreach (var connector in _connectors)
            {
                connector.PipeError += pipeError;
            }
        }

        public PipeLineRunner(IConnector startConnector, IEnumerable<IConnector> connectors)
        {
            _startConnector = startConnector;
            _connectors = connectors;
        }

        public void Run(IDictionary<string, object> load)
           => RunInternal(_startConnector, load);

        public void RunFrom(Func<IEnumerable<IConnector>, IConnector> condition, IDictionary<string, object> load)
            => RunInternal(condition(_connectors), load);

        private static void RunInternal(IConnector currentConnector, IDictionary<string, object> load)
        {
            while (currentConnector != null)
            {
                currentConnector = currentConnector.RunPipes(load);
            }
        }
    }
}
