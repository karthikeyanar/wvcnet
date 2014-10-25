using System;
using System.Collections.Generic;

namespace WVC.Models
{
    public partial class aspnetuserlogin
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserId { get; set; }
        public virtual aspnetuser aspnetuser { get; set; }
    }
}
