using Microsoft.AspNetCore.Http;

namespace UserManagementSystem.Models
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
    }

}
   

