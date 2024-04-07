using System;

namespace LegacyApp;

public class UserDataValidationService
{
    private bool ValidateAge(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
        return age < 21;
    }

    private bool ValidateNames(string firstName, string lastName)
    {
        return string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName);
    }

    private bool ValidateEmail(string email)
    {
        return !email.Contains("@") && !email.Contains(".");
    }

    public bool ValidateData(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        return ValidateAge(dateOfBirth) || ValidateNames(firstName, lastName) || ValidateEmail(email);
    }
}