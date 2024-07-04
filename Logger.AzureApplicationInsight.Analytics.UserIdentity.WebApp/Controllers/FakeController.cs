using Logger.AzureApplicationInsight.Analytics.UserIdentity.WebApp.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Security.Claims;
using Xunit;

namespace Logger.AzureApplicationInsight.Analytics.UserIdentity.WebApp.Controllers
{
    public class FakeController : Controller
    {
        public IActionResult Index()
        {
            var name = User.Identity.Name;

            return View();
        }

        [Fact]
        public void SampleTest()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, "SomeValueHere"),
            new Claim(ClaimTypes.Name, "gunnar@somecompany.com")
                // other required and custom claims
            }, "TestAuthentication"));

            //var logger = new NullLogger<HomeController>();
            //var controller = new HomeController(logger);
            var controller = new FakeController();
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            controller.Index();
        }



        [Fact]
        public static void TestWithAuthenticatedUser()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            SetUser(httpContext, true);

            //Act
            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;

            //Assert
            Assert.True(isAuthenticated);
        }

        [Fact]
        public void TestWithUnauthenticatedUser()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            SetUser(httpContext, false);

            // Act
            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;

            // Assert
            Assert.False(isAuthenticated);
        }





        public static ClaimsPrincipal Create_FakeUserDetails(bool isAunthenticated)
        {
            var identity = new ClaimsIdentity();

            if (isAunthenticated)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, $"testUser{DateTime.Now.ToString("yyyyMMddHH")}"),
                    new Claim(ClaimTypes.NameIdentifier,"1"),
                    new Claim("Email","testuser@abc.com"),
                    new Claim("Mobile","9988776655", ClaimValueTypes.Integer64),
                }, "TestAuthentication");
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);
            return claimsPrincipal;
        }

        /// <summary>
        /// Assign the Custom ClaimsPrincipal to HttpContext
        /// </summary>
        public static void SetUser(HttpContext httpContext, bool isAuthenticated)
        {
            var claimsPrincipal = Create_FakeUserDetails(isAuthenticated);
            httpContext.User = claimsPrincipal;
        }

    }
}
