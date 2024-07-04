using Logger.AzureApplicationInsight.Analytics.UserIdentity.WebApp.Common;
using Logger.AzureApplicationInsight.Analytics.UserIdentity.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Xunit;

namespace Logger.AzureApplicationInsight.Analytics.UserIdentity.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        //[Authorize]
        public IActionResult Index()
        {
            var name = User.Identity.Name;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LoginFake()
        {
            //Test1();
            Test2();
            //Test3();
            //Test4();       

            var name = User.Identity.Name;

            if (name != null)
                return RedirectToAction("Index", "Home");
            else
                return RedirectToAction("Login", "Home");
        }

        [Fact]
        public void Test1()
        {
            var userProfile = new ClaimsPrincipal(new ClaimsIdentity(
                                    new Claim[]
                                    {
                                        new Claim(ClaimTypes.Name, "testUser"),
                                        new Claim(ClaimTypes.NameIdentifier,$"testUser{DateTime.Now.ToString("yyyyMMddHH")}"),
                                        new Claim("Email","testuser@abc.com"),
                                        new Claim("Mobile","9988776655", ClaimValueTypes.Integer64),
                                    }, "TestAuthentication"));

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
                httpContext.User = userProfile;

        }

        [Fact]
        public void Test2()
        {
            var obj1 = new FakeIdentityUser(_httpContextAccessor);
            obj1.Set_UserIdentityToHttpContext(_httpContextAccessor.HttpContext, true);
            var name1 = User.Identity.Name;
        }

        [Fact]
        public void Test3()
        {
            var obj = new FakeIdentityUser(null);
            obj.Set_UserIdentityToHttpContext(new DefaultHttpContext(), true);
            var name = User.Identity.Name;
        }

        [Fact]
        public void Test4()
        {
            var obj = new FakeIdentityUser(null);
            obj.Set_UserIdentityToHttpContext(this.HttpContext, true);
            var name = User.Identity.Name;
        }

    }
}
