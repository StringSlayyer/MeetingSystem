using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

        public static Result Success() =>
            new Result { IsSuccess = true };
        public static Result Failure(string errorMessage) =>
            new Result { IsSuccess = false, ErrorMessage = errorMessage };
    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public static Result<T> Success(T value) =>
            new Result<T> { IsSuccess = true, Data = value };
        public static Result<T> Failure(string errorMessage) =>
            new Result<T> { IsSuccess = false, ErrorMessage = errorMessage };
    }
}
