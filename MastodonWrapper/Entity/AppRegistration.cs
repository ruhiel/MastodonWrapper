﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MastodonWrapper.Entity
{
    public class AppRegistration
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonIgnore]
        public string Instance { get; set; }

        [JsonIgnore]
        public OAuthScope Scope { get; set; }

        [JsonIgnore]
        public string AuthUrl { get; set; }
    }
}
