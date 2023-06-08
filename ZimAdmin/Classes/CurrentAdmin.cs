using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimAdmin.Entitys;

namespace ZimAdmin.Classes
{
    internal class CurrentAdmin
    {
        private static Admins _admin = null;
        public static void SetAdmin(Admins currentAdmin)
        {
            _admin = currentAdmin;
        }
        public static Admins GetAdmin()
        {
            if (_admin == null)
                _admin = new Admins();
            return _admin;
        }
    }
}
