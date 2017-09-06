using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class AgeService : IAgeService
    {
        public int CalculateAge(DateTime birthDate, DateTime checkTime)
        {
            var age = checkTime.Year - birthDate.Year;
            // Go back to the year the person was born in case of a leap year
            if (birthDate > checkTime.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}
