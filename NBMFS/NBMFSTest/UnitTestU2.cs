using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Napier_Message_Service.Models;


namespace NBMFSTest
{
    /// <summary>
    /// Summary description for UnitTestU2
    /// </summary>
    [TestClass]
    public class UnitTestU2
    {
        //Test ProcessAbbreviations method in SMS class
        [TestMethod]
        public void U2_1()
        {
            Tweet tweet = new Tweet("T000000001", "@test", "LOL");
            tweet.ProcessAbbreviations();
            Assert.AreEqual("LOL<Laughing out loud>", tweet.MessageText);
        }

        [TestMethod]
        public void U2_2()
        {
            Tweet tweet = new Tweet("T000000001", "@test", "LOL testU1_2 AKA testU1_2 B4N.");
            tweet.ProcessAbbreviations();
            Assert.AreEqual("LOL<Laughing out loud> testU1_2 AKA<Also known as> testU1_2 B4N<Bye for now>.", tweet.MessageText);
        }

        [TestMethod]
        public void U2_3()
        {
            Tweet tweet = new Tweet("T000000001", "@test", "PLZPLZPLZ");
            tweet.ProcessAbbreviations();
            Assert.AreEqual("PLZ<Please>PLZ<Please>PLZ<Please>", tweet.MessageText);
        }

    }
}
