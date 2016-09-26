using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Keyroads.DingtalkSDK
{
    public static class CommonApi
    {
        public static async Task<T> AccessDingtalkServerAsync<T>(string url, object postData, string method = "POST")
           where T : IResponseValidation
        {
            var client = new HttpClient();
            HttpResponseMessage response;
            if (method.ToUpper()=="POST")
                response = await client.PostAsJsonAsync(url, postData);
            else
                response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<T>();
                if (result.IsValidate())
                    return result;
                var errorText = await response.Content.ReadAsStringAsync();
                throw new Exception($"请求钉钉服务器错误：{errorText}");
            }
            throw new Exception($"请求钉钉服务器错误：{response.StatusCode}, {response.ReasonPhrase}");
        }

    }
}
