using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Models;
using UserManagementSystem.Repository.Interface;

namespace UserManagementSystem.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (await _repository.EmailExists(user.Email))
                return Conflict("Email already exists");

            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;

            var created = await _repository.Create(user);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.GetAll();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _repository.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, User request)
        {
            var user = await _repository.GetById(id);

            if (user == null)
                return NotFound();

            if (!string.IsNullOrEmpty(request.Email))
            {
                var emailExists = await _repository.EmailExists(request.Email);

                if (emailExists && request.Email != user.Email)
                    return Conflict("Email already exists");

                user.Email = request.Email;
            }

            if (!string.IsNullOrEmpty(request.Name))
                user.Name = request.Name;

            if (request.UserType != Guid.Empty)
                user.UserType = request.UserType;

            user.UpdatedAt = DateTime.UtcNow;

            await _repository.Update(user);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _repository.GetById(id);

            if (user == null)
                return NotFound();

            await _repository.SoftDelete(id);

            return NoContent();
        }
    }
}
