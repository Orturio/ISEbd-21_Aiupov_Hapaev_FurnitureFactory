using FurnitureFactoryBusinessLogics.BindingModels;
using FurnitureFactoryBusinessLogics.BusinessLogics;
using FurnitureFactoryBusinessLogics.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureFactoryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ClientController : Controller
    {
        private readonly UserLogic _logic;
        public ClientController(UserLogic logic)
        {
            _logic = logic;
        }
        [HttpGet]
        public UserViewModel Login(string login, string password) => _logic.Read(new
UserBindingModel
        { Email = login, Password = password })?[0];
        [HttpPost]
        public void Register(UserBindingModel model) => _logic.CreateOrUpdate(model);
        [HttpPost]
        public void UpdateData(UserBindingModel model) => _logic.CreateOrUpdate(model);
    }
}
