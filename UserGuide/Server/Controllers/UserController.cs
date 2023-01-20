using Microsoft.AspNetCore.Mvc;
using UserGuide.Shared.Models;
using UserGuide.Server.Repository;

namespace UserGuide.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo)
        {
        _userRepo = userRepo;

            if (userRepo.GetAll().GetAwaiter().GetResult().Count() == 0)
            {
                var user = new UserData()
                {
                    FirstName = "test1",
                    LastName = "test1",
                    Patronymic = "test1",
                    UserLogin = "sdfs.com\\test1",
                    UserEnable = true
                };
                _userRepo.Add(user);
                _userRepo.Save();
            }
           

        }

        [HttpGet]
        public async Task<ActionResult<List<UserData>>> GetUsers()
        {
            var users = await _userRepo.GetAll(x => x.UserEnable == true);
            return  Ok(users) ;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserData>>> GetSingleUser(int id)
        {
            var users = await _userRepo.FirstOfDefault(x => x.Userid == id);
            return Ok(users);
        }

        [HttpPost]
        public ActionResult<List<UserData>> CreateUser(UserData userData)
        {
            if (ModelState.IsValid)
            {
                _userRepo.Add(userData);
                _userRepo.Save();
            }

            return Ok(_userRepo.GetAll(x => x.UserEnable == true));
        }

        [HttpPut("{id}")]
        public void DeleteActiveUser(int id)
        {
            //, isTracking: false
            var obj = _userRepo.FirstOfDefault(x => x.Userid == id).Result;
            if (ModelState.IsValid)
            {
                obj.UserEnable = false;
                _userRepo.Update(obj);
                _userRepo.Save();
            }
           
        }

        [HttpPut]
        public void UpdateActiveUser(UserData userData)
        {
            //, isTracking: false
            var obj = _userRepo.FirstOfDefault(x => x.Userid == userData.Userid).Result;
            if (ModelState.IsValid)
            {
                obj.FirstName = userData.FirstName;
                obj.LastName = userData.LastName;
                obj.Patronymic = userData.Patronymic;
                obj.UserLogin = userData.UserLogin;


                _userRepo.Update(obj);
                _userRepo.Save();
            }

        }



    }
}
