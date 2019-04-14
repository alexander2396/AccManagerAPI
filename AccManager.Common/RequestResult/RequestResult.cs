using Microsoft.AspNetCore.Http;

namespace AccManager.Common.RequestResult
{
    public class RequestResult
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public bool IsSuccess => StatusCode == StatusCodes.Status200OK;

        public RequestResult() { StatusCode = StatusCodes.Status400BadRequest; }

        public RequestResult(int statusCode)
        {
            StatusCode = statusCode;
        }

        public void SetStatusOK()
        {
            StatusCode = StatusCodes.Status200OK;
        }

        public void SetStatusBadRequest()
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }

        public void SetStatusInternalServerError()
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public void SetStatusInternalServerError(string message)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
            Message = message;
        }
    }

    public class RequestResult<T> : RequestResult
    {
        public T Obj { get; set; }

        public RequestResult() { }

        public RequestResult(int statusCode) : base(statusCode) { }
    }
}
