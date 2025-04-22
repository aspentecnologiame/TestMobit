namespace TestMobit.Api.Models.Request.Base
{
    public abstract class BaseRequest<T> where T : class
    {
        public T Data { get; set; }

        public BaseRequest(T data)
        {
            Data = data;
        }
    }
}
