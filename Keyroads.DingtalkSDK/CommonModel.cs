namespace Keyroads.DingtalkSDK
{
    public interface IResponseValidation
    {
        bool IsValidate();
    }

    public class ErrorResponse : IResponseValidation
    {
        public int errcode { get; set; }

        public string errmsg { get; set; }
        public virtual bool IsValidate()
        {
            return true;
        }
    }

    public class BaseGetRequest
    {
        public string access_token { get; set; }
    }

    public class DepartmentBaseGetRequest : BaseGetRequest
    {
        public string id { get; set; }
        public string lang { get; set; } = "zh_CN";
    }

    public class UserBaseGetRequest : BaseGetRequest
    {
        public string userid { get; set; }
        public string lang { get; set; } = "zh_CN";
    }
}
