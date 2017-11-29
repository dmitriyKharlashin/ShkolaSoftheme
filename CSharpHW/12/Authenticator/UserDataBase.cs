using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    class UserDataBase : IUserDataBase
    {
        private bool _disposed;
        static private UserDataBase instance = null;
        private IUser[] _users;
        
        public int Length()
        {
            return GetLastNotEmptyIndex() + 1;
        }

        public IUser this[int index]
        {
            get
            {
                return _users[index];
            }
            set
            {
                _users[index] = value;
            }
        }

        private UserDataBase()
        {
            if (_users == null)
            {
                _users = new IUser[16];
            }
        }

        ~UserDataBase()
        {
            Dispose(false);
        }

        static public UserDataBase GetInstance()
        {
            return instance == null ?
                instance = new UserDataBase() :
                instance;
        }

        public IUser FindByEmail(string email)
        {
            for (var i = 0; i < Length(); i++)
            {
                if (_users[i] != null &&_users[i].Email == email)
                {
                    return _users[i];
                }
            }
            return null;
        }

        public IUser FindByName(string name)
        {
            for (var i = 0; i < Length(); i++)
            {
                if (_users[i] != null && _users[i].Name == name)
                {
                    return _users[i];
                }
            }
            return null;
        }

        public void Add(IUser user)
       {
            EnlargeIfFull();
            _users[Length()] = user;
            Console.WriteLine("New user was added!");
       }
        protected int GetLastNotEmptyIndex()
        {
            for (var i = _users.Length - 1; i >= 0; i--)
            {
                if (_users[i] != null)
                {
                    return i;
                }
            }
            return -1;
        }

        private void EnlargeIfFull()
        {
            if (_users.Length == Length())
            {
                var bigger = Length() + 16;

                var moreUsers = new IUser[bigger];
                _users.CopyTo(moreUsers, 0);
                _users = moreUsers;
            }
        }

        public IUser[] GetUsers()
        {
            return _users;
        }

        public void DisplayFullInfo()
        {
            Console.WriteLine("List of added users:");
            foreach (IUser user in GetUsers())
            {
                if(user != null)
                {
                    user.DisplayFullInfo();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_users != null)
                    {
                        DisplayFullInfo();
                    }
                }

                _disposed = true;
            }
        }
    }
}
