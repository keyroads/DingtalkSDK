using System;
using System.Threading.Tasks;

namespace Keyroads.DingtalkSDK
{
    public static class CorpApi
    {
        public static async Task<DepartmentMemberInfoResponse> GetDepartmentMemberInfo(string accessToken, DepartmentMemberInfoRequest requestData)
        {
            var result = await CommonApi.AccessDingtalkServerAsync<DepartmentMemberInfoResponse>(
                $"https://oapi.dingtalk.com/user/list?access_token={accessToken}&department_id={requestData.DepartmentID}&lang={requestData.Lang}&" +
                $"offset={requestData.Offset}&size={requestData.Size}&order={requestData.Order}",
                null, "GET");
            return result;
        }

        public static async Task<GetJsApiTicketResponse> GetJsApiTicketAsync(string accessToken)
        {
            return await CommonApi.AccessDingtalkServerAsync<GetJsApiTicketResponse>(
                $"https://oapi.dingtalk.com/get_jsapi_ticket?access_token={accessToken}&type=jsapi", null, "GET");
        }

        public static async Task<GetUserInfoResponse> GetUserInfo(string accessToken, string code)
        {
            return await CommonApi.AccessDingtalkServerAsync<GetUserInfoResponse>(
                $"https://oapi.dingtalk.com/user/getuserinfo?access_token={accessToken}&code={code}", null, "GET");
        }

        public static async Task<string> GetToken(string corpId, string corpSecret, IAccessTokenCacher cacher = null)
        {
            if (cacher == null) cacher = new MemoryAccessTokenCacher();
            var token = await cacher.GetAsync(corpId, corpSecret);
            if (!string.IsNullOrEmpty(token)) return token;
            var result = await CommonApi.AccessDingtalkServerAsync<GetTokenResponse>(
                $"https://oapi.dingtalk.com/gettoken?corpid={corpId}&corpsecret={corpSecret}", null, "GET");
            if (result.errcode == 0)
            {
                await cacher.SetAsync(corpId, corpSecret, result.access_token, DateTime.Now.AddSeconds(7200));
                return result.access_token;
            }
            throw new Exception($"errcode: {result.errcode}, errmsg: {result.errmsg}");
        }
    }
}
