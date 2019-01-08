using System;

namespace Inkett.Web.Exceptions
{
    public class UserNotFoundException : Exception
    {

        public UserNotFoundException(int userId) : base($"Something went wrong with loading user: {userId}")
        {
        }

    }
}
