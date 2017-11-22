using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    class Database : IDataInfo
    {
        static private Database instance = null;
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

        private Database()
        {
            if (_users == null)
            {
                _users = new IUser[16];
            }
        }

        static public Database GetInstance()
        {
            return instance == null ?
                instance = new Database() :
                instance;
        }

        public IUser FindByEmail(string email)
        {
            return _users.FirstOrDefault(p => p.Email == email);
        }

        public IUser FindByName(string name)
        {
            return _users.FirstOrDefault(p => p.Name == name);
        }

        public void Add(IUser user)
       {
            EnlargeIfFull();
            _users[Length()] = user;
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

        public void DisplayFullInfo()
        {
            foreach (User user in _users)
            {
                user.DisplayFullInfo();
            }
        }
    }
}
