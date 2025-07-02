using Microsoft.AspNetCore.Http;

namespace UserManagementSystem.Models.ResponseModel
{
    /// <summary>
    /// Api responses Class to 
    /// handle generic responses.
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = " ";
        public T Data { get; set; }
        public ApiResponse(T data, string message = "",bool success = true)
        {
            
            Success = success;
            Message = message;
            Data = data;
        }
        public ApiResponse(string message, bool success)
        {
            Message = message;
            Success = success;
            Data = default;
        }
    }

}
   

