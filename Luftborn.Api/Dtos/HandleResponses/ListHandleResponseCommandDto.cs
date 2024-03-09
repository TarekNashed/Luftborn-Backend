namespace Luftborn.Api.Dtos.HandleResponses
{
    public class ListHandleResponseCommandDto<T>: HandleResponseCommandDto<T> where T : class
    {
        public int TotalDataCount { get; set; }
    }
}
