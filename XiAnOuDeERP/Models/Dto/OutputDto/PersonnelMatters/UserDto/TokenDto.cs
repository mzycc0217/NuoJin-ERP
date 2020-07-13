using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XiAnOuDeERP.Models.Enum;

namespace XiAnOuDeERP.Models.Dto.OutputDto.PersonnelMatters.UserDto
{
    public class TokenDto
    {
        public long UserId { get; set; }

        public string UserName { get; set; }

        public List<long> UserTypeId { get; set; }

        public List<string> UserTypeKey { get; set; }
    }
}