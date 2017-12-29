using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MobileProvider
{
    public static class ExtentionsModule
    {
        public static bool Validate(this IMobileAccount mobileAccount)
        {
            var results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(mobileAccount);
            var isValid = Validator.TryValidateObject(mobileAccount, context, results, true);
            if (!isValid)
            {
                foreach (var error in results)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(error.ErrorMessage);
                    Console.ResetColor();
                }
            }

            return isValid;
        }

        public static MobileProviderDO ToDataObject(this IMobileProvider mobileProvider)
        {
            return new MobileProviderDO()
            {
                Name = mobileProvider.Name,
                Accounts = (List<MobileAccountDO>)mobileProvider.Accounts.Select(p => p.ToDataObject()).ToList()
            };
        }
        
        public static MobileAccountDO ToDataObject(this IMobileAccount mobileAccount)
        {
            return new MobileAccountDO()
            {
                Name = mobileAccount.Name,
                Surname = mobileAccount.Surname,
                BirthYear = mobileAccount.BirthYear,
                Number = mobileAccount.Number,
                Email = mobileAccount.Email,
                Addresses = mobileAccount.Addresses
            };
        }

        public static IMobileAccount FromDataObject(this MobileAccountDO mobileAccountDO)
        {
            return new MobileAccount((string)mobileAccountDO.Name, (string)mobileAccountDO.Surname, (string)mobileAccountDO.Email, (int)mobileAccountDO.BirthYear)
            {
                Number = mobileAccountDO.Number
            };
        }
    }
}