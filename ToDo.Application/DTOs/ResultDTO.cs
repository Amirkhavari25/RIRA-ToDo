using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.DTOs
{
    public class ResultDTO<T> where T : class
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> ValidationErrors { get; set; }
        public int? ErrorCode { get; set; }

        public ResultDTO(T data)
        {
            Success = true;
            Data = data;
            ValidationErrors = new List<string>();
        }

        public ResultDTO(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
            ValidationErrors = new List<string>();
        }

        public ResultDTO(List<string> validationErrors)
        {
            Success = false;
            ValidationErrors = validationErrors;
            ErrorMessage = string.Join(", ", validationErrors);
        }

        public static ResultDTO<T> SuccessResult(T data)
        {
            return new ResultDTO<T>(data);
        }

        public static ResultDTO<T> FailureResult(string errorMessage)
        {
            return new ResultDTO<T>(errorMessage);
        }

        public static ResultDTO<T> FailureResult(List<string> validationErrors)
        {
            return new ResultDTO<T>(validationErrors);
        }

        public static ResultDTO<T> FailureResult(string errorMessage, int errorCode)
        {
            return new ResultDTO<T>(errorMessage) { ErrorCode = errorCode };
        }
    }
}
