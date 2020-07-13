using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Util
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {

        }


    }
}