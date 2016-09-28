using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Keyroads.DingtalkSDK
{
    public interface IJsApiTicketCacher
    {
        Task<string> GetAsync(string accessToken);
        Task SetAsync(string accessToken, string ticket, DateTime expired);
    }

    public class MemoryJsApiTicketCacher : IJsApiTicketCacher
    {
        private static readonly ConcurrentDictionary<string, Tuple<string,DateTime>> Cache =
           new ConcurrentDictionary<string, Tuple<string, DateTime>>();

        public Task<string> GetAsync(string accessToken)
        {
            if (Cache.ContainsKey(accessToken))
            {
                Tuple<string, DateTime> info;
                if (Cache.TryGetValue(accessToken, out info))
                {
                    if (info.Item1 != accessToken || info.Item2 < DateTime.Now)
                    {
                        Cache.TryRemove(accessToken, out info);
                        return TaskConstants<string>.Default;
                    }
                    return Task.FromResult(info.Item1);
                }
            }
            return TaskConstants<string>.Default;
        }

        public Task SetAsync(string accessToken, string ticket, DateTime expired)
        {
            Cache.AddOrUpdate(accessToken, new Tuple<string, DateTime>(ticket, expired),
               (key, value) => new Tuple<string, DateTime>(ticket, expired));
            return TaskConstants.Completed;
        }
    }
}