using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Napier_Message_Service.Models;


namespace NBMFSTest
{
    /// <summary>
    /// Summary description for UnitTestU6
    /// </summary>
    [TestClass]
    public class UnitTestU6
    {
        //Test CreateMessage method in MessageFactory class
        [TestMethod]
        public void U6_1()
        {
            string id = "S000000001";
            string body = "+44829918273\nHi Bob, we have a great sale on at the moment, 50 % off!";

            Message actual = MessageFactory.CreateMessage(id, body);

            SMS expected = new SMS("S000000001", "+44829918273", "Hi Bob, we have a great sale on at the moment, 50 % off!");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Sender, actual.Sender);
            Assert.AreEqual(expected.MessageText, actual.MessageText);
            Assert.AreEqual(MessageFactory.ProcessingError, null);
        }

        [TestMethod]
        public void U6_2()
        {
            string id = "T000000001";
            string body = "@BestCompany\nCongratulations to @RobJones in accounting for winning our #NFL football pool!";

            Message actual = MessageFactory.CreateMessage(id, body);

            Tweet expected = new Tweet("T000000001", "@BestCompany", "Congratulations to @RobJones in accounting for winning our #NFL football pool!");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Sender, actual.Sender);
            Assert.AreEqual(expected.MessageText, actual.MessageText);
            Assert.AreEqual(MessageFactory.ProcessingError, null);
        }

        [TestMethod]
        public void U6_3()
        {
            string id = "E000000001";
            string body = "noreply@topshop.com\nwelcome\nThank you for signing up to Topshop emails.\nYou'll now be the first to know about our latest collections.";

            Message actual = MessageFactory.CreateMessage(id, body);

            Email expected = new Email("E000000001", "noreply@topshop.com", "welcome", "Thank you for signing up to Topshop emails.\nYou'll now be the first to know about our latest collections.");

            Assert.AreEqual(expected.GetType(), actual.GetType());
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Sender, actual.Sender);
            Assert.AreEqual(expected.MessageText, actual.MessageText);
            Assert.AreEqual(MessageFactory.ProcessingError, null);
        }

        [TestMethod]
        public void U6_4()
        {
            string id = "E000000002";
            string body = "police@metro.uk\nSIR 12/05/18\n22-72-87\nCustomer attack\nA customer has just been attacked in our branch in Edinburgh.";

            Message sir = MessageFactory.CreateMessage(id, body);

            SIR expected = new SIR("E000000002", "police@metro.uk", "SIR 12/05/18", "22-72-87", "Customer attack", "A customer has just been attacked in our branch in Edinburgh.");

            if (sir is SIR actual)
            {
                Assert.AreEqual(expected.GetType(), actual.GetType());
                Assert.AreEqual(expected.ID, actual.ID);
                Assert.AreEqual(expected.Sender, actual.Sender);
                Assert.AreEqual(expected.SortCode, actual.SortCode);
                Assert.AreEqual(expected.NatureOfIncident, actual.NatureOfIncident);
                Assert.AreEqual(expected.MessageText, actual.MessageText);
                Assert.AreEqual(null, MessageFactory.ProcessingError);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void U6_5()
        {
            string id = "012345";
            string body = "+44829918273\nHi Bob, we have a great sale on at the moment, 50 % off!!Get down to our store or visit us here www.link.com whilst stocks last";

            Message actual = MessageFactory.CreateMessage(id, body);
            string actualProcessingError = "Invalid message id. Please enter a valid message id.";

            Assert.AreEqual(null, actual);
            Assert.AreEqual(actualProcessingError, MessageFactory.ProcessingError);
        }

    }
}
