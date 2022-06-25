
namespace MobileStore.Product.Domain.Response
{
    public class SmartphoneResponse<T>
    {
        public string Descripton { get; set; }
        public T Data { get; set; }
    }
}
