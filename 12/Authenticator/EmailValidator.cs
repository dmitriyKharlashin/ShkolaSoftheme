using System;
using System.Net.Mail;

namespace Authenticator
{
    class EmailValidator : AbstractValidator
    {
        public override bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
