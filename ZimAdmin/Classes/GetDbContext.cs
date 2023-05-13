using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimAdmin.Entitys;

namespace ZimAdmin.Classes
{
    internal class GetDbContext
    {
        private static Privat_HospitalEntities _context;
        public static Privat_HospitalEntities GetContext()
        {
            if (_context == null)
                _context = new Privat_HospitalEntities();
            return _context;
        }
    }
}
