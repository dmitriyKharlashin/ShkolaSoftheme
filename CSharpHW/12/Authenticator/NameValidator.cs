using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    class NameValidator : AbstractValidator
    {
        public override bool IsValid(string name)
        {
           if (name.Length > 0 && name.Length < 20)
            {
                return true;
            }

            return false;
        }
    }
}
