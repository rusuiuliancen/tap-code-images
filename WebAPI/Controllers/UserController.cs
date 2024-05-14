using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Converter;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IRepository<User> _userRepository;


        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet("get")]
        public IEnumerable<UserDto> Get()
        {
            return _userRepository.GetAll().Select(user=> new UserDto()
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                ProfilePicture = ImageConverter.ImageToBase64(user.ProfilePicture),
                TypeId = user.TypeId
            });
        }

        [HttpPost("add")]
        public ObjectResult Add(UserDto userDto)
        {
            var user = new User(userDto.Name, userDto.Email, userDto.Password, userDto.TypeId);
            user.UpdateProfilePicture(ImageConverter.Base64ToImage(userDto.ProfilePicture));

            _userRepository.Add(user);

            _userRepository.SaveChanges();

            return Ok("Added successfully.");
        }

        [HttpPut("update")]
        public ObjectResult Update(Guid id, UserDto userDto)
        {
            throw new NotImplementedException();
        }



        [HttpPut("delete")]
        public ObjectResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
