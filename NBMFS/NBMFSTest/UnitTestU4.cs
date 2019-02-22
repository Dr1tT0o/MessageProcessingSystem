using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Napier_Message_Service.Models;

namespace NBMFSTest
{
    /// <summary>
    /// Summary description for UnitTestU4
    /// </summary>
    [TestClass]
    public class UnitTestU4
    {
        //Test ProcessHashtags method in Tweet class
        [TestMethod]
        public void U4_1()
        {
            Tweet tweet = new Tweet("T000000001", "+@test", "Hi, just arrived in Mexico. #travel #thegoodlife");
            tweet.ProcessHashtags();

            List<string> hashtags = new List<string>();
            hashtags.Add("#travel");
            hashtags.Add("#thegoodlife");

            CollectionAssert.AreEqual(hashtags, tweet.Hashtags);
        }

        [TestMethod]
        public void U4_2()
        {
            Tweet tweet = new Tweet("T000000001", "+@test", "Hi, just arrived in Mexico.");
            tweet.ProcessHashtags();

            List<string> hashtags = new List<string>();

            CollectionAssert.AreEqual(hashtags, tweet.Hashtags);
        }

        [TestMethod]
        public void U4_3()
        {
            Tweet tweet = new Tweet("T000000001", "+@test", "Hi, just arrived in Mexico. #travel#thegoodlife");
            tweet.ProcessHashtags();

            List<string> hashtags = new List<string>();
            hashtags.Add("#travel");
            hashtags.Add("#thegoodlife");

            CollectionAssert.AreEqual(hashtags, tweet.Hashtags);
        }

    }
}
