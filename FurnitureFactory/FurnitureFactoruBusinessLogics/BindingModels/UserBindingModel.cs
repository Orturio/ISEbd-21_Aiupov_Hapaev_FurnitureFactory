using System.Runtime.Serialization;
using System.Collections.Generic;

namespace FurnitureFactoryBusinessLogics.BindingModels
{
    [DataContract]
    public class UserBindingModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string Login { get; set; }
        
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> UserPurchases { get; set; }
    }
}
