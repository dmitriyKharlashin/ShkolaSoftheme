using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    }
}