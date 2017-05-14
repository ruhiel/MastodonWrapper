using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MastodonWrapper.Client;
using System.Threading.Tasks;
using System.Linq;

namespace MastodonWrapperTest
{
    [TestClass]
    public class MastodonWrapperTest : BaseTest
    {
        [TestMethod]
        public async Task TestGetFollowers()
        {
            var client = new MastodonWrapperClient(_Host, _AccessToken);
            var id = _TestUserID;

            var result = await client.GetFollowers(id);

            var contents = result.Content;

            Assert.IsTrue(contents.Any());
        }
    }
}
