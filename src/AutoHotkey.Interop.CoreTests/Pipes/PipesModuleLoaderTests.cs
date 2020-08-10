using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoHotkey.Interop.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHotkey.Interop.Pipes.Tests
{
    [TestClass()]
    public class PipesModuleLoaderTests
    {
        AutoHotkeyEngine ahk = AutoHotkeyEngine.Instance;

        private void InitPipes()
        {
            Func<string, string> EchoHandler = new Func<string, string>(msg => "SERVER:" + msg);
            AutoHotkey.Interop.Pipes.PipesModuleLoader.LoadPipesModule(EchoHandler);
        }

        [TestMethod()]
        public void LoadPipesModuleMultipleTimesHasNoErrors()
        {
            InitPipes();
            InitPipes();
            InitPipes();
            InitPipes();
            InitPipes();
        }

        [TestMethod]
        public void LoadingPipesLibraryHasSendPipeMessageFunction()
        {
            InitPipes();
            Assert.IsTrue(ahk.FunctionExists("SendPipeMessage"));
        }

        [TestMethod]
        public void TestPipeCommunication()
        {
            InitPipes();
            string ahk_code =
                @"serverMessage := SendPipeMessage(""Hello"")
                 ";
            ahk.LoadScript(ahk_code);
            Assert.AreEqual("SERVER:Hello", ahk.GetVar("serverMessage"));
        }
    }
}