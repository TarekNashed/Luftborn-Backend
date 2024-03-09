namespace Luftborn.Application.Model
{
    public class HandlerResponse<T> where T : class
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public Exception? Exception { get; set; }
    }
}
