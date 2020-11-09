using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdmissionsCommittee.Models;
using Microsoft.AspNetCore.Identity;
using AdmissionsCommittee.ViewModel;
using AdmissionsCommittee.ViewModels;

namespace AdmissionsCommittee.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public AccountController(RoleManager<IdentityRole> roleManager,UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

       [HttpGet]
       public IActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register (RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                //создание юзерв
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
                var result = await _userManager.CreateAsync(user, model.Password);
                

                if (result.Succeeded)//при успешном входе идет переадресация на домашнюю страницу
                {
                    //создание роли поумолчанию "Студент"
                    User userRole = await _userManager.FindByEmailAsync(model.Email);
                    await _userManager.AddToRoleAsync(userRole, "student");
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Student");//тут будет переадресация в личный кабинет
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        /// <summary>
        /// Вход
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login(string returnUrl=null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        //проверяем роли и направляем по личным кабинетам
                        User user = await _userManager.FindByEmailAsync(model.Email);
                        var userRoles = await _userManager.GetRolesAsync(user);

                        foreach( var role in userRoles) 
                        { 
                            if (role=="admin")
                             {
                                return RedirectToAction("Index", "Roles");//личный абинет администратора-методиста
                             }
                            else
                             { 
                                return RedirectToAction("Index", "Student");//линый кабинет студента
                             }  
                        }                            
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }               
            }
           return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
