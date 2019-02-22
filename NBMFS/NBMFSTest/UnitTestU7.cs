using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Napier_Message_Service.Models;

namespace NBMFSTest
{
    /// <summary>
    /// Summary description for UnitTestU7
    /// </summary>
    [TestClass]
    public class UnitTestU7
    {
        //Test ProcessMessage method in MessageProcessingService class
        [TestMethod]
        public void U7_1()
        {
            MessageProcessingService mps = MessageProcessingService.Instance;

            string id = "S000000001";
            string body = "+44829918273\nHi HRU Bob, we have a great sale on at the moment, 50 % off! Visit www.topshop.com";

            Message actual = mps.ProcessMessage(id, body);

            SMS expected = new SMS("S000000001", "+44829918273", "Hi HRU<How are you?> Bob, we have a great sale on at the moment, 50 % off! Visit www.topshop.com");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Sender, actual.Sender);
            Assert.AreEqual(expected.MessageText, actual.MessageText);
            Assert.AreEqual(MessageFactory.ProcessingError, null);
        }

        [TestMethod]
        public void U7_2()
        {
            MessageProcessingService mps = MessageProcessingService.Instance;

            string id = "E000000001";
            string body = "noreply@topshop.com\nwelcome\nHRU? Thank you for signing up to Topshop emails.\nVisit us at http://topshop.com";

            Message actual = mps.ProcessMessage(id, body);

            Email expected = new Email("E000000001", "noreply@topshop.com", "welcome", "HRU? Thank you for signing up to Topshop emails.\nVisit us at <URL Quarantined>");

            if (actual is Email email)
            {
                Assert.AreEqual(expected.GetType(), email.GetType());
                Assert.AreEqual(expected.ID, email.ID);
                Assert.AreEqual(expected.Sender, email.Sender);
                Assert.AreEqual(expected.Subject, email.Subject);
                Assert.AreEqual(expected.MessageText, email.MessageText);
                Assert.AreEqual(MessageFactory.ProcessingError, null);
            }
            else
            {
                Assert.Fail();
            } 
        }

        [TestMethod]
        public void U7_3()
        {
            MessageProcessingService mps = MessageProcessingService.Instance;

            string id = "E000000001";
            string body = "+44829918273\nwelcome\nThank you for signing up to Topshop emails.\nVisit us at http://topshop.com";

            Message actual = mps.ProcessMessage(id, body);

            Email expected = null;
            string expectedError = "Invalid email sender. The email sender must be a valid email address.";

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedError, MessageFactory.ProcessingError);
        }

    }
}
