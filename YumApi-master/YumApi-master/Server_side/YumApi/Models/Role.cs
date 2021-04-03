using System.Collections.Generic;

namespace YumApi.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<User> User { get; set; }
    }
}
