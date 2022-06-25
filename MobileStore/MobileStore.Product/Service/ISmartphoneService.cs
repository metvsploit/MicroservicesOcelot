using MobileStore.Domain.Entities;
using MobileStore.Product.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileStore.Product.Domain.Service
{
    public interface ISmartphoneService
    {
        Task<SmartphoneResponse<Smartphone>> CreateSmartphone(Smartphone product);
        Task<SmartphoneResponse<Smartphone>> GetSmartphoneById(int id);
        Task<SmartphoneResponse<IEnumerable<Smartphone>>> GetAllSmartphone();
    }
}
