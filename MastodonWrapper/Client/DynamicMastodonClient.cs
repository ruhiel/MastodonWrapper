using Codeplex.Data;
using MastodonWrapper.Attributes;
using MastodonWrapper.Entity;
using MastodonWrapper.Extentions;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MastodonWrapper.Client
{
    public class MastodonDynamicClient : AbstractMastodonClient
    {

        public MastodonDynamicClient(string host, string accessToken = null) : base(host, accessToken)
        {

        }

        #region * Accounts
        #region Fetching an account
        public async new Task<dynamic> GetAccount(int id)
        {
            var response = await base.GetAccount(id);

            return DynamicJson.Parse(response.Content);
        }

        #endregion
        #region Getting the current user
        public async new Task<dynamic> GetCurrentAccount()
        {
            var response = await base.GetCurrentAccount();

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Updating the current user
        public async new Task<dynamic> UpdateAccount(string display_name = null, string note = null, string avatar = null, string header = null)
        {
            var response = await base.UpdateAccount(display_name, note, avatar, header);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Getting an account's followers
        public async new Task<StreamContent<dynamic>> GetFollowers(int id, int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetFollowers(id, max_id, since_id, limit);

            return CreateStreamContent(response);
        }
        #endregion
        #region Getting who account is following
        public async new Task<StreamContent<dynamic>> GetFollowing(int id, int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetFollowing(id, max_id, since_id, limit);

            return CreateStreamContent(response);
        }
        #endregion
        #region Getting an account's statuses
        public async new Task<dynamic[]> GetAccountsStatuses(int id, bool? only_media = null, bool? exclude_replies = null, int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetAccountsStatuses(id, only_media, exclude_replies, max_id, since_id, limit);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Following/unfollowing an account
        public async new Task<dynamic> Follow(int id)
        {
            var response = await base.Follow(id);

            return DynamicJson.Parse(response.Content);
        }
        public async new Task<dynamic> Unfollow(int id)
        {
            var response = await base.Unfollow(id);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Blocking/unblocking an account
        public async new Task<dynamic> Block(int id)
        {
            var response = await base.Block(id);

            return DynamicJson.Parse(response.Content);
        }
        public async new Task<dynamic> UnBlock(int id)
        {
            var response = await base.UnBlock(id);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Muting/unmuting an account
        public async new Task<dynamic> Mute(int id)
        {
            var response = await base.Mute(id);

            return DynamicJson.Parse(response.Content);
        }
        public async new Task<dynamic> UnMute(int id)
        {
            var response = await base.UnMute(id);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Getting an account's relationships
        public async new Task<dynamic[]> GetRelationships(int id)
        {
            var response = await base.GetRelationships(id);

            return DynamicJson.Parse(response.Content);
        }

        public async new Task<dynamic[]> GetRelationships(IEnumerable<int> id)
        {
            var response = await base.GetRelationships(id);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Searching for accounts
        public async new Task<dynamic[]> SearchAccount(string q, int limit = 40)
        {
            var response = await base.SearchAccount(q, limit);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #endregion
        #region * Apps
        #region Registering an application
        public async new Task<dynamic> Register(string client_name, OAuthScope scopes, string redirect_uris = "urn:ietf:wg:oauth:2.0:oob", string website = null)
        {
            var response = await base.Register(client_name, scopes, redirect_uris, website);

            var json = DynamicJson.Parse(response.Content);

            json.auth_url = OAuthUrl(_Host, json.client_id, scopes, redirect_uris);

            return json;
        }
        #endregion
        #endregion
        #region * Blocks
        #region Fetching a user's blocks
        public async new Task<StreamContent<dynamic>> GetBlocks(int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetBlocks(max_id, since_id, limit);

            return CreateStreamContent(response);
        }
        #endregion
        #endregion
        #region * Favourites
        #region Fetching a user's favourites
        public async new Task<StreamContent<dynamic>> GetFavourites(int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetFavourites(max_id, since_id, limit);

            return CreateStreamContent(response);
        }
        #endregion
        #endregion
        #region * Follow Requests
        #region Fetching a list of follow requests
        public async new Task<StreamContent<dynamic>> GetFollowRequests(int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetFollowRequests(max_id, since_id, limit);

            return CreateStreamContent(response);
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
        public async new Task<dynamic> FollowRemoteUser(string uri)
        {
            var response = await base.FollowRemoteUser(uri);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #endregion
        #region * Instances
        #region Getting instance information
        public async new Task<dynamic> GetInstance()
        {
            var response = await base.GetInstance();

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #endregion
        #region * Media
        #region Uploading a media attachment
        public async new Task<dynamic> UploadMedia(FileInfo file)
        {
            var response = await base.UploadMedia(file);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #endregion
        #region * Mutes
        #region Fetching a user's mutes
        public async new Task<StreamContent<dynamic>> GetMutes(int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetMutes(max_id, since_id, limit);

            return CreateStreamContent(response);
        }
        #endregion
        #endregion
        #region * Notifications
        #region Fetching a user's notifications
        public async new Task<StreamContent<dynamic>> GetNotifications(int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetNotifications(max_id, since_id, limit);

            return CreateStreamContent(response);
        }
        #endregion
        #region Getting a single notification
        public async new Task<dynamic> GetNotification(int id)
        {
            var response = await base.GetNotification(id);

            return DynamicJson.Parse(response.Content);
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
        public async new Task<dynamic> GetReports()
        {
            var response = await base.GetReports();

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Reporting a user
        public async new Task<dynamic> Report(int account_id, IEnumerable<int> status_ids, string comment)
        {
            var response = await base.Report(account_id, status_ids, comment);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #endregion
        #region * Search
        #region Searching for content
        public async new Task<dynamic> Search(string q, bool resolve = false)
        {
            var response = await base.Search(q, resolve);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #endregion
        #region * Statuses
        #region Fetching a status
        public async new Task<dynamic> GetStatus(int id)
        {
            var response = await base.GetStatus(id);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Getting status context
        public async new Task<dynamic> GetStatusContext(int id)
        {
            var response = await base.GetStatusContext(id);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Getting a card associated with a status
        public async new Task<dynamic> GetStatusCard(int id)
        {
            var response = await base.GetStatusCard(id);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Getting who reblogged/favourited a status
        public async new Task<dynamic[]> GetRebloggedStatus(int id, int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetRebloggedStatus(id, max_id, since_id, limit);

            return DynamicJson.Parse(response.Content);
        }
        public async new Task<dynamic[]> GetFavouritedStatus(int id, int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetFavouritedStatus(id, max_id, since_id, limit);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Posting a new status
        public async new Task<dynamic> PostStatus(string status, int? in_reply_to_id = null, IEnumerable<int> media_ids = null, bool? sensitive = null, string spoiler_text = null, Visibility? visibility = null)
        {
            var response = await base.PostStatus(status, in_reply_to_id, media_ids, sensitive, spoiler_text, visibility);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Deleting a status
        public async new Task<dynamic> DeleteStatus(int id)
        {
            var response = await base.DeleteStatus(id);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Reblogging/unreblogging a status
        public async new Task<dynamic> Reblog(int id)
        {
            var response = await base.Reblog(id);

            return DynamicJson.Parse(response.Content);
        }
        public async new Task<dynamic> UnReblog(int id)
        {
            var response = await base.UnReblog(id);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #region Favouriting/unfavouriting a status
        public async new Task<dynamic> Favourite(int id)
        {
            var response = await base.Favourite(id);

            return DynamicJson.Parse(response.Content);
        }

        public async new Task<dynamic> UnFavourite(int id)
        {
            var response = await base.UnFavourite(id);

            return DynamicJson.Parse(response.Content);
        }
        #endregion
        #endregion
        #region * Timelines
        #region Retrieving a timeline
        public async new Task<StreamContent<dynamic>> GetHomeTimeline(int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetHomeTimeline(max_id, since_id, limit);

            return CreateStreamContent(response);
        }

        public async new Task<StreamContent<dynamic>> GetPublicTimeline(bool? local = null, int? max_id = null, int? since_id = null, int? limit = 20)
        {
            var response = await base.GetPublicTimeline(local, max_id, since_id, limit);

            return CreateStreamContent(response);
        }
        
        public async new Task<StreamContent<dynamic>> GetHashtagTimeline(string hashtag, bool? local = null, int? max_id = null, int? since_id = null, int? limit = null)
        {
            var response = await base.GetHashtagTimeline(hashtag, local, max_id, since_id, limit);

            return CreateStreamContent(response);
        }
        #endregion
        #endregion
        #region private Methods
        private StreamContent<dynamic> CreateStreamContent(IRestResponse response)
        {
            return CreateStreamContent(response, Deserialize);
        }

        private dynamic[] Deserialize(string value) => DynamicJson.Parse(value);
        #endregion
    }
}
