using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace FurnitureFactoryBusinessLogics.ViewModels
{
    [DataContract]
    public class EmployeeViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Логин сотрудника")]
        public string Login { get; set; }

        [DataMember]
        [DisplayName("Email сотрудника")]
        public string Email { get; set; }

        [DataMember]
        [DisplayName("Пароль сотрудника")]
        public string Password { get; set; }
    }
}
