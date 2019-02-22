using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Napier_Message_Service.Models;

namespace NBMFSTest
{
    /// <summary>
    /// Summary description for UnitTestU1
    /// </summary>
    [TestClass]
    public class UnitTestU1
    {
        //Test ProcessAbbreviations method in SMS class
        [TestMethod]
        public void U1_1()
        {
            SMS sms = new SMS("S000000001", "+002338176271", "LOL");
            sms.ProcessAbbreviations();
            Assert.AreEqual("LOL<Laughing out loud>", sms.MessageText);
        }

        [TestMethod]
        public void U1_2()
        {
            SMS sms = new SMS("S000000001", "+002338176271", "LOL testU1_2 AKA testU1_2 B4N.");
            sms.ProcessAbbreviations();
            Assert.AreEqual("LOL<Laughing out loud> testU1_2 AKA<Also known as> testU1_2 B4N<Bye for now>.", sms.MessageText);
        }

        [TestMethod]
        public void U1_3()
        {
            SMS sms = new SMS("S000000001", "+002338176271", "PLZPLZPLZ");
            sms.ProcessAbbreviations();
            Assert.AreEqual("PLZ<Please>PLZ<Please>PLZ<Please>", sms.MessageText);
        }

    }
}
