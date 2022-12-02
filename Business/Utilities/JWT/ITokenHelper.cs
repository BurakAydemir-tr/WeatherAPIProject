using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user);
    }
}
