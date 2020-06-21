using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Billboard360.API.Models;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;
using Billboard360.API.Models.Enums;

namespace Billboard360.API.BussinessLogic
{
    public class RegisterBL
    {
        protected readonly DatabaseContext DbContext;

        public RegisterBL(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        public RegisterResponseModel Register(RegisterInputModel data, RoleEnum roleEnum)
        {

            UserRepository userRepo = new UserRepository(DbContext);
            var checkUserName = userRepo.FindByUserName(data.Username);

            if (checkUserName == null)
            {
                Guid id = Guid.NewGuid();
                User temp = new User();

                temp.ID = id;
                temp.PasswordHash = data.Password.ConvertToMD5();
                temp.FirstName = data.FirstName;
                temp.LastName = data.LastName;
                temp.UserName = data.Username;
                temp.PhoneNumber = data.NoTelp;
                temp.CreateDate = DateTime.Now;
                temp.CreateByUserID = id;
                if(roleEnum == RoleEnum.MediaOwner || roleEnum == RoleEnum.MediaBuyer)
                {
                    temp.IsActive = false;
                } else
                {
                    temp.IsActive = true;
                }                

                userRepo = new UserRepository(DbContext);
                var response = userRepo.Insert(temp, false);

                //insert ke table userRole
                UserRole userRole = new UserRole();
                RoleRepository roleRepo = new RoleRepository(DbContext);

                userRole.ID = Guid.NewGuid();
                userRole.UserID = temp.ID;
                //RoleID di ambil dari table role
                userRole.RoleID = roleRepo.GetRole((int)roleEnum).FirstOrDefault().ID;
                userRole.CreateDate = DateTime.Now;
                userRole.CreateByUserID = id;

                UserRoleRepository userRoleRepository = new UserRoleRepository(DbContext);
                var res2 = userRoleRepository.Insert(userRole, false);

                //INSERT TO COMPANY TABLE
                Company dataComp = new Company();

                dataComp.ID = Guid.NewGuid();
                dataComp.NPWP = data.NPWP;
                dataComp.Website = data.Website;
                dataComp.Alamat = data.Alamat;
                dataComp.CompanyName = data.NamaPerusahaan;
                dataComp.Email = data.EmailPerusahaan;
                dataComp.UserID = temp.ID;
                dataComp.Kategory = data.Kategori;
                dataComp.CreateByUserID = id;
                dataComp.CreateDate = DateTime.Now;

                CompanyRepository comRepo = new CompanyRepository(DbContext);
                comRepo.Insert(dataComp, false);

                //INSERT TO TABLE CONTACT
                ContactPerson temCon = new ContactPerson();

                temCon.ID = Guid.NewGuid();
                temCon.CompanyID = dataComp.ID;
                temCon.Email = data.Username;
                temCon.Name = data.FirstName + " " + data.LastName;
                temCon.UserID = temp.ID;
                temCon.CreateDate = DateTime.Now;
                temCon.CreateByUserID = id;

                ContactPersonRepository conRepo = new ContactPersonRepository(DbContext);
                conRepo.Insert(temCon, true);

                RegisterResponseModel result = new RegisterResponseModel();

                result.Message = response.Message;
                result.Response = response.Result;

                RegisterOutputModel output = new RegisterOutputModel();
                output.FirstName = temp.FirstName;
                output.LastName = temp.LastName;
                output.UserName = temp.UserName;
                output.UserID = temp.ID;

                result.data = output;
                result.Message = "Daftar berhasil.";
                result.Response = true;

                return result;
            }
            else
            {
                RegisterResponseModel result = new RegisterResponseModel();
                result.Message = "Email sudah terdaftar!!";
                result.Response = false;

                return result;
            }
        }

    }


}

