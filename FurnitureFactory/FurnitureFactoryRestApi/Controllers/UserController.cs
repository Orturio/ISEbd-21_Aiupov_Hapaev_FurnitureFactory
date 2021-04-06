using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureFactoryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly UserLogic _logic;
        public UserController(UserLogic logic)
        {
            _logic = logic;
        }
        [HttpGet]
        public UserViewModel Login(string email, string password) => _logic.Read(new UserBindingModel { Email = email, Password = password })?[0];

        [HttpGet]
        public UserViewModel UserList() => _logic.Read(null)?[0];

        [HttpPost]
        public void Register(UserBindingModel model) => _logic.CreateOrUpdate(model);
        [HttpPost]
        public void UpdateData(UserBindingModel model) => _logic.CreateOrUpdate(model);
    }
}
