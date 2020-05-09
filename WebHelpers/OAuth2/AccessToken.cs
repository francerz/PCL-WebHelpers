using Newtonsoft.Json;
using System;

namespace WebHelpers.OAuth2
{
    public class TokenSuccess
    {
        public TokenSuccess()
        {
            CreateTime = DateTime.Now;
        }

        [JsonProperty("access_token")]
        public string AccessToken {
            get;
            private set;
        }
        [JsonProperty("token_type")]
        public string Type {
            get;
            private set;
        }
        [JsonProperty("expires_in")]
        public int Lifetime {
            get;
            private set;
        }
        [JsonProperty("refresh_token")]
        public string RefreshToken {
            get;
            private set;
        }
        public DateTime CreateTime {
            get;
            private set;
        }
        public DateTime ExpireTime {
            get {
                return CreateTime + new TimeSpan(Lifetime * 10000000);
            }
        }
        public TimeSpan ExpiresIn {
            get {
                return ExpireTime - DateTime.Now;
            }
        }
        public bool IsExpired {
            get {
                return ExpiresIn.Seconds < 30;
            }
        }
    }
}
