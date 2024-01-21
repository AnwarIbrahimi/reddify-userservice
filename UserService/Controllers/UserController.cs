using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.DTO;
using UserService.Models;
using UserService.RabbitMQ;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IMessageBusClient _messageBusClient;

        public UserController(IUserRepo repository, IMapper mapper, IConfiguration configuration, IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
            _messageBusClient = messageBusClient;
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
        [HttpDelete("deleteUser/{uid}")]
        public ActionResult DeleteUser(string uid)
        {
            try
            {
                // Find the user by UID
                var userItem = _repository.GetUserByUid(uid);

                if (userItem == null)
                {
                    return NotFound();
                }

                string userUid = userItem.Uid;


                _messageBusClient.PublishUserDeletion(userUid);

                // Delete the user from the user repository
                _repository.DeleteUser(userItem);
                _repository.saveChanges();

                return NoContent(); // Successful deletion
            }
            catch (Exception ex)
            {
                // Handle any exceptions or errors that may occur during user deletion
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
