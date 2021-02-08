using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace VotingSystem.Models
{


    public class User
    {
        public long Id { get; private set; }
        public string Username { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }

        public bool Locked { get; set; } = false;

        public int RoleId { get; private set; }
        public UserRole Role { get; private set; }

        public static User FromCredentials(string username, string password, int roleId)
        {
            if (!password.Any(char.IsDigit) || !password.Any(char.IsUpper)||!password.Any(char.IsPunctuation))
                throw new ArgumentException("Password must contain at least one digit, one upper case letter and one special sign.");
            GetSaltAndHash(password, out var salt, out var saltedHash);
            return new User
            {
                Username = username,
                PasswordSalt = salt,
                PasswordHash = saltedHash,
                RoleId = roleId
            };
        }
        private User()
        {
        }

        public bool Authenticate(string password)
        {
            return Enumerable.SequenceEqual(
                PasswordHash,
                GenerateHash(password, PasswordSalt));
        }

        private static void GetSaltAndHash(string password, out byte[] salt, out byte[] saltedHash)
        {
            salt = GenerateSalt();
            saltedHash = GenerateHash(password, salt);
        }

        private static byte[] GenerateHash(string password, byte[] salt)
        {
            var passwordHasher = new Rfc2898DeriveBytes(
                password,
                salt,
                100000,
                HashAlgorithmName.SHA512);

            return passwordHasher.GetBytes(64);
        }

        private static byte[] GenerateSalt()
        {
            var salt = new byte[16];

            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(salt);

            return salt;
        }

    }
}