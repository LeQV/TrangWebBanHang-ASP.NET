using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021302.DomainModels;

namespace _19T1021302.Datalayers
{
    public interface IUserAccountDAL
    {
        UserAccount Acthorize(String userName, string password);
        bool ChangePassword(string userName, string oldPassword, string newPassword);

        bool ExistAccount(string key);
        string ReturnAccountInformation(string email);

        bool Register(UserAccount user);
    }
}
