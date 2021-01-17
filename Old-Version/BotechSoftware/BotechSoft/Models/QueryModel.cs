using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotechSoft.Models
{
    public class QueryModel
    {
        public string UserName { get; set; }
        public string UserCompany { get; set; }
        public string UserDesignation { get; set; }
        public string IsdCode { get; set; }
        public string UserMobile { get; set; }
        public string UserLocation { get; set; }
        public string UserEmail { get; set; }
        public bool IsTCAccepted { get; set; }
        public string QueryType { get; set; }
        public string UserExperience { get; set; }
        public string UserSkill { get; set; }
        public string Query { get; set; }
        public IFormFileCollection file { get; set; }
    }
}
