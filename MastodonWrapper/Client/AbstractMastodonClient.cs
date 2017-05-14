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
    /// <summary>
    /// AbstractMastodonClient
    /// 
    /// https://github.com/tootsuite/documentation/blob/master/Using-the-API/API.md
    /// </summary>
    public abstract class AbstractMastodonClient
    {
        protected string _Host;

        protected string _AccessToken;

        protected AbstractMastodonClient(string host, string accessToken = null)
        {
            _Host = host;
            _AccessToken = accessToken;
        }

        [Method(Method.GET)]
        [Query("/api/v1/accounts/{id}")]
        protected Task<IRestResponse> GetAccount(int id)
        {
            return Execute(nameof(GetAccount),
                null,
                new { id });
        }

        [Method(Method.GET)]
        [Query("/api/v1/accounts/verify_credentials")]
        protected Task<IRestResponse> GetCurrentAccount()
        {
            return Execute(nameof(GetCurrentAccount));
        }

        [Method(Method.PATCH)]
        [Query("/api/v1/accounts/update_credentials")]
        protected Task<IRestResponse> UpdateAccount(string display_name = null, string note = null, string avatar = null, string header = null)
        {
            return Execute(nameof(UpdateAccount),
                new { display_name, note, avatar, header });
        }

        [Method(Method.GET)]
        [Query("/api/v1/accounts/{id}/followers")]
        protected Task<IRestResponse> GetFollowers(int id, int? max_id = null, int? since_id = null, int? limit = null)
        {
            return Execute(nameof(GetFollowers),
                new { max_id, since_id, limit },
                new { id });
        }

        [Method(Method.GET)]
        [Query("/api/v1/accounts/{id}/following")]
        protected Task<IRestResponse> GetFollowing(int id, int? max_id = null, int? since_id = null, int? limit = null)
        {
            return Execute(nameof(GetFollowing),
                new { max_id, since_id, limit },
                new { id });
        }

        [Method(Method.GET)]
        [Query("/api/v1/accounts/{id}/statuses")]
        protected Task<IRestResponse> GetAccountsStatuses(int id, bool? only_media = null, bool? exclude_replies = null, int? max_id = null, int? since_id = null, int? limit = null)
        {
            return Execute(nameof(GetAccountsStatuses),
                new { only_media, exclude_replies, max_id, since_id, limit },
                new { id });
        }

        [Method(Method.POST)]
        [Query("/api/v1/accounts/{id}/follow")]
        protected Task<IRestResponse> Follow(int id)
        {
            return Execute(nameof(Follow),
                null,
                new { id });
        }

        [Method(Method.POST)]
        [Query("/api/v1/accounts/{id}/unfollow")]
        protected Task<IRestResponse> Unfollow(int id)
        {
            return Execute(nameof(Unfollow),
                null,
                new { id });
        }
        [Method(Method.POST)]
        [Query("/api/v1/accounts/{id}/block")]
        protected Task<IRestResponse> Block(int id)
        {
            return Execute(nameof(Block),
                null,
                new { id });
        }

        [Method(Method.POST)]
        [Query("/api/v1/accounts/{id}/unblock")]
        protected Task<IRestResponse> UnBlock(int id)
        {
            return Execute(nameof(UnBlock),
                null,
                new { id });
        }

        [Method(Method.POST)]
        [Query("/api/v1/accounts/{id}/mute")]
        protected Task<IRestResponse> Mute(int id)
        {
            return Execute(nameof(Mute),
                null,
                new { id });
        }

        [Method(Method.POST)]
        [Query("/api/v1/accounts/{id}/unmute")]
        protected Task<IRestResponse> UnMute(int id)
        {
            return Execute(nameof(UnMute),
                null,
                new { id });
        }

        [Method(Method.GET)]
        [Query("/api/v1/accounts/relationships")]
        protected Task<IRestResponse> GetRelationships(int id)
        {
            var method = GetAllMethod(nameof(GetRelationships), new Type[] { typeof(int) });
            return Execute(method,
                new { id });
        }

        [Method(Method.GET)]
        [Query("/api/v1/accounts/relationships")]
        protected Task<IRestResponse> GetRelationships(IEnumerable<int> id)
        {
            var method = GetAllMethod(nameof(GetRelationships), new Type[] { typeof(IEnumerable<int>) });
            return Execute(method,
                new { id });
        }

        [Method(Method.GET)]
        [Query("/api/v1/accounts/search")]
        protected Task<IRestResponse> SearchAccount(string q, int limit = 40)
        {
            return Execute(nameof(SearchAccount),
                new { q, limit });
        }

        [Method(Method.POST)]
        [Query("/api/v1/apps")]
        protected Task<IRestResponse> Register(string client_name, OAuthScope scopes, string redirect_uris = "urn:ietf:wg:oauth:2.0:oob", string website = null)
        {
            return Execute(nameof(Register),
                new { client_name, redirect_uris, scopes, website });
        }

        [Method(Method.GET)]
        [Query("/api/v1/blocks")]
        protected Task<IRestResponse> GetBlocks(int? max_id = null, int? since_id = null, int? limit = null)
        {
            return Execute(nameof(GetBlocks),
                new { max_id, since_id, limit });
        }

        [Method(Method.GET)]
        [Query("/api/v1/favourites")]
        protected Task<IRestResponse> GetFavourites(int? max_id = null, int? since_id = null, int? limit = null)
        {
            return Execute(nameof(GetFavourites),
                new { max_id, since_id, limit });
        }

        [Method(Method.GET)]
        [Query("/api/v1/follow_requests")]
        protected Task<IRestResponse> GetFollowRequests(int? max_id = null, int? since_id = null, int? limit = null)
        {
            return Execute(nameof(GetFollowRequests),
                new { max_id, since_id, limit });
        }

        [Method(Method.POST)]
        [Query("/api/v1/follow_requests/{id}/authorize")]
        protected async Task AuthorizeFollowRequest(int id)
        {
            await Execute(nameof(AuthorizeFollowRequest),
                null,
                new { id });
        }

        [Method(Method.POST)]
        [Query("/api/v1/follow_requests/{id}/reject")]
        protected async Task RejectFollowRequest(int id)
        {
            await Execute(nameof(RejectFollowRequest),
                null,
                new { id });
        }
        [Method(Method.POST)]
        [Query("/api/v1/follows")]
        protected Task<IRestResponse> FollowRemoteUser(string uri)
        {
            return Execute(nameof(FollowRemoteUser),
                new { uri });
        }

        [Method(Method.GET)]
        [Query("/api/v1/instance")]
        protected Task<IRestResponse> GetInstance()
        {
            return Execute(nameof(GetInstance));
        }

        [Method(Method.POST)]
        [Query("/api/v1/media")]
        protected Task<IRestResponse> UploadMedia(FileInfo file)
        {
            return Execute(nameof(UploadMedia),
                new { file });
        }

        [Method(Method.GET)]
        [Query("/api/v1/mutes")]
        protected Task<IRestResponse> GetMutes(int? max_id = null, int? since_id = null, int? limit = null)
        {
            return Execute(nameof(GetMutes),
                new { max_id, since_id, limit });
        }

        [Method(Method.GET)]
        [Query("/api/v1/notifications")]
        protected Task<IRestResponse> GetNotifications(int? max_id = null, int? since_id = null, int? limit = null)
        {
            return Execute(nameof(GetNotifications),
                new { max_id, since_id, limit });
        }

        [Method(Method.GET)]
        [Query("/api/v1/notifications/{id}")]
        protected Task<IRestResponse> GetNotification(int id)
        {
            return Execute(nameof(GetNotification),
                null,
                new { id });
        }

        [Method(Method.POST)]
        [Query("/api/v1/notifications/clear")]
        protected async Task ClearNotifications()
        {
            await Execute(nameof(ClearNotifications));
        }

        [Method(Method.GET)]
        [Query("/api/v1/reports")]
        protected Task<IRestResponse> GetReports()
        {
            return Execute(nameof(GetReports));
        }

        [Method(Method.POST)]
        [Query("/api/v1/reports")]
        protected Task<IRestResponse> Report(int account_id, IEnumerable<int> status_ids, string comment)
        {
            return Execute(nameof(Report),
                new { account_id, status_ids, comment });
        }

        [Method(Method.GET)]
        [Query("/api/v1/search")]
        protected Task<IRestResponse> Search(string q, bool resolve = false)
        {
            return Execute(nameof(Search),
                new { q, resolve });
        }

        [Method(Method.GET)]
        [Query("/api/v1/statuses/{id}")]
        protected Task<IRestResponse> GetStatus(int id)
        {
            return Execute(nameof(GetStatus),
                null,
                new { id });
        }

        [Method(Method.GET)]
        [Query("/api/v1/statuses/{id}/context")]
        protected Task<IRestResponse> GetStatusContext(int id)
        {
            return Execute(nameof(GetStatusContext),
                null,
                new { id });
        }

        [Method(Method.GET)]
        [Query("/api/v1/statuses/{id}/card")]
        protected Task<IRestResponse> GetStatusCard(int id)
        {
            return Execute(nameof(GetStatusCard),
                null,
                new { id });
        }

        [Method(Method.GET)]
        [Query("/api/v1/statuses/{id}/reblogged_by")]
        protected Task<IRestResponse> GetRebloggedStatus(int id, int? max_id = null, int? since_id = null, int? limit = null)
        {
            return Execute(nameof(GetRebloggedStatus),
                new { max_id, since_id, limit },
                new { id });
        }

        [Method(Method.GET)]
        [Query("/api/v1/statuses/{id}/favourited_by")]
        protected Task<IRestResponse> GetFavouritedStatus(int id, int? max_id = null, int? since_id = null, int? limit = null)
        {
            return Execute(nameof(GetFavouritedStatus),
                new { max_id, since_id, limit },
                new { id });
        }

        [Method(Method.POST)]
        [Query("/api/v1/statuses")]
        protected Task<IRestResponse> PostStatus(string status, int? in_reply_to_id = null, IEnumerable<int> media_ids = null, bool? sensitive = null, string spoiler_text = null, Visibility? visibility = null)
        {
            return Execute(nameof(PostStatus),
                new { status, in_reply_to_id, media_ids, sensitive, spoiler_text, visibility });
        }

        [Method(Method.DELETE)]
        [Query("/api/v1/statuses/{id}")]
        protected Task<IRestResponse> DeleteStatus(int id)
        {
            return Execute(nameof(DeleteStatus),
                null,
                new { id });
        }

        [Method(Method.POST)]
        [Query("/api/v1/statuses/{id}/reblog")]
        protected Task<IRestResponse> Reblog(int id)
        {
            return Execute(nameof(Reblog),
                null,
                new { id });
        }

        [Method(Method.POST)]
        [Query("/api/v1/statuses/{id}/unreblog")]
        protected Task<IRestResponse> UnReblog(int id)
        {
            return Execute(nameof(UnReblog),
                null,
                new { id });
        }

        [Method(Method.POST)]
        [Query("/api/v1/statuses/{id}/favourite")]
        protected Task<IRestResponse> Favourite(int id)
        {
            return Execute(nameof(Favourite),
                null,
                new { id });
        }

        [Method(Method.POST)]
        [Query("/api/v1/statuses/{id}/unfavourite")]
        protected Task<IRestResponse> UnFavourite(int id)
        {
            return Execute(nameof(UnFavourite),
                null,
                new { id });
        }

        [Method(Method.GET)]
        [Query("/api/v1/timelines/home")]
        protected Task<IRestResponse> GetHomeTimeline(int? max_id = null, int? since_id = null, int? limit = null)
        {
            return Execute(nameof(GetHomeTimeline),
                new { max_id, since_id, limit });
        }

        [Method(Method.GET)]
        [Query("/api/v1/timelines/public")]
        protected Task<IRestResponse> GetPublicTimeline(bool? local = null, int? max_id = null, int? since_id = null, int? limit = 20)
        {
            return Execute(nameof(GetPublicTimeline),
                new { local, max_id, since_id, limit });
        }

        [Method(Method.GET)]
        [Query("/api/v1/timelines/tag/{hashtag}")]
        protected Task<IRestResponse> GetHashtagTimeline(string hashtag, bool? local = null, int? max_id = null, int? since_id = null, int? limit = null)
        {
            return Execute(nameof(GetHashtagTimeline),
                new { local, max_id, since_id, limit },
                new { hashtag });
        }

        private Method GetMethod(MethodBase methodBase)
        {
            var methodAttribute = (MethodAttribute)methodBase.GetCustomAttribute(typeof(MethodAttribute));
            return methodAttribute.Method;
        }

        private string GetQuery(MethodBase methodBase)
        {
            var queryAttribute = (QueryAttribute)methodBase.GetCustomAttribute(typeof(QueryAttribute));
            return queryAttribute.Query;
        }

        private Task<IRestResponse> Execute(MethodBase methodBase, object parameter = null, object urlSegment = null)
        {
            var client = new RestClient($"https://{_Host}");

            var query = GetQuery(methodBase);

            var method = GetMethod(methodBase);

            var request = CreateRequest(query, method, parameter, urlSegment);

            return client.ExecuteTaskAsync(request);
        }

        private Task<IRestResponse> Execute(string methodName, object parameter = null, object urlSegment = null)
        {
            return Execute(GetAllMethod(methodName), parameter, urlSegment);
        }

        private MethodInfo GetAllMethod(string methodName)
        {
            return typeof(AbstractMastodonClient).GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        private MethodInfo GetAllMethod(string methodName, Type[] types)
        {
            return typeof(AbstractMastodonClient).GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, types, null);
        }

        private RestRequest CreateRequest(string query, Method method, object parameter, object urlSegment)
        {
            var request = new RestRequest(query, method);

            if (!string.IsNullOrEmpty(_AccessToken))
            {
                request.AddHeader("Authorization", $"Bearer {_AccessToken}");
            }

            var parameterSet = parameter?.ToDictionary();

            if (parameterSet != null)
            {
                foreach (var item in parameterSet)
                {
                    if (item.Value is string)
                    {
                        request.AddParameter(item.Key, item.Value);
                    }
                    else if (item.Value is FileInfo fileinfo)
                    {
                        var bs = File.ReadAllBytes(fileinfo.FullName);

                        request.AddFileBytes(item.Key, bs, fileinfo.Name);
                    }
                    else if (item.Value is IEnumerable enumerable)
                    {
                        foreach (var element in enumerable)
                        {
                            request.AddParameter($"{item.Key}[]", element);
                        }
                    }
                    else if (item.Value is Visibility visibility)
                    {
                        request.AddParameter(item.Key, visibility.ToParam());
                    }
                    else if (item.Value is bool flag && flag)
                    {
                        request.AddParameter(item.Key, "1");
                    }
                    else if (item.Value is OAuthScope scope)
                    {
                        request.AddParameter(item.Key, scope.ToString());
                    }
                    else
                    {
                        request.AddParameter(item.Key, item.Value);
                    }
                }
            }

            var urlSegmentSet = urlSegment?.ToUrlSegment();

            if (urlSegmentSet != null)
            {
                foreach (var item in urlSegmentSet)
                {
                    request.AddUrlSegment(item.Key, item.Value);
                }
            }

            return request;
        }

        public string OAuthUrl(string host, string clientid, OAuthScope scope, string redirectUri)
        {
            return $"https://{host}/oauth/authorize?response_type=code&client_id={clientid}&scope={scope.ToString().Replace(" ", "%20")}&redirect_uri={redirectUri}";
        }

        protected StreamContent<T> CreateStreamContent<T>(IRestResponse response, Func<string, T[]> deserializer)
        {
            var linkHeader = response.Headers.FirstOrDefault(x => x.Name == "Link");

            var streamContent = new StreamContent<T>();

            streamContent.Content = deserializer(response.Content);
            var header = linkHeader.Value.ToString().GetHeader();
            streamContent.Next = header.next;
            streamContent.Prev = header.prev;

            return streamContent;
        }
    }
}
