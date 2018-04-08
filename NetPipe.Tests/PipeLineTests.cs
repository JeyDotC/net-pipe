using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using NetPipe.Pipes.Extensions;
using NetPipe.Connectors.Extensions;
using NetPipe.Pipes;
using System.Linq;

namespace NetPipe.Tests
{
    [TestClass]
    public class PipeLineTests
    {
        [TestMethod]
        public void PipeLineTests_DirectConnector()
        {
            var testLoad = new Dictionary<string, object>();

            new PipeLine()
                .Pipe(load => load["pipe1"] = true)
                .Connect()
                .Pipe(load => load["pipe2"] = true)
                .Connect()
                .Pipe(load => load["pipe3"] = true)
                .Finish()
                .Run(testLoad);

            Assert.AreEqual(testLoad["pipe1"], true);
            Assert.AreEqual(testLoad["pipe2"], true);
            Assert.AreEqual(testLoad["pipe3"], true);
        }

        [TestMethod]
        public void PipeLineTests_DirectConnector_Events()
        {
            var testLoad = new Dictionary<string, object>();

            var pipeLineRunner = new PipeLine()
                .Pipe("pipe1", load => load["pipe1"] = true)
                .Connect()
                .Pipe("pipe2", load => load["pipe2"] = true)
                .Connect()
                .Pipe("pipe3", load => load["pipe3"] = true)
                .Finish();

            pipeLineRunner.BeforePipeRun((sender, e) 
                => e.Load[$"{(e.Pipe as NamedPipe).Name}-BeforeRun"] = true);

            pipeLineRunner.PipeSuccess((sender, e)
                => e.Load[$"{(e.Pipe as NamedPipe).Name}-PipeSuccess"] = true);

            pipeLineRunner.Run(testLoad);

            Assert.AreEqual(testLoad["pipe1"], true);
            Assert.AreEqual(testLoad["pipe2"], true);
            Assert.AreEqual(testLoad["pipe3"], true);

            Assert.AreEqual(testLoad["pipe1-BeforeRun"], true);
            Assert.AreEqual(testLoad["pipe2-BeforeRun"], true);
            Assert.AreEqual(testLoad["pipe3-BeforeRun"], true);

            Assert.AreEqual(testLoad["pipe1-PipeSuccess"], true);
            Assert.AreEqual(testLoad["pipe2-PipeSuccess"], true);
            Assert.AreEqual(testLoad["pipe3-PipeSuccess"], true);
        }

        [TestMethod]
        public void ConditionalConnectorTests_BasicConnectPipes()
        {
            //         2
            //       /   \
            // -> 1-+--3--+-5
            //       \   /
            //         4
            var pipeLine = new PipeLine()
                .Pipe(load => load["pipe1"] = true)
                .ConnectWhen((pipes, load) =>
                {
                    var chosenPath = load["ChosenPipe"].ToString();
                    return pipes.OfType<NamedPipe>().First(p => p.Name == chosenPath);
                })
                .Pipe("pipe2", load => load["pipe2"] = true)
                .Pipe("pipe3", load => load["pipe3"] = true)
                .Pipe("pipe4", load => load["pipe4"] = true)
                .Join()
                .Pipe(load => load["pipe5"] = true)
                .Finish();
            
            var path125 = new Dictionary<string, object> { ["ChosenPipe"] = "pipe2" };
            var path135 = new Dictionary<string, object> { ["ChosenPipe"] = "pipe3" };
            var path145 = new Dictionary<string, object> { ["ChosenPipe"] = "pipe4" };

            pipeLine.Run(path125);
            pipeLine.Run(path135);
            pipeLine.Run(path145);

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
