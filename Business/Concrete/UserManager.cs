using Business.Abstract;
using Business.Utilities.JWT;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User{Id = 1, Name = "Admin", Surname = "Admin", Email = "admin@admin.com", Password = "1234"},
            new User{Id = 2, Name = "Deneme", Surname = "Deneme", Email = "deneme@deneme.com", Password = "4321"},
        };

        private readonly ITokenHelper _tokenHelper;

        public UserManager(ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }

        public AccessToken Login(string email, string password)
        {
            var user=_users.SingleOrDefault(x=>x.Email == email && x.Password == password);

            if (user!=null)
            {
                return _tokenHelper.CreateToken(user);
            }
            return null;
        }
    }
}
