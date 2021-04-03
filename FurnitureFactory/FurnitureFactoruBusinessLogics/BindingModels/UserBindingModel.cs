using System.Collections.Generic;
using FurnitureFactoryBusinessLogics.Enums;

namespace FurnitureFactoryBusinessLogics.BindingModels
{
    public class UserBindingModel
    {
        public int? Id { get; set; }

        public UserRole Role { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Dictionary<int, (string, int)> UserPurchases { get; set; }
    }
}
