using MastodonWrapper.Entity;
using MastodonWrapper.Extentions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastodonWrapper.Client
{
    public class MastodonWrapperClient : AbstractMastodonClient
    {
        public MastodonWrapperClient(string host, string accessToken = null) : base(host, accessToken)
        {
        }
        #region * Accounts
        #region Fetching an account
        public async new Task<Account> GetAccount(int id)
        {
            var response = await base.GetAccount(id);

            return JsonConvert.DeserializeObject<Account>(response.Content);
        }
        #endregion
        #region Getting the current user
        public async new Task<Account> GetCurrentAccount()
        {
            var response = await base.GetCurrentAccount();

            return JsonConvert.DeserializeObject<Account>(response.Content);
        }
        #endregion
        #region Updating the current user
        public async new Task<Account> UpdateAccount(string display_name = null, string note = null, string avatar = null, string header = null)
        {
            var response = await base.UpdateAccount(display_name, note, avatar, header);

            return JsonConvert.DeserializeObject<Account>(response.Content);
        }
        #endregion
        #region Getting an account's followers
        public async new Task<StreamContent<Account>> GetFollowers(int id, int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetFollowers(id, max_id, since_id, limit);

            return CreateStreamContent(response, JsonConvert.DeserializeObject<Account[]>);
        }
        #endregion
        #region Getting who account is following
        public async new Task<StreamContent<Account>> GetFollowing(int id, int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetFollowing(id, max_id, since_id, limit);

            return CreateStreamContent(response, JsonConvert.DeserializeObject<Account[]>);
        }
        #endregion
        #region Getting an account's statuses
        public async new Task<StreamContent<Status>> GetAccountsStatuses(int id, bool? only_media = null, bool? exclude_replies = null, int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetAccountsStatuses(id, only_media, exclude_replies, max_id, since_id, limit);

            return CreateStreamContent(response, JsonConvert.DeserializeObject<Status[]>);
        }
        #endregion
        #region Following/unfollowing an account
        public async new Task<Relationship> Follow(int id)
        {
            var response = await base.Follow(id);

            return JsonConvert.DeserializeObject<Relationship>(response.Content);
        }
        public async new Task<Relationship> Unfollow(int id)
        {
            var response = await base.Unfollow(id);

            return JsonConvert.DeserializeObject<Relationship>(response.Content);
        }
        #endregion
        #region Blocking/unblocking an account
        public async new Task<Relationship> Block(int id)
        {
            var response = await base.Block(id);

            return JsonConvert.DeserializeObject<Relationship>(response.Content);
        }
        public async new Task<Relationship> UnBlock(int id)
        {
            var response = await base.UnBlock(id);

            return JsonConvert.DeserializeObject<Relationship>(response.Content);
        }
        #endregion
        #region Muting/unmuting an account
        public async new Task<Relationship> Mute(int id)
        {
            var response = await base.Mute(id);

            return JsonConvert.DeserializeObject<Relationship>(response.Content);
        }
        public async new Task<Relationship> UnMute(int id)
        {
            var response = await base.UnMute(id);

            return JsonConvert.DeserializeObject<Relationship>(response.Content);
        }
        #endregion
        #region Getting an account's relationships
        public async new Task<Relationship[]> GetRelationships(int id)
        {
            var response = await base.GetRelationships(id);

            return JsonConvert.DeserializeObject<Relationship[]>(response.Content);
        }

        public async new Task<Relationship[]> GetRelationships(IEnumerable<int> id)
        {
            var response = await base.GetRelationships(id);

            return JsonConvert.DeserializeObject<Relationship[]>(response.Content);
        }
        #endregion
        #region Searching for accounts
        public async new Task<Account[]> SearchAccount(string q, int limit = 40)
        {
            var response = await base.SearchAccount(q, limit);

            return JsonConvert.DeserializeObject<Account[]>(response.Content);
        }
        #endregion
        #endregion
        #region * Apps
        #region Registering an application
        public async new Task<AppRegistration> Register(string client_name, OAuthScope scopes, string redirect_uris = "urn:ietf:wg:oauth:2.0:oob", string website = null)
        {
            var response = await base.Register(client_name, scopes, redirect_uris, website);

            var appRegistration = JsonConvert.DeserializeObject<AppRegistration>(response.Content);

            appRegistration.Instance = _Host;

            appRegistration.Scope = scopes;

            appRegistration.AuthUrl = OAuthUrl(_Host, appRegistration.ClientId, scopes, appRegistration.RedirectUri);

            return appRegistration;
        }
        #endregion
        #endregion
        #region * Blocks
        #region Fetching a user's blocks
        public async new Task<StreamContent<Account>> GetBlocks(int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetBlocks(max_id, since_id, limit);

            return CreateStreamContent(response, JsonConvert.DeserializeObject<Account[]>);
        }
        #endregion
        #endregion
        #region * Favourites
        #region Fetching a user's favourites
        public async new Task<StreamContent<Status>> GetFavourites(int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetFavourites(max_id, since_id, limit);

            return CreateStreamContent(response, JsonConvert.DeserializeObject<Status[]>);
        }
        #endregion
        #endregion
        #region * Follow Requests
        #region Fetching a list of follow requests
        public async new Task<StreamContent<Account>> GetFollowRequests(int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetFollowRequests(max_id, since_id, limit);

            return CreateStreamContent(response, JsonConvert.DeserializeObject<Account[]>);
        }
        #endregion
        #region Authorizing or rejecting follow requests
        public new Task AuthorizeFollowRequest(int id)
        {
            return base.AuthorizeFollowRequest(id);
        }
        public new Task RejectFollowRequest(int id)
        {
            return base.RejectFollowRequest(id);
        }
        #endregion
        #endregion
        #region * Follows
        #region Following a remote user
        public async new Task<Account> FollowRemoteUser(string uri)
        {
            var response = await base.FollowRemoteUser(uri);

            return JsonConvert.DeserializeObject<Account>(response.Content);
        }
        #endregion
        #endregion
        #region * Instances
        #region Getting instance information
        public async new Task<Instance> GetInstance()
        {
            var response = await base.GetInstance();

            return JsonConvert.DeserializeObject<Instance>(response.Content);
        }
        #endregion
        #endregion
        #region * Media
        #region Uploading a media attachment
        public async new Task<Attachment> UploadMedia(FileInfo file)
        {
            var response = await base.UploadMedia(file);

            return JsonConvert.DeserializeObject<Attachment>(response.Content);
        }
        #endregion
        #endregion
        #region * Mutes
        #region Fetching a user's mutes
        public async new Task<StreamContent<Account>> GetMutes(int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetMutes(max_id, since_id, limit);

            return CreateStreamContent(response, JsonConvert.DeserializeObject<Account[]>);
        }
        #endregion
        #endregion
        #region * Notifications
        #region Fetching a user's notifications
        public async new Task<StreamContent<Notification>> GetNotifications(int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetNotifications(max_id, since_id, limit);

            return CreateStreamContent(response, JsonConvert.DeserializeObject<Notification[]>);
        }
        #endregion
        #region Getting a single notification
        public async new Task<Notification> GetNotification(int id)
        {
            var response = await base.GetNotification(id);

            return JsonConvert.DeserializeObject<Notification>(response.Content);
        }
        #endregion
        #region Clearing notifications
        public new Task ClearNotifications()
        {
            return base.ClearNotifications();
        }
        #endregion
        #endregion
        #region * Reports
        #region Fetching a user's reports
        public async new Task<Report[]> GetReports()
        {
            var response = await base.GetReports();

            return JsonConvert.DeserializeObject<Report[]>(response.Content);
        }
        #endregion
        #region Reporting a user
        public async new Task<Report> Report(int account_id, IEnumerable<int> status_ids, string comment)
        {
            var response = await base.Report(account_id, status_ids, comment);

            return JsonConvert.DeserializeObject<Report>(response.Content);
        }
        #endregion
        #endregion
        #region * Search
        #region Searching for content
        public async new Task<Results[]> Search(string q, bool resolve = false)
        {
            var response = await base.Search(q, resolve);

            return JsonConvert.DeserializeObject<Results[]>(response.Content);
        }
        #endregion
        #endregion
        #region * Statuses
        #region Fetching a status
        public async new Task<Status> GetStatus(int id)
        {
            var response = await base.GetStatus(id);

            return JsonConvert.DeserializeObject<Status>(response.Content);
        }
        #endregion
        #region Getting status context
        public async new Task<Context> GetStatusContext(int id)
        {
            var response = await base.GetStatusContext(id);

            return JsonConvert.DeserializeObject<Context>(response.Content);
        }
        #endregion
        #region Getting a card associated with a status
        public async new Task<Card> GetStatusCard(int id)
        {
            var response = await base.GetStatusCard(id);

            return JsonConvert.DeserializeObject<Card>(response.Content);
        }
        #endregion
        #region Getting who reblogged/favourited a status
        public async new Task<Account[]> GetRebloggedStatus(int id, int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetRebloggedStatus(id, max_id, since_id, limit);

            return JsonConvert.DeserializeObject<Account[]>(response.Content);
        }
        public async new Task<dynamic[]> GetFavouritedStatus(int id, int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetFavouritedStatus(id, max_id, since_id, limit);

            return JsonConvert.DeserializeObject<Account[]>(response.Content);
        }
        #endregion
        #region Posting a new status
        public async new Task<Status> PostStatus(string status, int? in_reply_to_id = null, IEnumerable<int> media_ids = null, bool? sensitive = null, string spoiler_text = null, Visibility? visibility = null)
        {
            var response = await base.PostStatus(status, in_reply_to_id, media_ids, sensitive, spoiler_text, visibility);

            return JsonConvert.DeserializeObject<Status>(response.Content);
        }
        #endregion
        #region Deleting a status
        public async new Task<Status> DeleteStatus(int id)
        {
            var response = await base.DeleteStatus(id);

            return JsonConvert.DeserializeObject<Status>(response.Content);
        }
        #endregion
        #region Reblogging/unreblogging a status
        public async new Task<Status> Reblog(int id)
        {
            var response = await base.Reblog(id);

            return JsonConvert.DeserializeObject<Status>(response.Content);
        }
        public async new Task<Status> UnReblog(int id)
        {
            var response = await base.UnReblog(id);

            return JsonConvert.DeserializeObject<Status>(response.Content);
        }
        #endregion
        #region Favouriting/unfavouriting a status
        public async new Task<Status> Favourite(int id)
        {
            var response = await base.Favourite(id);

            return JsonConvert.DeserializeObject<Status>(response.Content);
        }

        public async new Task<Status> UnFavourite(int id)
        {
            var response = await base.UnFavourite(id);

            return JsonConvert.DeserializeObject<Status>(response.Content);
        }
        #endregion
        #endregion
        #region * Timelines
        #region Retrieving a timeline
        public async new Task<StreamContent<Status>> GetHomeTimeline(int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetHomeTimeline(max_id, since_id, limit);

            return CreateStreamContent(response, JsonConvert.DeserializeObject<Status[]>);
        }

        public async new Task<StreamContent<Status>> GetPublicTimeline(bool? local = null, int? max_id = null, int? since_id = null, int? limit = 20)
        {
            var response = await base.GetPublicTimeline(local, max_id, since_id, limit);

            return CreateStreamContent(response, JsonConvert.DeserializeObject<Status[]>);
        }

        public async new Task<StreamContent<Status>> GetHashtagTimeline(string hashtag, bool? local = null, int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetHashtagTimeline(hashtag, local, max_id, since_id, limit);

            return CreateStreamContent(response, JsonConvert.DeserializeObject<Status[]>);
        }
        #endregion
        #endregion
        #region private Methods
        #endregion
    }
}
