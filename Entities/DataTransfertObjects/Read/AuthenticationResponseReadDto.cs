using Entities.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransfertObjects
{
    public class AuthenticationResponseReadDto
    {
        public Dictionary<string, string> UserInfo { get; set; }
        //[JsonIgnore]
        //public virtual AppUserReadDto User { get; set; }
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> ErrorDetails { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
