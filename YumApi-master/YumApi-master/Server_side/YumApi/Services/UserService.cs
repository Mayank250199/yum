using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Writers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using YumApi.Data;
using YumApi.Models;

namespace YumApi.Services
{
    public interface IUserService
    {
        Task<Token> AuthenticateUser(string username, string password);
        //IEnumerable<User> GetAll();
        public string EncodePasswordToBase64(string password);

        public string DecodeFrom64(string encodedData);
    }
    public class UserService : IUserService
    {
        
        private readonly YumDbContext _context;
        public string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public UserService(YumDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public object ProtectedData { get; private set; }

        public async Task<Token> AuthenticateUser(string username, string password)
        {
            var user = _context.User.SingleOrDefault(x => x.Username == username && x.Password == password);

            var role = await _context.Role.FindAsync(user.RoleId);

            // return null if user not found
            if (user == null)
                return null;

            if (role.RoleName != "User")
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



        //public IEnumerable<User> GetAll()
        //{
        //    return _users.WithoutPasswords();
        //}

        //public User GetById(int id)
        //{
        //    var user = _users.FirstOrDefault(x => x.Id == id);
        //    return user.WithoutPassword();
        //}
    }

}
