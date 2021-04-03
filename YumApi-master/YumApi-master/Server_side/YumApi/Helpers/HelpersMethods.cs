//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using YumApi.Models;


//namespace YumApi.Helpers
//{
//    public class HelpersMethods
//    {

//        public static User WithoutPassword(this User user)
//        {
//            if (user == null) return null;

//            user.Password = null;
//            return user;
//        }
//        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
//        {
//            if (users == null) return null;

//            return users.Select(x => x.WithoutPassword());
//        }



//    }
//}
