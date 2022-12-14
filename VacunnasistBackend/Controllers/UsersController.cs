using Microsoft.AspNetCore.Mvc;
using VacunassistBackend.Models;
using VacunassistBackend.Services;
using VacunassistBackend.Helpers;
using VacunassistBackend.Models.Filters;
using VacunassistBackend.Utils;

namespace VacunassistBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IDevelopedVaccinesService _vaccinesService;
        private readonly IConfiguration _configuration;

        public UsersController(IUsersService usersService, IDevelopedVaccinesService vaccinesService, IConfiguration configuration)
        {
            this._usersService = usersService;
            this._vaccinesService = vaccinesService;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterRequest model)
        {
            if (_usersService.Exists(model.UserName))
                return BadRequest(new
                {
                    message = "Nombre de usuario ya registrado en el sistema"
                });

            var result = _usersService.Register(model);
            if (result)
                return Ok(new
                {
                    message = "Usuario registrado correctamente"
                });

            return BadRequest(new
            {
                message = "Ocurrió un inconveniente al registrar el usuario, intente nuevamente"
            });
        }

        [HttpPost]
        [Route("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (_usersService.Exists(request.UserName) == false)
                return BadRequest(new
                {
                    message = "Nombre de usuario no encontrado"
                });

            var user = _usersService.Get(request.UserName);
            var model = new UpdateUserRequest();
            model.Password = RandomGenerator.RandomString(10);
            _usersService.Update(user.Id, model);

            return Ok(new
            {
                message = "Contraseña reseteada correctamente"
            });
        }

        [Helpers.Authorize]
        [HttpPost]
        [Route("{id}/change-password")]
        public IActionResult ChangePassword(int id, [FromBody] ChangePasswordRequest model)
        {
            if (string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.NewPassword))
                return BadRequest("Invalid Request");

            var user = _usersService.Get(id);
            if (user == null)
                return BadRequest(new
                {
                    message = "El usuario no existe"
                });

            var checkCredentials = _usersService.Authenticate(user.UserName, model.Password);
            if (checkCredentials == null)
            {
                return BadRequest(new
                {
                    message = "La contraseña ingresada es incorrecta"
                });
            }

            _usersService.Update(id, new UpdateUserRequest() { Password = model.NewPassword });

            return Ok(new
            {
                message = "Usuario actualizado correctamente"
            });
        }

        [Helpers.Authorize]
        [HttpPost]
        [Route("{id}/add-vaccine")]
        public IActionResult AddVaccine(int id, [FromBody] AddVaccineRequest model)
        {
            var user = _usersService.Get(id);
            if (user == null)
                return BadRequest(new
                {
                    message = "El usuario no existe"
                });

            _usersService.AddVaccine(id, model);

            return Ok(new
            {
                message = "Usuario actualizado correctamente"
            });
        }

        [Helpers.Authorize]
        [HttpPost]
        [Route("{id}/delete-vaccine")]
        public IActionResult DeleteVaccine(int id, [FromBody] int vaccineId)
        {
            var user = _usersService.Get(id);
            if (user == null)
                return BadRequest(new
                {
                    message = "El usuario no existe"
                });

            _usersService.DeleteVaccine(id, vaccineId);

            return Ok(new
            {
                message = "Usuario actualizado correctamente"
            });
        }

        [HttpGet]
        [Route("profile")]
        [Helpers.Authorize]
        public IActionResult MyProfile()
        {
            var id = User.GetId().Value;
            var user = _usersService.Get(id);
            return Ok(new
            {
                user
            });
        }

        [HttpGet]
        [Route("{id}/can-delete")]
        [Helpers.Authorize]
        public IActionResult CanBeDeleted(int id)
        {
            return Ok(_usersService.CanBeDeleted(id));
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit(int? id, [FromBody] UpdateUserRequest model)
        {
            if (id == null)
            {
                return NotFound();
            }

            _usersService.Update(id.Value, model);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_usersService.Get(id));
        }

        [HttpGet]
        public IActionResult Get([FromQuery] UsersFilterRequest filter)
        {
            return Ok(new
            {
                users = _usersService.GetAll(filter)
            });
        }
    }
}