namespace LegacyApp;

public class UserCreditValidationService
{
    public bool CheckCredit(User user)
    {
        return user.HasCreditLimit && user.CreditLimit < 500;
    }
}