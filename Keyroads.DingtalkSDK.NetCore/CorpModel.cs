using System.Collections.Generic;

namespace Keyroads.DingtalkSDK
{
    public class DepartmentListResponse : ErrorResponse
    {
        public List<DepartmentListInfo> department { get; set; }
    }

    public class DepartmentListInfo
    {
        public long id { get; set; }
        public string name { get; set; }
        public long parentid { get; set; }
        public bool createDeptGroup { get; set; }
        public bool autoAddUser { get; set; }
    }

    public class UserListGetRequest : DepartmentBaseGetRequest
    {
        public long department_id { get; set; }
        public long offset { get; set; } = 100;
        public int size { get; set; } = 100;
        public string order { get; set; } = "entry_asc";
    }

    public class UserListResponse : ErrorResponse
    {
        public bool hasMore { get; set; }
        public List<UserListInfo> userList { get; set; }
    }

    public class UserGetResponse : ErrorResponse
    {
        public string userid { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public string workPlace { get; set; }
        public string remark { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string orgEmail { get; set; }
        public bool active { get; set; }
        //orderInDepts
        public bool isAdmin { get; set; }
        public bool isBoss { get; set; }
        public string dingId { get; set; }
        //isLeaderInDepts
        public bool isHide { get; set; }
        public int[] department { get; set; }
        public string position { get; set; }
        public string avatar { get; set; }
        public string jobnumber { get; set; }
        //extattr
    }

    public class UserListInfo : UserGetResponse
    {
        public long order { get; set; }
        public bool isLeader { get; set; }
    }

    public class GetJsApiTicketResponse : ErrorResponse
    {
        public string ticket { get; set; }

        public int expires_in { get; set; }

        public override bool IsValidate()
        {
            return !string.IsNullOrEmpty(ticket);
        }
    }

    public class GetUserInfoResponse : ErrorResponse
    {
        public string userid { get; set; }

        public string deviceId { get; set; }

        public bool is_sys { get; set; }

        public int sys_level { get; set; }

        public override bool IsValidate()
        {
            return !string.IsNullOrEmpty(userid);
        }
    }

    public class GetTokenRequest
    {
        public string corpid { get; set; }

        public string corpsecret { get; set; }
    }

    public class GetTokenResponse : ErrorResponse
    {
        public string access_token { get; set; }
    }
}
