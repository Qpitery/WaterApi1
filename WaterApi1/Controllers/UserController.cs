using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WaterApi1.DB;
using WaterApi1.DTO;


namespace WaterApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly WaterShopContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UsersController(WaterShopContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(UserDTO login)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(l => l.UserName == login.UserName && l.Password == login.Password) ;
            if (user == null)
            {
                return NotFound();
            }

            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password
               
            };

        }

        [HttpPost("registration")]
        public async Task<ActionResult<UserDTO>> Register(UserDTO newUser)
        {
            // Проверьте, существует ли уже пользователь с таким именем
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == newUser.UserName);
            if (existingUser != null)
            {
                return Conflict(new { message = "Пользователь уже существует" });
            }

            // Создайте нового пользователя и добавьте его в базу данных
            var user = new User
            {
                UserName = newUser.UserName,
                Password = newUser.Password,
                Email = newUser.Email

            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Login), new { userName = newUser.UserName, password = newUser.Password, email = newUser.Email }, newUser);
        }

        [HttpGet("rememberPassword")]
        public async Task<ActionResult> RememberPassword(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                // Email не найден в базе данных
                return NotFound();
            }

            // Получаем путь к файлу HtmlRememberPage.html
            string htmlFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "C:\\Users\\Z8908\\source\\repos\\WaterApi1\\WaterApi1\\Controllers\\HtmlRememberPage.html");

            // Считываем содержимое файла в строку
            string htmlPage = System.IO.File.ReadAllText(htmlFilePath);

            // Вставляем пароль в HTML-страницу
            htmlPage = htmlPage.Replace("<h1 style=\"color: #5c68e2;\"><br></h1>", $"<h1 style=\"color: #5c68e2;\">{user.Password}</h1>");

            // Отправляем HTML-страницу по электронной почте
            await EmailService.SendEmail(email, htmlPage);
            return Ok();
        }
    }
}