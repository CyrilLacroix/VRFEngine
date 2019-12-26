using VRFEngine.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using System.Security.Principal;

namespace VRFEngine.Test.Helper
{
    public static class MockHelperFactory
    {
        public static Mock<IHttpContextAccessor> GetHttpContextWithUser(string username = "UserUnitTest")
        {
            Mock<IHttpContextAccessor> httpContextMock = new Mock<IHttpContextAccessor>();
            Mock<IIdentity> identityMock = new Mock<IIdentity>();
            identityMock.SetupGet(x => x.Name).Returns(username);
            Mock<IPrincipal> principalMock = new Mock<IPrincipal>();
            principalMock.SetupGet(x => x.Identity).Returns(identityMock.Object);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(principalMock.Object);
            httpContextMock.SetupGet(x => x.HttpContext.User).Returns(claimsPrincipal);

            return httpContextMock;
        }

        public static Mock<ILoggerService> GetLoggerService()
        {
            return new Mock<ILoggerService>();
        }
    }
}