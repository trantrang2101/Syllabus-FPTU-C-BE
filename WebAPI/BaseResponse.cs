using Newtonsoft.Json;

namespace WebAPI
{
    public class BaseResponse<T> where T : class
    {
        public string code;
        public string message;
        public T data;

        public BaseResponse()
        {
        }

        public BaseResponse(string code, string message, T data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }
        
        public BaseResponse(string code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(new BaseResponse<T>(this.code,this.message,this.data));
        }

        public static BaseResponse<T> successWithMessage(string message)
        {
            return withMessageAndCode(message, "00");
        }

        public BaseResponse<T> successWithData(T result)
        {
            return successWithMessageAnData("Thành công!", result);
        }

        public static BaseResponse<T> withMessageAndCode(string message, string code)
        {
            return new BaseResponse<T>(code, message);
        }

        public BaseResponse<T> successWithMessageAnData(string message, T result)
        {
            return withAllAttribute("00", message, result);
        }

        public static BaseResponse<T> errWithMessage(string message)
        {
            return new BaseResponse<T>("101", message);
        }

        public BaseResponse<T> errWithMessageAndData(string message, T data)
        {
            return new BaseResponse<T>("101", message, data);
        }

        public BaseResponse<T> errWithData(T result)
        {
            return withAllAttribute("-1", "Thất bại", result);
        }

        public BaseResponse<T> errWithDataAndMessage(string message, T result)
        {
            return withAllAttribute("-1", message, result);
        }

        public BaseResponse<T> withAllAttribute(string code, string message, T result)
        {
            return new BaseResponse<T>(code, message, result);
        }

        public BaseResponse<T> successWithMessageAndData(string message, T result)
        {
            return withAllAttribute("00", message, result);
        }
    }
}
