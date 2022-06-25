using MobileStore.Domain.Entities;
using System.Collections.Generic;

namespace MobileStore.Authentication.Domain.Response
{
    public class UserResponse<T>
    {
        public string Description { get ; set; }
        public T Data { get; set; }
    }
}
