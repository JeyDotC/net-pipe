using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Connectors.Extensions
{
    public static class ConditionalConnectorExtensions
    {
        public static PipeLine ConnectWhen(this PipeLine pipeLine, Func<IEnumerable<IPipe>, IDictionary<string, object>, IPipe> condition)
        {
            pipeLine.Connector(new ConditionalConnector(condition));
            return pipeLine;
        }
    }
}
