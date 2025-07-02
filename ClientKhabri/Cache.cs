using System;
using Enums;

namespace ClientKhabri
{
    public static class Cache
    {
        public static int? UserID { get; private set; } = 2;
        public static string Username { get; private set; }
        public static Role Role { get; private set; }

        public static void SetUser(int userId, string username, Role role)
        {
            UserID = userId;
            Username = username;
            Role = role;
        }

        public static void Clear()
        {
            UserID = null;
            Username = null;
            Role=Role.User;
        }

            }
}
