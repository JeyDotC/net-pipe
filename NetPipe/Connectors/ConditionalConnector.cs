using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Connectors
{
    public class ConditionalConnector : IConnector
    {
        private IList<IPipe> _options = new List<IPipe>();

        private readonly Func<IEnumerable<IPipe>, IDictionary<string, object>, IPipe> _condition;

        public ConditionalConnector(Func<IEnumerable<IPipe>, IDictionary<string, object>, IPipe> condition)
        {
            _condition = condition;
        }

        public void ReceivePipe(IPipe previous, IPipe next)
        {
            if (previous != null)
            {
                previous.OutputConnector = this;
            }

            _options.Add(next ?? throw new ArgumentNullException(nameof(next)));
            next.InputConnector = this;
        }

        public IConnector RunPipes(IDictionary<string, object> load)
        {
            var pipe = _condition(_options, load);
            
            pipe?.Process(load);

            return pipe?.OutputConnector;
        }
    }
}
