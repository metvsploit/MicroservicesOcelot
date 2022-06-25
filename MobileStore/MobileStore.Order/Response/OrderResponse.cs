namespace MobileStore.Order.Domain.Response
{
    public class OrderResponse<T>
    {
        public string Description { get; set; }
        public T Data { get; set; }
    }
}
