using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace FurnitureFactoryBusinessLogics.ViewModels
{
    [DataContract]
    public class UserViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Логин пользователя")]
        public string Login { get; set; }

        [DataMember]
        [DisplayName("Email пользователя")]
        public string Email { get; set; }

        [DataMember]
        [DisplayName("Пароль пользователя")]
        public string Password { get; set; }
    }
}
