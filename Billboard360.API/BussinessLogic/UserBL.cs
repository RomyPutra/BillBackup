using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billboard360.API.Models;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;


namespace Billboard360.API.BussinessLogic
{
    public class UserBL
    {

        protected readonly DatabaseContext db;

        public UserBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public List<ListUserOutputModel> GetListUser(ListUserInputModel data)
        {


            var baseQuery = (from u in db.User
                             join ur in db.UserRole on u.ID equals ur.UserID
                             join r in db.Role on ur.RoleID equals r.ID
                             from c in db.Company.Where(x => x.UserID == u.ID).DefaultIfEmpty()
                             select new ListUserOutputModel
                             {
                                 UserID = u.ID,
                                 CompanyName = c.CompanyName,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 UserName = u.UserName,
                                 RoleID = r.ID,
                                 RoleName = r.NormalizedName,
                                 IsActive = u.IsActive,
                                 Address = c.Alamat,
                                 Website = c.Website,
                                 EmailPerusahaan = c.Email,
                                 Kategory = c.Kategory,
                                 NPWP = c.NPWP,
                                 Note = c.Note,
                                 CompanyPhone = u.PhoneNumber,
                             });

            if (data.UserName != null && data.UserName != string.Empty)
            {
                baseQuery = (from fu in baseQuery
                             where fu.UserName.Contains(data.UserName)
                             select new ListUserOutputModel
                             {
                                 UserID = fu.UserID,
                                 CompanyName = fu.CompanyName,
                                 FirstName = fu.FirstName,
                                 LastName = fu.LastName,
                                 UserName = fu.UserName,
                                 RoleID = fu.RoleID,
                                 RoleName = fu.RoleName,
                                 IsActive = fu.IsActive,
                                 Address = fu.Address,
                                 Website = fu.Website,
                                 EmailPerusahaan = fu.EmailPerusahaan,
                                 Kategory = fu.Kategory,
                                 NPWP = fu.NPWP,
                                 Note = fu.Note,
                                 CompanyPhone = fu.CompanyPhone,
                             });

            }

            if (data.CompanyName != null && data.CompanyName != string.Empty)
            {
                baseQuery = (from fu in baseQuery
                             where fu.CompanyName.Contains(data.CompanyName)
                             select new ListUserOutputModel
                             {
                                 UserID = fu.UserID,
                                 CompanyName = fu.CompanyName,
                                 FirstName = fu.FirstName,
                                 LastName = fu.LastName,
                                 UserName = fu.UserName,
                                 RoleID = fu.RoleID,
                                 RoleName = fu.RoleName,
                                 IsActive = fu.IsActive,
                                 Address = fu.Address,
                                 Website = fu.Website,
                                 EmailPerusahaan = fu.EmailPerusahaan,
                                 Kategory = fu.Kategory,
                                 NPWP = fu.NPWP,
                                 Note = fu.Note,
                                 CompanyPhone = fu.CompanyPhone,
                             });

            }

            if (data.RoleName != null && data.RoleName != string.Empty)
            {
                baseQuery = (from fu in baseQuery
                             where fu.RoleName.Contains(data.RoleName)
                             select new ListUserOutputModel
                             {
                                 UserID = fu.UserID,
                                 CompanyName = fu.CompanyName,
                                 FirstName = fu.FirstName,
                                 LastName = fu.LastName,
                                 UserName = fu.UserName,
                                 RoleID = fu.RoleID,
                                 RoleName = fu.RoleName,
                                 IsActive = fu.IsActive,
                                 Address = fu.Address,
                                 Website = fu.Website,
                                 EmailPerusahaan = fu.EmailPerusahaan,
                                 Kategory = fu.Kategory,
                                 NPWP = fu.NPWP,
                                 Note = fu.Note,
                                 CompanyPhone = fu.CompanyPhone,
                             });

            }

            if (data.PageSize == 0)
            {
                data.PageSize = 5;
            }

            if (data.PageNumber == 0)
            {
                data.PageNumber = 1;
            }

            int pageNumber = data.PageNumber - 1;

            baseQuery = baseQuery.Skip(pageNumber * data.PageSize).Take(data.PageSize);

            return baseQuery.ToList();
        }

        public List<ListUserOutputModel> EnabledDisabledUser(UserEnableDisabledInputModel data)
        {
            UserRepository userRepo = new UserRepository(db);

            bool isActive = false;

            if (data.IsActive)
            {
                isActive = false;
            }
            else
            {
                isActive = true;
            }

            var res = userRepo.EnableDisabled(data.CurrentUserIDLogin, data.UserID, isActive);

            List<ListUserOutputModel> newList = new List<ListUserOutputModel>();

            if (res.Result)
            {
                ListUserInputModel input = new ListUserInputModel();
                input.PageNumber = 1;
                input.PageSize = 5;
                newList = GetListUser(input);
            }

            return newList;
        }

        public string CheckRole(Guid userID)
        {
            var result = (from ur in db.UserRole
                          join r in db.Role on ur.RoleID equals r.ID
                          where ur.UserID == userID
                          select new
                          {
                              RoleName = r.Name
                          }).FirstOrDefault();

            return result.RoleName;
        }

        public string CheckRole(string userName)
        {
            var result = (from u in db.User
                          join ur in db.UserRole on u.ID equals ur.UserID
                          join r in db.Role on ur.RoleID equals r.ID
                          where u.UserName == userName
                          select new
                          {
                              RoleName = r.Name
                          }).FirstOrDefault();

            return result.RoleName;
        }
    }
}
