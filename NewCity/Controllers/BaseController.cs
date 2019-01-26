using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewCity.Data;

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

        /// <summary>
        /// 获取当前用户Guid
        /// </summary>
        /// <returns></returns>
        public Guid GetUserId() {
            return Guid.Parse(_userManager.GetUserId(User));
        }

        /// <summary>
        /// 是否该故事系列的作者
        /// </summary>
        /// <param name="AuthorID"></param>
        /// <returns></returns>
        public bool IsAuthor(Guid AuthorID) {
            return true;
        }

    }
}