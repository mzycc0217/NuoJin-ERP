using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using XiAnOuDeERP.Models.Enum;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto
{
    /// <summary>
    /// Header值
    /// </summary>
    public class UserIdentity:IIdentity
    {
        public UserIdentity(string name,long userId,string tokenStr,TokenDto token)
        {
            Name = name;
            UserId = userId;
            TokenStr = tokenStr;
            Token = token;
        }

        public string Name { get; set; }

        public long UserId { get; set; }

        public string TokenStr { get; set; }

        public TokenDto Token { get; set; }

        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }
    }

    public class ApplicationUser : IPrincipal
    {
        public ApplicationUser(string name, long userId, string tokenStr, TokenDto token)
        {
            Identity = new UserIdentity(name, userId,tokenStr,token);
        }

        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}