using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Enums;

namespace ClientKhabri.Dtos
{
    public class LoginResponseDto
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }

        [JsonPropertyName("role")]
        public string RoleString { get; set; }

        [JsonIgnore]
        public Role Role => Enum.TryParse<Role>(RoleString, true, out var role) ? role : Role.User;
    }
}
