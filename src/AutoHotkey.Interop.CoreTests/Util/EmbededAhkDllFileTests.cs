using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHotkey.Interop.Core.Util.Tests
{
    [TestClass]
    public class EmbededAhkDllFileTests
    {
        [TestMethod]
        public void HasX86AhkdllEmbeded()
        {
            VerifyEmbededResource("AutoHotkey.Interop.x86.AutoHotkey.dll");
        }

        [TestMethod]
        public void HasX64AhkdllEmbeded()
        {
            VerifyEmbededResource("AutoHotkey.Interop.x64.AutoHotkey.dll");
        }

        private static void VerifyEmbededResource(string embededResourceFullName)
        {
            var interopAssembly = typeof(AutoHotkeyEngine).Assembly;
            var assemblyResourceNames = interopAssembly.GetManifestResourceNames();
            bool foundResource = assemblyResourceNames.Contains(embededResourceFullName);

            Assert.IsTrue(foundResource,
                string.Format("Could not find the embeded resource '{0}'", embededResourceFullName));
        }
    }
}
