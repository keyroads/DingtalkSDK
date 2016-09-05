using System;
using System.Collections.Generic;

namespace Keyroads.DingtalkSdk
{
    public interface IResponseValidation
    {
        bool IsValidate();
    }

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
    }

    public class ActivateSuiteRequest
    {
        public string suite_key { get; set; }

        public string auth_corpid { get; set; }

        public string permanent_code { get; set; }
    }

    public class ErrorResponse : IResponseValidation
    {
        public int errcode { get; set; }

        public string errmsg { get; set; }
        public bool IsValidate()
        {
            return true;
        }
    }

    public class DepartmentMemberInfoRequest
    {
        protected string access_token;
        public void SetAccessToken(string token)
        {
            this.access_token = token;
        }
        public string AccessToken
        {
            get
            {
                return access_token;
            }
        }

        public string Lang { get; set; } = "zh_CN";
        public long DepartmentID { get; set; }
        public long Offset { get; set; } = 100;
        public int Size { get; set; } = 100;
        public string Order { get; set; } = "entry_asc";
    }

    public class DepartmentMemberInfoResponse : ErrorResponse
    {
        public bool HasMore { get; set; }
        public List<DepartmentMemberInfo> UserList { get; set; }
    }

    public class DepartmentMemberInfo
    {
        public string userid { get; set; }
        public int order { get; set; }
        public string dingId { get; set; }
        public string mobile { get; set; }
        public string tel { get; set; }
        public string workPlace { get; set; }
        public string remark { get; set; }
        public bool isAdmin { get; set; }
        public bool isBoss { get; set; }
        public bool isHide { get; set; }
        public bool isLeader { get; set; }
         

        public string name { get; set; }

        public bool active { get; set; }

        

        public List<int> department { get; set; }

        public string position { get; set; }

        public string orgEmail { get; set; }

        public string email { get; set; }

        public string avatar { get; set; }
        public string jobnumber { get; set; }
    }

}
