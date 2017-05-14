using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using MastodonWrapper.Client;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using MastodonWrapper.Entity;

namespace MastodonWrapperTest
{
    [TestClass]
    public class DynamicMastodonTest : BaseTest
    {
        [TestMethod]
        public async Task TestGetAccount()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var id = _TestUserID;

            var result = await client.GetAccount(id);

            Assert.IsNotNull(result.username);
        }

        [TestMethod]
        public async Task TestGetCurrentAccount()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var id = _TestUserID;

            var result = await client.GetCurrentAccount();

            Assert.IsNotNull(result.username);
        }

        [TestMethod]
        public async Task TestUpdateAccount()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var id = _TestUserID;

            var account = await client.GetCurrentAccount();

            var result = await client.UpdateAccount(account.display_name, account.note);

            Assert.IsNotNull(result.display_name);
        }

        [TestMethod]
        public async Task TestGetFollowers()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var id = _TestUserID;

            var result = await client.GetFollowers(id);

            var contents = result.Content;

            Assert.IsTrue(contents.Any());
        }

        [TestMethod]
        public async Task TestGetFollowing()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var id = _TestUserID;

            var result = await client.GetFollowing(id);

            var contents = result.Content;

            Assert.IsTrue(contents.Any());
        }

        [TestMethod]
        public async Task TestGetAccountsStatuses()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var id = _TestUserID;

            var result = await client.GetAccountsStatuses(id);

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task TestFollow()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var id = _TestTargetUserId;

            var result = await client.Follow(id);

            Assert.AreEqual(_TestTargetUserId, (int)result.id);
        }

        [TestMethod]
        public async Task TestUnfollow()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var id = _TestTargetUserId;

            var result = await client.Unfollow(id);

            Assert.AreEqual(_TestTargetUserId, (int)result.id);
        }

        [TestMethod]
        public async Task TestBlock()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var id = _TestTargetUserId;

            var result = await client.Block(id);

            Assert.AreEqual(_TestTargetUserId, (int)result.id);

            result = await client.UnBlock(id);

            Assert.AreEqual(_TestTargetUserId, (int)result.id);
        }

        [TestMethod]
        public async Task TestMute()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var id = _TestTargetUserId;

            var result = await client.Mute(id);

            Assert.AreEqual(_TestTargetUserId, (int)result.id);

            result = await client.UnMute(id);

            Assert.AreEqual(_TestTargetUserId, (int)result.id);
        }

        [TestMethod]
        public async Task TestGetRelationships()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var id = _TestUserID;

            var result = await client.GetRelationships(id);

            Assert.IsTrue(result.Any());

            result = await client.GetRelationships(new[] { _TestUserID, _TestTargetUserId });

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task TestSearchAccount()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var id = _TestUserID;

            var account = await client.GetCurrentAccount();

            dynamic[] result = await client.SearchAccount(account.username);

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task TestRegister()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);

            var result = await client.Register("MastodonClient", OAuthScope.of(Scope.Read, Scope.Write, Scope.Follow));

            Assert.IsNotNull(result.id);

            Assert.IsNotNull(result.client_id);

            Assert.IsNotNull(result.client_secret);

            Assert.IsNotNull(result.auth_url);

            Process.Start((string)result.auth_url);
        }

        [TestMethod]
        public async Task TestGetBlocks()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var id = _TestTargetUserId;

            await client.Block(id);

            var result = await client.GetBlocks();

            Assert.IsTrue(result.Content.Any());

            await client.UnBlock(id);
        }

        [TestMethod]
        public async Task TestGetFavourites()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);

            var result = await client.GetFavourites();

            Assert.IsTrue(result.Content.Any());
        }

        [TestMethod]
        public async Task TestAuthorizeFollowRequest()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);

            var requests = await client.GetFollowRequests();

            Assert.IsTrue(requests.Content.Any());

            var id = (int)requests.Content.First().id;

            await client.AuthorizeFollowRequest(id);
        }

        [TestMethod]
        public async Task TestRejectFollowRequest()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);

            var requests = await client.GetFollowRequests();

            Assert.IsTrue(requests.Content.Any());

            var id = (int)requests.Content.First().id;

            await client.RejectFollowRequest(id);
        }

        [TestMethod]
        public async Task TestFollowRemoteUser()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);

            var result = await client.FollowRemoteUser(_RemoteUser);

            Assert.IsNotNull(result.id);
        }

        [TestMethod]
        public async Task TestGetInstance()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);

            var result = await client.GetInstance();

            Assert.IsNotNull(result.uri);
        }

        [TestMethod]
        public async Task TestGetMutes()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);

            var result = await client.GetMutes();

            Assert.IsTrue(result.Content.Any());
        }

        [TestMethod]
        public async Task TestGetNotifications()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);

            var result = await client.GetNotifications();

            Assert.IsTrue(result.Content.Any());

            var id = (int)result.Content.First().id;

            var notification = await client.GetNotification(id);

            Assert.AreEqual(id, notification.id);
        }

        [TestMethod]
        public async Task TestClearNotifications()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);

            await client.ClearNotifications();
        }

        [Ignore]
        public async Task TestGetReports()
        {
        }

        [Ignore]
        public async Task TestReport()
        {
        }

        [TestMethod]
        public async Task TestSearch()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var result = await client.Search("超会議");
            var statuses = (object[])result.hashtags;

            Assert.IsTrue(statuses.Any());
        }

        [Ignore]
        public async Task TestGetStatusCard()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);

            int id = 0;
            var card = await client.GetStatusCard(id);

            Assert.IsNotNull(card.url);
        }

        [TestMethod]
        public async Task TestPostStatus()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);

            var media = await client.UploadMedia(new FileInfo(_TestPicture));

            Assert.IsNotNull(media.url);

            var status = await client.PostStatus(status: "画像投稿テスト", media_ids: new[] { (int)media.id });

            Assert.IsNotNull(status.content);

            var id = (int)status.id;

            status = await client.GetStatus(id);

            Assert.IsNotNull(status.content);

            var context = await client.GetStatusContext(id);

            Assert.IsNotNull(context.ancestors);

            await client.DeleteStatus((int)status.id);
        }

        [TestMethod]
        public async Task TestReblog()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);

            var statuses = await client.GetAccountsStatuses(_TestTargetUserId);

            var id = (int)statuses.First().id;

            var status = await client.Reblog(id);

            Assert.IsNotNull(status.uri);

            status = await client.Favourite(id);

            Assert.IsNotNull(status.uri);

            var accounts = await client.GetRebloggedStatus(id);

            Assert.IsTrue(accounts.Any());

            accounts = await client.GetFavouritedStatus(id);

            Assert.IsTrue(accounts.Any());

            status = await client.UnReblog(id);

            Assert.IsNotNull(status.uri);

            status = await client.UnFavourite(id);

            Assert.IsNotNull(status.uri);
        }

        [TestMethod]
        public async Task TestGetHomeTimeline()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);

            var result = await client.GetHomeTimeline(limit: 20);

            Assert.AreEqual(20, result.Content.Length);
        }

        [TestMethod]
        public async Task TestGetPublicTimeline()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);
            var result = await client.GetPublicTimeline(limit: 20);

            Assert.AreEqual(20, result.Content.Length);
        }

        [TestMethod]
        public async Task TestGetHashtagTimeline()
        {
            var client = new MastodonDynamicClient(_Host, _AccessToken);

            var result = await client.GetHashtagTimeline(hashtag: "超会議", limit: 20);

            Assert.AreEqual(20, result.Content.Length);
        }

    }
}
