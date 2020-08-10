using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoHotkey.Interop.Util;

namespace AutoHotkey.Interop.Core.Tests.Util
{
    [TestClass]
    public class EmbededResourceHelperTests
    {
        private Assembly TestAssembly = typeof(EmbededResourceHelperTests).Assembly;


        public EmbededResourceHelperTests()
        {
            DeleteExtractFiles();
        }

        private void DeleteExtractFiles()
        {
            //delete any phsyical output files that may have happend
            if (File.Exists("extract1.txt"))
                File.Delete("extract1.txt");
            if (File.Exists("extract2.txt"))
                File.Delete("extract2.txt");
        }

        [TestMethod]
        public void can_find_resource_with_only_file_name()
        {
            string name = EmbededResourceHelper.FindByName(TestAssembly, "file.txt");
            Assert.AreEqual("AutoHotkey.Interop.CoreTests.Util.file.txt", name);
        }

        [TestMethod]
        public void can_find_resource_with_folder_and_file_name()
        {
            string name = EmbededResourceHelper.FindByName(TestAssembly, "Util/file.txt");
            Assert.AreEqual("AutoHotkey.Interop.CoreTests.Util.file.txt", name);
        }

        [TestMethod]
        public void can_find_resource_with_full_name()
        {
            string found = EmbededResourceHelper.FindByName(TestAssembly,
                "AutoHotkey.Interop.CoreTests.Util.file.txt");

            Assert.AreEqual("AutoHotkey.Interop.CoreTests.Util.file.txt", found);
        }

        [TestMethod]
        public void retuns_null_for_missing()
        {
            string name = EmbededResourceHelper.FindByName(TestAssembly, "missingfile.txt");
            Assert.IsNull(name);
        }

        [TestMethod]
        public void returns_null_on_partial_folder_name()
        {
            //not Util/file.txt
            string found = EmbededResourceHelper.FindByName(TestAssembly, "til/file.txt");
            Assert.IsNull(found);
        }

        [TestMethod]
        public void can_extract_resouce_to_file()
        {
            string testOutputFile = "extract1.txt";
            string fullResourceName = EmbededResourceHelper.FindByName(TestAssembly, "file.txt");
            EmbededResourceHelper.ExtractToFile(TestAssembly, fullResourceName, testOutputFile);

            string testFileContent = File.ReadAllText("extract1.txt");
            string expectedFileContent = "this is a test file.";
            Assert.AreEqual(expectedFileContent, testFileContent);
        }
    }
}
