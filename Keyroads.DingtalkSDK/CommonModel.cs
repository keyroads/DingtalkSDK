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
}
