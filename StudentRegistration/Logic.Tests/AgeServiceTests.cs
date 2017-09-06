using Data;
using Logic.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;
using System;

namespace Logic.Tests
{
    [TestClass]
    public class AgeServiceTests
    {
        
        [TestMethod]
        public void CalculateAge_NoLeapYear()
        {
            var studentService = new AgeService();
            var birthDate = new DateTime(2015,1,1);
            var checkTime = DateTime.Today;

            var age = studentService.CalculateAge(birthDate, checkTime);

            age.ShouldBe(DateTime.Today.Year - birthDate.Year);
        }

        [TestMethod]
        public void CalculateAge_LeapYear()
        {
            var studentService = new AgeService();
            var birthDate = new DateTime(2010, 3, 1);
            var checkTime = new DateTime(2012, 2, 29);

            var age = studentService.CalculateAge(birthDate, checkTime);

            age.ShouldBe(checkTime.Year - birthDate.Year -1);
        }
    }
}
