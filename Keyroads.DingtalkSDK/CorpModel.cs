using System.Collections.Generic;

namespace Keyroads.DingtalkSDK
{
    public class DepartmentMemberInfoRequest
    {
        public string Lang { get; set; } = "zh_CN";
        public long DepartmentID { get; set; }
        public long Offset { get; set; } = 100;
        public int Size { get; set; } = 100;
        public string Order { get; set; } = "entry_asc";
    }

    public class DepartmentMemberInfoResponse : ErrorResponse
    {
        public bool hasMore { get; set; }
        public List<DepartmentMemberInfo> userList { get; set; }
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
}
