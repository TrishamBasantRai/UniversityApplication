using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class UserAccount
    {
        public int UserAccountID { get; set; }
        public string EmailAddress { get; set; }
        public byte[] HashedPassword { get; set; }
        public string UserAccountStatus { get; set; }
        public int RoleID { get; set; } //Foreign Key
    }
}
