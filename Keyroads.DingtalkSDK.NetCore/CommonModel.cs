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

    public class BaseGetContactRequest : BaseGetRequest
    {
        public string lang { get; set; } = "zh_CN";

        public string id { get; set; }
    }
}
