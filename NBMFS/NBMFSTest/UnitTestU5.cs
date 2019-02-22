using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Napier_Message_Service.Models;

namespace NBMFSTest
{
    /// <summary>
    /// Summary description for UnitTestU5
    /// </summary>
    [TestClass]
    public class UnitTestU5
    {
        //Test ProcessHyperlinks method in Email class
        [TestMethod]
        public void U5_1()
        {
            Email email = new Email("E000000001", "discount@tesco.uk", "Discounts at Tesco", "Hi, Please visit www.tesco.com/discount!");
            email.ProcessHyperlinks();

            List<string> expectedListOfHyperlinks = new List<string>();
            expectedListOfHyperlinks.Add("www.tesco.com/discount");

            string expectedMessage = "Hi, Please visit <URL Quarantined>!";

            Assert.AreEqual(expectedMessage, email.MessageText);
            CollectionAssert.AreEqual(expectedListOfHyperlinks, email.URLs);
        }

        [TestMethod]
        public void U5_2()
        {
            Email email = new Email("E000000001", "discount@tesco.uk", "Discounts at Tesco", "Hi, Please visit https://tesco.com");
            email.ProcessHyperlinks();

            List<string> expectedListOfHyperlinks = new List<string>();
            expectedListOfHyperlinks.Add("https://tesco.com");

            string expectedMessage = "Hi, Please visit <URL Quarantined>";

            Assert.AreEqual(expectedMessage, email.MessageText);
            CollectionAssert.AreEqual(expectedListOfHyperlinks, email.URLs);
        }


        [TestMethod]
        public void U5_3()
        {
            Email email = new Email("E000000001", "discount@tesco.uk", "Discounts at Tesco", "Hi, please visit http://tesco.com/discount and www.Tesco.com/bargains!!!");
            email.ProcessHyperlinks();

            List<string> expectedListOfHyperlinks = new List<string>();
            expectedListOfHyperlinks.Add("http://tesco.com/discount");
            expectedListOfHyperlinks.Add("www.Tesco.com/bargains");

            string expectedMessage = "Hi, please visit <URL Quarantined> and <URL Quarantined>!!!";

            Assert.AreEqual(expectedMessage, email.MessageText);
            CollectionAssert.AreEqual(expectedListOfHyperlinks, email.URLs);
        }
    }
}
