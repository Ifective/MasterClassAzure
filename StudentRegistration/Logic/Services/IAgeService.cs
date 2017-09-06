using System;

namespace Logic.Services
{
    public interface IAgeService
    {
        int CalculateAge(DateTime birthDate, DateTime checkTime);
    }
}