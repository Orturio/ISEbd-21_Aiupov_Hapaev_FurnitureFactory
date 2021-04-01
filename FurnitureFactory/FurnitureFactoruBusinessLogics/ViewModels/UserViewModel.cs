using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using FurnitureFactoryBusinessLogics.Enums;

namespace FurnitureFactoryBusinessLogics.ViewModels
{
    [DataContract]
    public class UserViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Роль пользователя")]
        public UserRole Role { get; set; }

        [DataMember]
        [DisplayName("Email пользователя")]
        public string Email { get; set; }

        [DataMember]
        [DisplayName("Пароль пользователя")]
        public string Password { get; set; }
    }
}
