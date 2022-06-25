namespace MobileStore.Customer.Domain.Response
{
    public class CustomerResponse<T>
    {
        public string Description { get; set; }
        public T Data { get; set; }
    }
}
