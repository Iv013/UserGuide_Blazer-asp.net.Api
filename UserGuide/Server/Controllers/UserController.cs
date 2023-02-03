using Microsoft.AspNetCore.Mvc;
using UserGuide.Core.Repository;
using UserGuide.Server.Services;
using UUserGuide.Core.Data;

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


            if (userRepo.GetAllUsers().GetAwaiter().GetResult().Count() == 0)
            {
                InitializedBD.MethodInit(_userRepo);
            }
          
        }

        [HttpGet]
        public async Task<ActionResult<List<UserData>>> GetUsers()
        {
            var users = await _userRepo.GetAllUsers(x => x.UserEnable == true);
            return  Ok(users) ;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserData>> GetSingleUser(int id)
        {
            var users = await _userRepo.FirstOfDefault(x => x.Userid == id);
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> CreateUser(UserData userData)
        {
            
            var result =await _userRepo.AddUser(userData);

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
            var result = await _userRepo.UpdateUser(userData);
            CheckUserInActiveDirectory(userData, result);
            return Ok(result);
        }

        private void CheckUserInActiveDirectory(UserData userData, ServiceResponse result)
        {
            string[] domenLogin = userData.UserLogin.Split('\\').ToArray();
            var userInAD = _ADservice.ContainsUser(domenLogin[0], domenLogin[1]);
            if (!userInAD)
            {
                result.Success = result.Success == 200 ? 102 : result.Success;
                result.Message = result.Message + WebConstant.FindUserInADNotSuccess;
            }
            else
            {
                result.Message = result.Message + WebConstant.FindUserInADSuccess;
            }
        }


   
      
    }
}
