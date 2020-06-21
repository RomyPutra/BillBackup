using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class CompanyCategoryRepository
    {
        DatabaseContext db = new DatabaseContext();
        public CompanyCategoryRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public IQueryable<CompanyCategory> GetCompanyCategory()
        {
            return db.CompanyCategory.Where(x => x.DeletedDate == null);
        }
    }
}
