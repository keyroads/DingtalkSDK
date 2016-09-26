using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Keyroads.DingtalkSDK
{
    public interface IAccessTokenCacher
    {
        Task<string> GetAsync(string corpId, string corpSecret);
        Task SetAsync(string corpId, string corpSecret, string accessToken, DateTime expired);
    }

    public class MemoryAccessTokenCacher : IAccessTokenCacher
    {
        /// <summary>
        /// Tuple.Item1=corpSecret,Tuple.Item2=accessToken,Tuple.Item3=accessTokenExpiredTime
        /// </summary>
        private static readonly ConcurrentDictionary<string, AccessTokenInfo> Cache =
            new ConcurrentDictionary<string, AccessTokenInfo>();

        public Task<string> GetAsync(string corpId, string corpSecret)
        {
            if (Cache.ContainsKey(corpId))
            {
                AccessTokenInfo info;
                if (Cache.TryGetValue(corpId, out info))
                {
                    if (info.Secret != corpSecret || info.ExpiredTime < DateTime.Now)
                    {
                        Cache.TryRemove(corpId, out info);
                        return TaskConstants<string>.Default;
                    }
                    return Task.FromResult(info.Token);
                }
            }
            return TaskConstants<string>.Default;
        }

        public Task SetAsync(string corpId, string corpSecret, string accessToken, DateTime expired)
        {
            Cache.AddOrUpdate(corpId, new AccessTokenInfo(accessToken, corpSecret, expired),
                (key, value) => new AccessTokenInfo(accessToken, corpSecret, expired));
            return TaskConstants.Completed;
        }

        private struct AccessTokenInfo
        {
            public AccessTokenInfo(string token, string secret, DateTime expiredTime)
            {
                Token = token;
                Secret = secret;
                ExpiredTime = expiredTime;
            }

            public string Token { get; }

            public string Secret { get; }

            public DateTime ExpiredTime { get; }
        }
    }
}