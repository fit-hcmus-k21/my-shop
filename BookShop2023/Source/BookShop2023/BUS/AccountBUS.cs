using ProjectMyShop.Config;
using ProjectMyShop.DAO;
using ProjectMyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop.BUS
{
    internal class AccountBUS
    {
        private static AccountBUS _ins;
        private AccountDAO _accountDAO;
        private Account _account;

        private AccountBUS()
        {
            _accountDAO = new AccountDAO();
            _account= new Account();
        }

        public static AccountBUS Instance()
        {
            if (_ins == null)
            {
                _ins = new AccountBUS();

            }
            return _ins;
        }

        public bool Authenticate(string username, string password)
        {

            #region check if user account is exist or not
            return _accountDAO.Authenticate(username, password);
            #endregion

        }

        public void SetUserInfo(string Name, string Role)
        {
            _account.Name = Name;
            _account.Role = Role;
        }

        public string GetName()
        {
            return _account.Name;
        }

        public string GetRole()
        {
            return _account.Role;
        }

    }
}
