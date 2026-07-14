using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Application.Common.Models
{
    public class ApiResponse<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public IDictionary<string, string[]>? Errors { get; set; }

        public static ApiResponse<T> Success(T data, string message = "Operation completed successfully.")
            => new() { Succeeded = true, Data = data, Message = message };

        public static ApiResponse<T> Fail(string message, IDictionary<string, string[]>? errors = null)
            => new() { Succeeded = false, Message = message, Errors = errors };
    }

    public class ApiResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public IDictionary<string, string[]>? Errors { get; set; }

        public static ApiResponse Success(string message = "Operation completed successfully.")
            => new() { Succeeded = true, Message = message };

        public static ApiResponse Fail(string message, IDictionary<string, string[]>? errors = null)
            => new() { Succeeded = false, Message = message, Errors = errors };
    }
}
