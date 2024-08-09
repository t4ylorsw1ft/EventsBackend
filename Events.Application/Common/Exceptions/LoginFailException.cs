using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Common.Exceptions
{
    public class LoginFailException : Exception
    {
        public LoginFailException()
        : base($"Неверный логин или пароль")
        {

        }
    }
}
