using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

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
        /// Tuple.Item1=corpSecret,Tuple.Item1=accessToken,Tuple.Item3=accessTokenExpiredTime
        /// </summary>
        private static readonly ConcurrentDictionary<string, Tuple<string, string, DateTime>> Cache =
            new ConcurrentDictionary<string, Tuple<string, string, DateTime>>();

        public Task<string> GetAsync(string corpId, string corpSecret)
        {
            if (Cache.ContainsKey(corpId))
            {
                Tuple<string, string, DateTime> tuple;
                if (Cache.TryGetValue(corpId, out tuple))
                {
                    if (tuple.Item1 != corpSecret || tuple.Item3 < DateTime.Now)
                    {
                        Cache.TryRemove(corpId, out tuple);
                        return null;
                    }
                    return Task.FromResult(tuple.Item1);
                }
            }
            return null;
        }

        public Task SetAsync(string corpId, string corpSecret, string accessToken, DateTime expired)
        {
            Cache.AddOrUpdate(corpId, Tuple.Create(corpSecret, accessToken, expired),
                (key, value) => Tuple.Create(corpSecret, accessToken, expired));
            return null;
        }
    }
}