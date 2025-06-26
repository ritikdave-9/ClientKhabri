using System;
using Enums;

namespace ClientKhabri
{
    public static class Cache
    {
        public static string? UserID { get; private set; }
        public static string Username { get; private set; }
        public static Role Role { get; private set; }

        public static void SetUser(string userId, string username, Role role)
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
