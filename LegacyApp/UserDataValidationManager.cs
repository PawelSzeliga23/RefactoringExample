﻿using System;

namespace LegacyApp;

public class UserDataValidationManager
{
    public static bool ValidateAge(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
        return age < 21;
    }

    public static bool ValidateNames(string firstName, string lastName)
    {
        return string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName);
    }

    public static bool ValidateEmail(string email)
    {
        return !email.Contains("@") && !email.Contains(".");
    }
    
}