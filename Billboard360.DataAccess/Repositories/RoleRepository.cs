using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class RoleRepository
    {
        DatabaseContext db = new DatabaseContext();
        public RoleRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public IQueryable<Role> GetRole(int index)
        {
            if (index == 1)
            {
                return db.Role.Where(x => x.Name == "ADM");
            }
            else if (index == 2)
            {
                return db.Role.Where(x => x.Name == "SPV");
            }
            else if (index == 3)
            {
                return db.Role.Where(x => x.Name == "MDB");
            }
            else
            {
                return db.Role.Where(x => x.Name == "MDO");
            }
        }

        public IQueryable<Role> GetRoleDropDown()
        {
            return db.Role.Where(x => x.DeletedDate == null && x.Name != "ADM");
        }

        
    }
}
