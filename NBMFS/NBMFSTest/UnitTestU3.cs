using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Napier_Message_Service.Models;

namespace NBMFSTest
{
    /// <summary>
    /// Summary description for UnitTestU3
    /// </summary>
    [TestClass]
    public class UnitTestU3
    {
        //Test ProcessHashtags method in Tweet class
        [TestMethod]
        public void U3_1()
        {
            Tweet tweet = new Tweet("T000000001", "+@test", "Hi @test, how are you?");
            tweet.ProcessMentions();

            List<string> mentions = new List<string>();
            mentions.Add("@test");

            CollectionAssert.AreEqual(mentions, tweet.Mentions);
        }

        [TestMethod]
        public void U3_2()
        {
            Tweet tweet = new Tweet("T000000001", "+@test", "Hi, how are you?");
            tweet.ProcessMentions();

            List<string> mentions = new List<string>();

            CollectionAssert.AreEqual(mentions, tweet.Mentions);
        }

        [TestMethod]
        public void U3_3()
        {
            Tweet tweet = new Tweet("T000000001", "+@test", "Hi @test1 @test2 @test3 @test4 @test5, how are you?");
            tweet.ProcessMentions();

            List<string> mentions = new List<string>();
            mentions.Add("@test1");
            mentions.Add("@test2");
            mentions.Add("@test3");
            mentions.Add("@test4");
            mentions.Add("@test5");

            CollectionAssert.AreEqual(mentions, tweet.Mentions);
        }
    }
}
