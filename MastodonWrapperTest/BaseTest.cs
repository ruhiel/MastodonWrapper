using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace MastodonWrapperTest
{
    [TestClass]
    public class BaseTest
    {
        protected static string _AccessToken;
        protected static string _Host;
        protected static int _TestUserID;
        protected static int _TestTargetUserId;
        protected static string _TestPicture;
        protected static string _RemoteUser;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            // アセンブリ内のすべてのテストが実行される前に、アセンブリによって取得されるリソースを割り当てるために使用されるコードを含むメソッドを識別します。 
            Trace.WriteLine("AssemblyInit " + context.TestName);

            _AccessToken = Environment.GetEnvironmentVariable("TestAuthCode");

            _Host = Environment.GetEnvironmentVariable("TestHost");

            _TestUserID = int.Parse(Environment.GetEnvironmentVariable("TestUserID"));

            _TestTargetUserId = int.Parse(Environment.GetEnvironmentVariable("TestTargetUserId"));

            _TestPicture = Environment.GetEnvironmentVariable("TestPicture");

            _RemoteUser = Environment.GetEnvironmentVariable("RemoteUser");
        }
    }
}
