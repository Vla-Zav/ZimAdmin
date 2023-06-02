using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZimAdmin.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimAdmin.Classes.Tests
{
    [TestClass()]
    public class LFMTemplateTests
    {
        String lastName, firstName, middleName;
        StringBuilder error = new StringBuilder();
        [TestMethod()]
        public void LFMComplitePassTest()
        {
            lastName = "Шипеков";
            firstName = "Максим";
            middleName = "Олегович";
            
            bool expectedResult = true;
            bool actual = LFMTemplate.LFMComplite(lastName, firstName, middleName, error);

            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod()]
        public void LFMCompliteShortLastNameTest()
        {
            lastName = "Z";
            firstName = "Максим";
            middleName = "Олегович";

            bool expectedResult = false;
            bool actual = LFMTemplate.LFMComplite(lastName, firstName, middleName, error);

            Assert.AreEqual(expectedResult, actual);
        }
        [TestMethod()]
        public void LFMCompliteNullFirstNameTest()
        {
            lastName = "Макиев";
            firstName = "";
            middleName = "Евгеньевич";

            bool expectedResult = false;
            bool actual = LFMTemplate.LFMComplite(lastName, firstName, middleName, error);

            Assert.AreEqual(expectedResult, actual);
        }
        [TestMethod()]
        public void LFMCompliteShortMiddleNameTest()
        {
            lastName = "Жилая";
            firstName = "Полина";
            middleName = "Яник";


            bool expectedResult = false;
            bool actual = LFMTemplate.LFMComplite(lastName, firstName, middleName, error);

            Assert.AreEqual(expectedResult, actual);
        }
    }
}