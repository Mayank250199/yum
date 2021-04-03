using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YumApi.Data;
using YumApi.Models;
namespace YumApi.Services
{
    public interface IBloggerService
    {
        Task<Token> AuthenticateBlogger(string username, string password);
        //IEnumerable<User> GetAll();
        //User GetById(int id);
    }

    public class BloggerService : IBloggerService
    {

        private readonly YumDbContext _context;

        public BloggerService(YumDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }


        public async Task<Token> AuthenticateBlogger(string username, string password)
        {
            var user = _context.User.SingleOrDefault(x => x.Username == username && x.Password == password);

            var role = await _context.Role.FindAsync(user.Role);

            // return null if user not found
            if (user == null)
                return null;

            if (role.RoleName != "Blogger")
                return null;

            var appSettings = Configuration.GetSection("AppSettings");

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings["Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, role.RoleName)
                  }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            Token GetUser_Token = new Token
            {
                User_Token = tokenHandler.WriteToken(token),
                User = user
            };


            //return user.WithoutPassword();
            return GetUser_Token;
        }

    }
}
