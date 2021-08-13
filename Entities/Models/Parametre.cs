using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Parametre
    {
        public Guid Id { get; set; }
        public string NumPort { get; set; }
        public string Token { get; set; }
        public bool Mecef { get; set; }
        public bool EMecef { get; set; }
    }
}
