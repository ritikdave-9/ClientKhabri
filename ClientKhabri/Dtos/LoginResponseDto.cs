﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace ClientKhabri.Dtos
{
    public class LoginResponseDto
    {
        public string FirstName { get; set; }

        public string UserID { get; set; }
        public Role Role { get; set; }
    }
}
