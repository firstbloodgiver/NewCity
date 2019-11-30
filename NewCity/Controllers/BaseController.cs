using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewCity.Data;
using NewCity.Models;

namespace NewCity.Controllers
{
    public class BaseController : Controller
    {
        public readonly SignInManager<IdentityUser> _SignInManager;
        public readonly UserManager<IdentityUser> _userManager;

        
        /// <summary>
        /// NewCityDbContext数据库对象
        /// </summary>
        public readonly NewCityDbContext _context;

        public BaseController(SignInManager<IdentityUser> SignInManager, UserManager<IdentityUser> UserManager, NewCityDbContext context)
        {
            _SignInManager = SignInManager;
            _userManager = UserManager;
            _context = context;
            
        }


        public bool isCreator() {
            return _context.Creator.Where(a => a.UserID == Guid.Parse(GetUserId().ToString())).FirstOrDefault() != null ? true : false;
        }
        /// <summary>
        /// 获取当前用户Guid
        /// </summary>
        /// <returns></returns>
        public Guid GetUserId()
        {
            try
            {
                return Guid.Parse(_userManager.GetUserId(User));
            }
            catch
            {
                return Guid.Empty;
            }
                
        }

    }
}