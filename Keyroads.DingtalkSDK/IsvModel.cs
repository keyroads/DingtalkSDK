using System.Collections.Generic;

namespace Keyroads.DingtalkSDK
{
    public class GetSuiteTokenRequest
    {
        public string suite_key { get; set; }

        public string suite_secret { get; set; }

        public string suite_ticket { get; set; }
    }

    public class GetSuiteTokenResponse : IResponseValidation
    {
        public string suite_access_token { get; set; }

        public int expires_in { get; set; }
        public bool IsValidate()
        {
            return !string.IsNullOrEmpty(suite_access_token);
        }
    }

    public class GetPermanentCodeRequest
    {
        public string tmp_auth_code { get; set; }
    }

    public class GetPermanentCodeResponse : IResponseValidation
    {
        public string permanent_code { get; set; }

        public AuthCorpInfo auth_corp_info { get; set; }
        public bool IsValidate()
        {
            return !string.IsNullOrEmpty(permanent_code) && auth_corp_info != null;
        }
    }

    public class AuthCorpInfo
    {
        public string corpid { get; set; }
        public string corp_name { get; set; }
        public string corp_logo_url { get; set; }
        public string industry { get; set; }
        public string invite_code { get; set; }
        public string license_code { get; set; }
        public string auth_channel { get; set; }
        public bool is_authenticated { get; set; }
        public string invite_url { get; set; }
    }

    public class AuthUserInfo
    {
        public string userId { get; set; }
    }

    public class AuthAgentInfo
    {
        public string agent_name { get; set; }
        public int agentid { get; set; }
        public int appid { get; set; }
        public string logo_url { get; set; }
        public bool IsValidate()
        {
            return true;
        }
    }

    public class AuthInfo
    {
        public List<AuthAgentInfo> agent { get; set; }
    }

    /// <summary>
    /// 用于 activate_suite get_auth_info get_corp_token 的请求数据
    /// </summary>
    public class CommonIsvRequest
    {
        public string suite_key { get; set; }

        public string auth_corpid { get; set; }

        public string permanent_code { get; set; }
    }

    public class ActivateSuiteRequest
    {
        public string suite_key { get; set; }

        public string auth_corpid { get; set; }

        public string permanent_code { get; set; }
    }

    public class GetAuthInfoResponse : ErrorResponse
    {
        public AuthCorpInfo auth_corp_info { get; set; }
        public AuthUserInfo auth_user_info { get; set; }
        public AuthInfo auth_info { get; set; }
        public override bool IsValidate()
        {
            return auth_corp_info != null && auth_user_info != null && auth_info != null;
        }
    }

    public class GetAgentIsvRequest : CommonIsvRequest
    {
        public int agentid { get; set; }
    }

    public class GetAgentResponse : ErrorResponse
    {
        public int agentid { get; set; }
        public string name { get; set; }
        public string logo_url { get; set; }
        public string description { get; set; }
        public int close { get; set; }

        public override bool IsValidate()
        {
            return !string.IsNullOrEmpty(name);
        }
    }

    public class GetCorpTokenResponse : IResponseValidation
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public bool IsValidate()
        {
            return !string.IsNullOrEmpty(access_token);
        }
    }
}

