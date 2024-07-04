using System.Security.Claims;
using Xunit;

namespace Logger.AzureApplicationInsight.Analytics.UserIdentity.WebApp.Common
{
    /// <summary>
    /// To set a custom value for HttpContext.User.Identity.IsAuthenticated in testing, 
    /// we need to create a fake user and assign it to the HttpContext.
    /// </summary>
    public class FakeIdentityUser2
    {
        [Fact]
        public void TestWithAuthenticatedUser()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            //var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
                Set_UserIdentityToHttpContext(httpContext, true);

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

            Set_UserIdentityToHttpContext(httpContext, false);

            // Act
            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;

            // Assert
            Assert.False(isAuthenticated);
        }


        /// <summary>
        /// Assign the Custom ClaimsPrincipal to HttpContext
        /// </summary>
        public void Set_UserIdentityToHttpContext(HttpContext httpContext, bool isAuthenticated)
        {
            if (httpContext != null)
                httpContext.User = Create_UserIdentity(isAuthenticated);
        }

        public ClaimsPrincipal Create_UserIdentity(bool isAunthenticated)
        {
            var identity = new ClaimsIdentity();

            if (isAunthenticated)
            {
                identity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "testUser"),
                    new Claim(ClaimTypes.NameIdentifier,$"testUser{DateTime.Now.ToString("yyyyMMddHH")}"),
                    new Claim("Email","testuser@abc.com"),
                    new Claim("Mobile","9988776655", ClaimValueTypes.Integer64),
                }, "TestAuthentication");
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);
            return claimsPrincipal;
        }

    }
}
