using System;

namespace LegacyApp
{
    public class UserService
    {
        private UserDataValidationService ValidationService;
        private ClientRepository ClientRepository;
        private UserCreditValidationService CreditValidationService;

        public UserService()
        {
            this.ValidationService = new UserDataValidationService();
            this.ClientRepository = new ClientRepository();
            this.CreditValidationService = new UserCreditValidationService();
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (ValidationService.ValidateData(firstName, lastName, email, dateOfBirth))
            {
                return false;
            }

            var client = ClientRepository.GetById(clientId);
            
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            user = PrepareUser(user, client);

            if (CreditValidationService.CheckCredit(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
        
        private User PrepareUser(User user, Client client)
        {
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                user.CreditLimit = ImportantUserCreditLimit(new UserCreditService(), user);
            }
            else
            {
                user.HasCreditLimit = true;
                user.CreditLimit = NormalUserCreditLimit(new UserCreditService(), user);
            }
            return user;
        }

        private int ImportantUserCreditLimit(UserCreditService userCreditService, User user)
        {
            using (userCreditService)
            {
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                return creditLimit;
            }
        }

        private int NormalUserCreditLimit(UserCreditService userCreditService, User user)
        {
            using (userCreditService)
            {
                return userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            }
        }

        public int ImportantUserCreditLimitTest(UserCreditService userCreditService, User user)
        {
            return ImportantUserCreditLimit(userCreditService, user);
        }
        public int NormalUserCreditLimitTest(UserCreditService userCreditService, User user)
        {
            return NormalUserCreditLimit(userCreditService, user);
        }
    }
}