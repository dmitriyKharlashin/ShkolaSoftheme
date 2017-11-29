using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    abstract class AbstractValidator
    {
        abstract public bool IsValid(string login);
        public void Validate(string name)
        {
            bool isValid = IsValid(name);
            if (!isValid)
            {
                throw new Exception("Name should be string with less than 20 digits");
            }
        }
    }
}
