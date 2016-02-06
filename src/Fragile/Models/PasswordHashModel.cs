using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Microsoft.AspNet.Cryptography.KeyDerivation;

namespace Fragile.Models
{
    public class PasswordHashModel
    {
        public static RandomNumberGenerator Rng { get; private set; } = RandomNumberGenerator.Create();

        private const int SALT_BYTE_LENGTH = 128;
        private const int HASH_BYTE_LENGTH = 256;
        private const int PBKDF2_ITERATIONS = 10240;

        public int Iterations { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }
        public int Length { get; set; }

        /// <summary>
        /// Compares a raw password to the stored hash.
        /// </summary>
        /// <param name="password">A raw password to compare to the stored hash.</param>
        /// <returns>Returns true if the raw password matches the stored hash else returns false.</returns>
        public bool CompareTo(string password)
        {
            return SlowEquals(this.Hash, Generate(password, salt: this.Salt, hashLength: this.Hash.Length, iterations: this.Iterations).Hash);
        }

        /// <summary>
        /// Compares a password hash to the stored hash.
        /// </summary>
        /// <param name="passwordHash">A password hash to compare to the storec hash.</param>
        /// <returns>Returns true if the hash matches the stored hash else returns false.</returns>
        public bool CompareTo(byte[] passwordHash)
        {
            return SlowEquals(this.Hash, passwordHash);
        }

        /// <summary>
        /// Generates a new PasswordHashModel object
        /// </summary>
        /// <param name="password">The plain text password to be hashed</param>
        /// <param name="salt">An byte array containing the random salt. Can be left blank to generate a new random salt.</param>
        /// <param name="saltLength">The length of the random salt that should be generated Can be left blank to use the default value of 128.</param>
        /// <param name="hashLength">The number of bytes to derive from the password. Can be left blank to use the default value of 256.</param>
        /// <param name="iterations">The number of iterations while hashing the password. Can be left blank to use the default size of 1024.</param>
        /// <returns>Returns a generated PasswordHashModel object.</returns>
        public static PasswordHashModel Generate(string password, byte[] salt = null, int saltLength = SALT_BYTE_LENGTH, int hashLength = HASH_BYTE_LENGTH, int iterations = PBKDF2_ITERATIONS)
        {
            if (salt == null)
            {
                salt = new byte[saltLength];
                Rng.GetBytes(salt);
            }

            byte[] hash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, iterations, hashLength);

            return new PasswordHashModel()
            {
                Iterations = iterations,
                Salt = salt,
                Hash = hash,
                Length = password.Length
            };
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }
    }
}
