using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Basic.Library;
namespace UnitTestProject
{
    [TestClass]
    public class UnitTestEncryptUtils
    {
        public const string key = "jksd";
        [TestMethod]
        public void TestMethod1()
        {
            string data = "HelloWorld";
            string des3Data = EncryptUtils.DES3Encrypt(data, key);
            string to = EncryptUtils.DES3Decrypt(des3Data, key);
            Assert.IsTrue(to.Equals(data));
        }
    }
}
