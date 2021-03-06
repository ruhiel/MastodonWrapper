﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MastodonWrapper.Entity
{
    public class Auth
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }
}
