namespace LegacyApp;

public class UserCreditValidationManager
{
    public static bool CheckCredit(User user)
    {
        return user.HasCreditLimit == true && user.CreditLimit < 500;
    }
}