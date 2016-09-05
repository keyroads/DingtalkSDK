using System.Threading.Tasks;

namespace Keyroads.DingtalkSDK
{
    public static class IsvApi
    {
        public static async Task<GetSuiteTokenResponse> GetSuiteAccessTokenAsync(GetSuiteTokenRequest requestData)
        {
            return await CommonApi.AccessDingtalkServerAsync<GetSuiteTokenResponse>(
                "https://oapi.dingtalk.com/service/get_suite_token", requestData);
        }

        public static async Task<GetPermanentCodeResponse> GetPermanentCodeAsync(string accessToken, GetPermanentCodeRequest requestData)
        {
            return await CommonApi.AccessDingtalkServerAsync<GetPermanentCodeResponse>(
                "https://oapi.dingtalk.com/service/get_permanent_code?suite_access_token=" + accessToken, requestData);
        }

        public static async Task<bool> ActivateSuiteAsync(string accessToken, CommonIsvRequest isvRequestData)
        {
            var result = await CommonApi.AccessDingtalkServerAsync<ErrorResponse>(
                "https://oapi.dingtalk.com/service/activate_suite?suite_access_token=" + accessToken, isvRequestData);
            return result.errcode == 0;
        }

        public static async Task<GetAuthInfoResponse> GetAuthInfoAsync(string accessToken, CommonIsvRequest isvRequestData)
        {
            return await CommonApi.AccessDingtalkServerAsync<GetAuthInfoResponse>(
               "https://oapi.dingtalk.com/service/get_auth_info?suite_access_token=" + accessToken, isvRequestData);
        }

        public static async Task<GetAgentResponse> GetAgentAsync(string accessToken, GetAgentIsvRequest isvRequestData)
        {
            return await CommonApi.AccessDingtalkServerAsync<GetAgentResponse>(
                "https://oapi.dingtalk.com/service/get_agent?suite_access_token=" + accessToken, isvRequestData);
        }

        public static async Task<bool> ActivateSuiteAsync(string accessToken, ActivateSuiteRequest requestData)
        {
            var result = await CommonApi.AccessDingtalkServerAsync<ErrorResponse>(
                "https://oapi.dingtalk.com/service/activate_suite?suite_access_token=" + accessToken, requestData);
            return result.errcode == 0;
        }

        public static async Task<GetCorpTokenResponse> GetCorpTokenAsync(string accessToken, CommonIsvRequest isvRequestData)
        {
            return await CommonApi.AccessDingtalkServerAsync<GetCorpTokenResponse>(
                "https://oapi.dingtalk.com/service/get_corp_token?suite_access_token=" + accessToken, isvRequestData);
        }
    }
}


