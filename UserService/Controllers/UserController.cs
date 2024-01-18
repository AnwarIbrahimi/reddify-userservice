using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.DTO;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;

        public UserController(IUserRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<UserReadDTO>> GetAllUsers()
        {
            Console.WriteLine("--> Getting Users.....");

            var userItems = _repository.GetAllUsers();

            return Ok(_mapper.Map<IEnumerable<UserReadDTO>>(userItems));

        }

        [HttpGet("{id}", Name = "GetUserByID")]
        public ActionResult<UserReadDTO> GetUserByID(int id)
        {
            var userItem = _repository.GetUserByID(id);
            if (userItem != null)
            {
                return Ok(_mapper.Map<UserReadDTO>(userItem));
            }

            return NotFound();
        }


        [HttpPost("createUser")]
        public ActionResult<UserReadDTO> CreateUser(UserCreateDTO user)
        {
            try
            {
                // Extract user data from the request
                string uid = user.Uid;  // Assuming your UserCreateDTO contains a property for UID
                string email = user.Email;  // Assuming your UserCreateDTO contains a property for Email
                                            // Add other user properties as needed

                // Perform operations to store the user data in your user database
                // Example:
                var userModel = _mapper.Map<Users>(user);
                userModel.Uid = uid;
                userModel.Email = email;

                _repository.CreateUser(userModel);
                _repository.saveChanges();

                var userReadDTO = _mapper.Map<UserReadDTO>(userModel);

                return CreatedAtRoute(nameof(GetUserByID), new { Id = userReadDTO.Id }, userReadDTO);
            }
            catch (Exception ex)
            {
                // Handle any exceptions or errors that may occur during user creation
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
