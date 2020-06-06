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

        public static TokenSuccess FromPrimitives(
            in string accessToken,
            in string tokenType,
            in int expiresIn,
            in string refreshToken = null,
            long createTicks = -1
        ) {
            if (createTicks < 0)
            {
                createTicks = DateTime.Now.Ticks;
            }
            return new TokenSuccess
            {
                CreateTime = new DateTime(createTicks),
                AccessToken = accessToken,
                Type = tokenType,
                Lifetime = expiresIn,
                RefreshToken = refreshToken
            };
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
                return CreateTime + new TimeSpan(Lifetime * TimeSpan.TicksPerSecond);
            }
        }
        public TimeSpan ExpiresIn {
            get {
                return ExpireTime - DateTime.Now;
            }
        }
        public bool IsExpired {
            get {
                return ExpiresIn.TotalSeconds < 30;
            }
        }
    }
}
