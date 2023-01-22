using Microsoft.AspNetCore.Mvc;
using UserGuide.Shared.Models;
using UserGuide.Server.Repository;
using UserGuide.Server.Services;

namespace UserGuide.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IActiveDirectoryService _ADservice;

        public UserController(IUserRepository userRepo
            , IActiveDirectoryService ADservice)
        {
        _userRepo = userRepo;
            _ADservice = ADservice;


            if (userRepo.GetAll().GetAwaiter().GetResult().Count() == 0)
            {
                InitializedBD.MethodInit(_userRepo);
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

            var UserLogin = userData.UserLogin.Split('\\').ToArray();
            CheckUserInActiveDirectory(userData, result);
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
            var result = await _userRepo.Update(userData);
            CheckUserInActiveDirectory(userData, result);
            return Ok(result);
        }

        private void CheckUserInActiveDirectory(UserData userData, ServiceResponse result)
        {
            var UserLogin = userData.UserLogin.Split('\\').ToArray();
            var userInAD = _ADservice.ContainsUser(UserLogin[0], UserLogin[1]);
            if (!userInAD && result.Success == 200)
            {
                result.Success = 102;
                result.Message = result.Message +", но данный пользователь отсутcтвует в Active Directory";
            }
        }




    }
}
