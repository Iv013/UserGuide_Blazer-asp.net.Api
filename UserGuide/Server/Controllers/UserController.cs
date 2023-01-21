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
        public async Task<ActionResult<ServiceResponse>> CreateUser(UserData userData)
        {           
          var result =await _userRepo.Add(userData);    
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse>> DeleteActiveUser(int id)
        {
            var result = await _userRepo.DisableUser(id);
            return Ok(result);
        }
         
        [HttpPut]
        public async Task<ActionResult<ServiceResponse>> UpdateActiveUser(UserData userData)
        {
            var result  = await _userRepo.Update(userData);
            return Ok(result);
        }

    



    }
}
