using NetPipe.Connectors;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetPipe
{
    public class PipeLine
    {
        private List<IPipe> _previousPipeList = new List<IPipe>();
        private List<IPipe> _currentPipeList = new List<IPipe>();
        private IConnector _lastConnector;
        private IConnector _startConnector;
        private readonly IList<IConnector> _connectors;
        private bool _hasPipesPending = false;

        public PipeLine()
            : this(new DirectConnector())
        {
        }

        public PipeLine(IConnector startConnector)
        {
            _startConnector = startConnector;
            _lastConnector = startConnector;
            _connectors = new List<IConnector> { _startConnector };
        }

        public PipeLineRunner Finish()
        {
            Flush();
            return new PipeLineRunner(_startConnector, _connectors);
        }

        private static void RunInternal(IConnector currentConnector, IDictionary<string, object> load)
        {
            while (currentConnector != null)
            {
                currentConnector = currentConnector.RunPipes(load);
            }
        }

        public void Pipe(IPipe pipe)
        {
            _currentPipeList.Add(pipe);
            _hasPipesPending = true;
        }

        public void Connector(IConnector connector)
        {
            Flush();

            _connectors.Add(connector);
            _lastConnector = connector;
        }

        private void Flush()
        {
            if(!_hasPipesPending)
            {
                return;
            }

            foreach (var previous in _previousPipeList)
            {
                ReceivePipes(_lastConnector, previous, _currentPipeList);
            }

            if (!_previousPipeList.Any())
            {
                ReceivePipes(_lastConnector, null, _currentPipeList);
            }

            _previousPipeList = _currentPipeList;
            _currentPipeList = new List<IPipe>();
            _hasPipesPending = false;
        }

        private static void ReceivePipes(IConnector connector, IPipe previous, IEnumerable<IPipe> nexts)
        {
            foreach (var next in nexts)
            {
                connector.ReceivePipe(previous, next);
            }
        }
    }
}
