using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class AccountDb
    {
        public string UserId { get; set; }
        public string AccountPassword { get; set; }
        public string AccountFullName { get; set; }
        public int? AccountRole { get; set; }
    }
}
