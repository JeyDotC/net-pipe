using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Connectors
{
    public class ConditionalConnector : ConnectorBase
    {
        private IList<IPipe> _options = new List<IPipe>();

        private readonly Func<IEnumerable<IPipe>, IDictionary<string, object>, IPipe> _condition;

        public ConditionalConnector(Func<IEnumerable<IPipe>, IDictionary<string, object>, IPipe> condition)
        {
            _condition = condition;
        }

        public override void ReceivePipe(IPipe previous, IPipe next)
        {
            if (previous != null)
            {
                previous.OutputConnector = this;
            }

            _options.Add(next ?? throw new ArgumentNullException(nameof(next)));
            next.InputConnector = this;
        }

        public override IConnector RunPipes(IDictionary<string, object> load)
        {
            var pipe = _condition(_options, load);

            return pipe != null ? RunPipe(pipe, load) : null;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("{ ");
            foreach (var pipe in _options)
            {
                stringBuilder.Append($"{(pipe?.ToString())} -> {(pipe?.OutputConnector?.ToString())}");
            }
            stringBuilder.Append(" }");

            return stringBuilder.ToString();
        }
    }
}
