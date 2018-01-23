using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPipe.Connectors;
using NetPipe.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetPipe.Tests
{
    [TestClass]
    public class ConditionalConnectorTests
    {
        private IPipe pipe1;
        private IPipe pipe2;
        private IPipe pipe3;
        private IPipe pipe4;
        private IPipe pipe5;

        private class PipeWithName : PipeBase
        {
            private readonly string _name;

            public PipeWithName(string name) => _name = name;

            public string Name => _name;

            public override void Process(IDictionary<string, object> load) => load[_name] = true;
        }

        [TestInitialize]
        public void Init()
        {
            pipe1 = new PipeWithName("pipe1");
            pipe2 = new PipeWithName("pipe2");
            pipe3 = new PipeWithName("pipe3");
            pipe4 = new PipeWithName("pipe4");
            pipe5 = new PipeWithName("pipe5");
        }

        [TestMethod]
        public void ConditionalConnectorTests_BasicConnectPipes()
        {
            //         2
            //       /   \
            // -> 1-+--3--+-5
            //       \   /
            //         4

            var startConnector = new DirectConnector();
            var conditionalConnector = new ConditionalConnector((pipes, load) =>
            {
                var chosenPath = load["ChosenPipe"].ToString();
                return pipes.OfType<PipeWithName>().First(p => p.Name == chosenPath);
            });
            var endConnector5 = new JoinConnector();

            // Start
            startConnector.ReceivePipe(null, pipe1);
            // Divide into options
            conditionalConnector.ReceivePipe(pipe1, pipe2);
            conditionalConnector.ReceivePipe(pipe1, pipe3);
            conditionalConnector.ReceivePipe(pipe1, pipe4);
            // Join again
            endConnector5.ConnectPrevious(pipe2);
            endConnector5.ConnectPrevious(pipe3);
            endConnector5.ConnectPrevious(pipe4);
            endConnector5.JoinInto(pipe5);

            var path125 = new Dictionary<string, object> { ["ChosenPipe"] = "pipe2" };
            var path135 = new Dictionary<string, object> { ["ChosenPipe"] = "pipe3" };
            var path145 = new Dictionary<string, object> { ["ChosenPipe"] = "pipe4" };

            startConnector
                .RunPipes(path125)  // pipe1
                .RunPipes(path125)  // pipe2
                .RunPipes(path125); // pipe5

            startConnector
                .RunPipes(path135)  // pipe1
                .RunPipes(path135)  // pipe3
                .RunPipes(path135); // pipe5

            startConnector
                .RunPipes(path145)  // pipe1
                .RunPipes(path145)  // pipe4
                .RunPipes(path145); // pipe5

            Assert.AreEqual(path125["pipe1"], true);
            Assert.AreEqual(path125["pipe2"], true);
            Assert.AreEqual(path125["pipe5"], true);

            Assert.AreEqual(path135["pipe1"], true);
            Assert.AreEqual(path135["pipe3"], true);
            Assert.AreEqual(path135["pipe5"], true);

            Assert.AreEqual(path145["pipe1"], true);
            Assert.AreEqual(path145["pipe4"], true);
            Assert.AreEqual(path145["pipe5"], true);
        }
    }
}
