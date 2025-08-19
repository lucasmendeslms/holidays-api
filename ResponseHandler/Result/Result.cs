namespace HolidayApi.ResponseHandler
{
    public sealed class Result<T>
    {
        public T? Value { get; private set; }
        public Error? Error { get; private set; }
        public bool IsSuccess => Error == null;
        public bool IsFailure => !IsSuccess;

        //private constructors
        private Result(T value) => Value = value;
        private Result(Error error) => Error = error;

        //factory methods to create a result object
        public static Result<T> Success(T value) => new(value);
        public static Result<T> Failure(Error error) => new(error);

        public Result<U> Map<U>(Func<T, U> transform)
        {
            return IsSuccess ? Result<U>.Success(transform(Value!)) : Result<U>.Failure(Error!);
        }

        public Result<U> Bind<U>(Func<T, Result<U>> nextOperation)
        {
            return IsSuccess ? nextOperation(Value!) : Result<U>.Failure(Error!);
        }

        // https://www.tutorialsteacher.com/csharp/csharp-delegates

        // Assistir a aula do Balta sobre delegates
    }

    public enum OperationTypeCode
    {
        Create = 201,
        Update = 200,
        Delete = 204
    }
}