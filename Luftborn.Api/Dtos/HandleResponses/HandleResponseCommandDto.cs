namespace Luftborn.Api.Dtos.HandleResponses
{
    public class HandleResponseCommandDto<T> where T : class
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Exception? Exception { get; set; }
    }
}
