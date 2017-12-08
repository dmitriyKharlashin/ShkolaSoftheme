using System.ComponentModel.DataAnnotations;

namespace MobileProvider
{
    public class AdminValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var account = value as MobileAccount;
            if (account.Role == UserRoles.Admin)
            {
                return true;
            }

            return false;
        }
    }
}