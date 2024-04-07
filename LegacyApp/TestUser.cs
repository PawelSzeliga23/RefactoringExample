using System;

namespace LegacyApp;

public class TestUser : User
{
    public TestUser(int creditLimit,bool hasCredit)
    {
        base.HasCreditLimit = hasCredit;
        base.CreditLimit = CreditLimit;
    }
}