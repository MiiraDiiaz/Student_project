using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdmissionsCommittee.Data;
using AdmissionsCommittee.Models;
using AdmissionsCommittee.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdmissionsCommittee.Controllers
{
    /// <summary>
    /// Котроллер для личного кабинета
    /// </summary>
    public class StudentController : Controller
    {
        private ContextIdentity db;
        UserManager<User> _userManager;

        public StudentController(ContextIdentity context, UserManager<User> userManager)
        {
            db = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            //Загрузка данных для проверки, если пользователь не вводил данные для профиля, то программа выбрасывает Profile, иначе Details
            var userId = await _userManager.GetUserAsync(User);
            var result = from user in db.Users
                         join userDocument in db.UserDocuments on user.Id equals userDocument.UserId
                         join userProfile in db.UserProfiles on userDocument.UserId equals userProfile.UserId
                         where user.Id==userId.Id
                         select new
                         {
                             Id=user.Id,
                             UserDocumentId= userDocument.UserId,
                             UserProfileId= userProfile.UserId
                         };

            foreach (var item in result)
            {
                model = new ProfileViewModel
                {
                    Id=item.Id,
                    UserId=item.UserProfileId,
                    UserDocumetsId=item.UserDocumentId
                };
            }
            return View(model);
        }
        
        public IActionResult Profile()
        {
            return View();
        }

        /// <summary>
        /// Создание профиля для студента и загрузка документов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser(ProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                UserProfile userProfile = new UserProfile

                {
                    UserId=user.Id,
                    Name = model.Name,
                    Surname = model.Surname,
                    Patronymic = model.Patronymic,
                    Gender = model.Gender,
                    BirthDay = model.BirthDay,
                };
                db.UserProfiles.Add(userProfile);

                UserDocument userDocument = new UserDocument()
                {
                    UserId = user.Id,
                    TypeDocument = model.TypeDocument,
                    SeriesNamberDocument = model.SeriesNamberDocument,
                    DateIssuanceDicument = model.DateIssuanceDicument,
                    IssuanceOffice = model.IssuanceOffice,
                    BirthPlace = model.BirthPlace,
                    Phone = model.Phone
                };
                db.UserDocuments.Add(userDocument);

                UserDocumentFile userDocumentFile = new UserDocumentFile { UserId = user.Id };
                if (model.IdentityDocument != null && model.Photo != null)
                {
                    byte[] imageDocument = null;
                    using (var binaryReader = new BinaryReader(model.IdentityDocument.OpenReadStream()))
                    {
                        imageDocument = binaryReader.ReadBytes((int)model.IdentityDocument.Length);
                    }

                    byte[] imagePhoto = null;
                    using (var binaryReader = new BinaryReader(model.Photo.OpenReadStream()))
                    {
                        imagePhoto = binaryReader.ReadBytes((int)model.Photo.Length);
                    }
                    userDocumentFile.IdentityDocument = imageDocument;
                    userDocumentFile.Photo = imagePhoto;
                }
                db.UserDocumentFiles.Add(userDocumentFile);

                await db.SaveChangesAsync();
                return RedirectToAction("Details");
            }
            return NotFound();
        }

        /// <summary>
        /// Вывод профиля студента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public  async Task<IActionResult> Details(ProfileViewModel model)
        {
            var userId = await _userManager.GetUserAsync(User);
            if (userId != null)
            {
                var user = db.UserProfiles.Join(db.UserDocuments,
                p => p.UserId,
                c => c.UserId,
                (p, c) => new
                {
                    UserId=p.UserId,
                    Name = p.Name,
                    Surname = p.Surname,
                    Patronymic = p.Patronymic,
                    Gender = p.Gender,
                    BirthDay = p.BirthDay,
                    TypeDocument = c.TypeDocument,
                    SeriesNamberDocument = c.SeriesNamberDocument,
                    DateIssuanceDicument = c.DateIssuanceDicument,
                    IssuanceOffice = c.IssuanceOffice,
                    BirthPlace = c.BirthPlace,
                    Phone = c.Phone
                }).Where(w=>w.UserId==userId.Id);
                
                foreach (var item in user)
                {
                    model = new ProfileViewModel
                    {
                        UserId=item.UserId,
                        Name = item.Name,
                        Surname = item.Surname,
                        Patronymic = item.Patronymic,
                        Gender = item.Gender,
                        BirthDay = item.BirthDay,
                        TypeDocument = item.TypeDocument,
                        SeriesNamberDocument = item.SeriesNamberDocument,
                        DateIssuanceDicument = item.DateIssuanceDicument,
                        IssuanceOffice = item.IssuanceOffice,
                        BirthPlace = item.BirthPlace,
                        Phone = item.Phone
                    };
                }
                return View(model);
            }
            return NotFound();
        }

        /// <summary>
        /// Получаем Id для редактирования
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id != null)
            {
                User user = await db.Users.FirstOrDefaultAsync(p => p.Id==id);
                if (user != null)
                {
                    ProfileViewModel model = new ProfileViewModel
                    {
                        UserId = id
                    };
                    return View(model);
                }
            }
            return NotFound();
        }

        /// <summary>
        /// Редактирование профиля
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var userProfile = await db.UserProfiles.FirstOrDefaultAsync(p => p.UserId == user.Id);

            userProfile.Name = model.Name;
            userProfile.Surname = model.Surname;
            userProfile.Patronymic = model.Patronymic;
            userProfile.Gender = model.Gender;
            userProfile.BirthDay = model.BirthDay;
            
            db.UserProfiles.Update(userProfile);

            var userDocument = await db.UserDocuments.FirstOrDefaultAsync(p => p.UserId == user.Id);

            userDocument.TypeDocument = model.TypeDocument;
            userDocument.SeriesNamberDocument = model.SeriesNamberDocument;
            userDocument.DateIssuanceDicument = model.DateIssuanceDicument;
            userDocument.IssuanceOffice = model.IssuanceOffice;
            userDocument.BirthPlace = model.BirthPlace;
            userDocument.Phone = model.Phone;
            
            db.UserDocuments.Update(userDocument);
           
            await db.SaveChangesAsync();
            return RedirectToAction("Details");
        }

        public async Task< IActionResult> Document()
        {
            var user = await _userManager.GetUserAsync(User);
            var userFiles = await db.UserDocumentFiles.FirstOrDefaultAsync(p => p.UserId == user.Id);
            return View(userFiles);
        }
    }
}
