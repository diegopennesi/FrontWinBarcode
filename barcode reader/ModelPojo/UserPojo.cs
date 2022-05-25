using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace barcode_reader
{
    public class UserPojo
    {
        public UserPojo()
        {
        }

        public UserPojo(string username, string password)
        {
            this.username = username;
           this. Password = password;
        }
        public UserPojo(string username, string password,Boolean isActive)
        {
            this.username = username;
            this.Password = password;
            this.isactive = isActive;
        }
        public UserPojo(string username, string password, Boolean isActive, int accesslevel)
        {
            this.username = username;
            this.Password = password;
            this.isactive = isActive;
            this.accesslevel=accesslevel;
        }


        public String Id { get; set; } = generateSecurePublicId("DLtoolsAlphaTestClosed","0");
        public string username { get; set; } = "";
        public string Password { get; set; } = "";
        public long lastlogin { get; set; }
        public long loginAttempt { get; set; }
        public Boolean isbanned { get; set; } = false;
        public Boolean isactive { get; set; } = false;
        public int accesslevel { get; set; }
        // TBD
        // 0 SUPER ADMIN
        // 1 SUPER MODERATOR
        // 2 LOCAL ADMIN
        // 3 LOCAL MODERATOR
        // 4 GUEST
        public override string ToString()
        {
            return Utility.ToString(this);
        }
        private static String generateSecurePublicId(String salt2,String username)
        {
            Byte[] salt = new byte[salt2.Length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            //  Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
              password: username,
              salt: salt,
              prf: KeyDerivationPrf.HMACSHA1,
              iterationCount: 10000,
              numBytesRequested: 32 / 8));
            return hashed;
        }
    }



    
}
