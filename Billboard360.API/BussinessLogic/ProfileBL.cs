using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billboard360.API.Models;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;
using GeoCoordinatePortable;


namespace Billboard360.API.BussinessLogic
{
    public class ProfileBL
    {
        protected readonly DatabaseContext db;

        public ProfileBL(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public ProfileOutputModel GetProfile(Guid userID)
        {
            UserRepository repo = new UserRepository(db);

            var data = repo.FindByID(userID).FirstOrDefault();

            ProfileOutputModel output = new ProfileOutputModel();

            output.EmailPerusahaan = data.UserName;
            output.UserName = data.UserName;
            output.PhotoUrl = data.PhotoUrl;
            output.NoTelp = data.PhoneNumber;
            output.LastName = data.LastName;
            output.FirstName = data.FirstName;
            output.UserID = data.ID;

            return output;
        }

        public ProfileOutputModel GetCompleteProfie(Guid userID)
        {
            var query = (from u in db.User
                         join c in db.Company on u.ID equals c.UserID into joinedT
                         from uc in joinedT.DefaultIfEmpty()
                         where u.ID == userID
                         select new ProfileOutputModel()
                         {
                             EmailPerusahaan = uc.Email == null ? string.Empty : uc.Email,
                             EmailPIC = u.UserName,
                             Alamat = uc.Alamat == null ? string.Empty : uc.Alamat,
                             Catatan = uc.Note == null ? string.Empty : uc.Note,
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             NamaPerusahaan = uc.CompanyName == null ? string.Empty : uc.CompanyName,
                             NoTelp = u.PhoneNumber,
                             NPWP = uc.NPWP == null ? string.Empty : uc.NPWP,
                             PhotoUrl = u.PhotoUrl,
                             UserID = u.ID,
                             UserName = u.UserName,
                             Website = uc.Website == null ? string.Empty : uc.Website,
                             Kategori = uc.Kategory == null ? string.Empty : uc.Kategory,
                             CountCart = db.Cart.Where(x => x.UserID == userID && x.DeletedDate == null && x.BookID == Guid.Empty).Count(),
                             IsHaveUserBank = db.UserBank.Count(x => x.UserID == userID) > 0 ? true : false
                         }) ;

            return query.FirstOrDefault();
        }

        public EditProfileOutputModel EditProfile(EditProfileInputModel data)
        {
            DateTime? today = DateTime.Now;

            User user = new User();
            user.FirstName = data.FirstName;
            user.LastName = data.LastName;
            user.ID = data.UserID;
            user.UserName = data.UserName;
            user.PhoneNumber = data.NoTelp;
            user.LastUpdateByUserID = data.UserID;
            user.LastUpdateDate = today;

            UserRepository userRepo = new UserRepository(db);
            userRepo.Update(user, false);

            Company comp = new Company();
            comp.UserID = data.UserID;
            comp.NPWP = data.NPWP;
            comp.Website = data.Website;
            comp.Alamat = data.Alamat;
            comp.CompanyName = data.NamaPerusahaan;
            comp.Email = data.EmailPerusahaan;
            comp.Note = data.Catatan;
            comp.Kategory = data.Kategori;
            
            
            CompanyRepository compRepo = new CompanyRepository(db);
            compRepo.Update(comp, false);
            
            ContactPerson cp = new ContactPerson();
            cp.Email = data.EmailPIC;
            cp.Name = data.FirstName + " " +data.LastName;
            cp.UserID = data.UserID;

            ContactPersonRepository cpRepo = new ContactPersonRepository(db);
            cpRepo.Update(cp, true);


            var query = (from u in db.User
                         join c in db.Company on u.ID equals c.UserID
                         where u.ID == data.UserID
                         select new EditProfileOutputModel()
                         {
                             EmailPerusahaan = c.Email,
                             EmailPIC = u.UserName,
                             Alamat = c.Alamat,
                             Catatan = c.Note,
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             NamaPerusahaan = c.CompanyName,
                             NoTelp = u.PhoneNumber,
                             NPWP = c.NPWP,
                             PhotoUrl = u.PhotoUrl,
                             UserID = u.ID,
                             UserName = u.UserName,
                             Website = c.Website,
                             Kategori = c.Kategory
                         });
            return query.FirstOrDefault();
        }
    }

    
}
