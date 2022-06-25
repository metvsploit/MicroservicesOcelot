using Microsoft.EntityFrameworkCore;
using MobileStore.Domain.Entities;
using MobileStore.Product.Domain.Data;
using MobileStore.Product.Domain.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileStore.Product.Domain.Service
{
    public class SmartphoneService : ISmartphoneService
    {
        private readonly DataContext _db;

        public SmartphoneService(DataContext db) =>
            _db = db;

        public async Task<SmartphoneResponse<Smartphone>> CreateSmartphone(Smartphone product)
        {
            var response = new SmartphoneResponse<Smartphone>();

            try
            {
                await _db.AddAsync(product);
                await _db.SaveChangesAsync();
                response.Descripton = "Продукт успешно добавлен";
                response.Data = product;
                return response;
            }
            catch (Exception ex)
            {
                return new SmartphoneResponse<Smartphone> () { Descripton = $"Ошибка {ex.Message}"};
            }
        }

        public async Task<SmartphoneResponse<IEnumerable<Smartphone>>> GetAllSmartphone()
        {
            var response = new SmartphoneResponse<IEnumerable<Smartphone>>();

            try
            {
                var smartphonesList = await _db.Products.ToListAsync();

                response.Data = smartphonesList;
                response.Descripton = "Успешный запрос";
            }
            catch(Exception ex)
            {
                return new SmartphoneResponse<IEnumerable<Smartphone>> { Descripton = $"Ошибка {ex.Message}."};
            }
            return response;
        }

        public async Task<SmartphoneResponse<Smartphone>> GetSmartphoneById(int id)
        {
            var response = new SmartphoneResponse<Smartphone>();

            try
            {
                var smartphone = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);

                response.Data = smartphone;
                response.Descripton = "Успешный запрос";
            }
            catch(Exception ex)
            {
                return new SmartphoneResponse<Smartphone>() { Descripton = $"Ошибка {ex.Message}" };
            }

            return response;
        }
    }
}
