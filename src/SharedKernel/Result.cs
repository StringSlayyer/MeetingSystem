using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SharedKernel
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool isFailuer => !IsSuccess;
        public Error? Error { get; set; }

        public Result(bool isSuccess, Error error)
        {
            if(isSuccess && error != Error.None || !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }
            this.IsSuccess = isSuccess;
            this.Error = error;
        }

        public static Result Success() => new(true, Error.None);
        public static Result<T> Success<T>(T data) => new(data, true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        public static Result<T> Failure<T>(Error error) => new(default, false, error);
    }

    public class Result<T> : Result
    {
        private readonly T? _data;

        public Result(T? data, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _data = data;
        }

        [NotNull]
        public T Data => IsSuccess
            ? _data!
            : throw new InvalidOperationException("The value of a failure result cant be accessed.");

        public static implicit operator Result<T>(T? data) =>
            data is not null ? Success(data) : Failure<T>(Error.NullValue);

        public static Result<T> ValidationFailure(Error error) => new(default, false, error);
    }
}
