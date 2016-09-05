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
    }
}
