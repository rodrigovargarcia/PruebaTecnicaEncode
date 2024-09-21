using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace PruebaTecnicaEncode.Entities
{
    public class Usuario
    {
        [JsonIgnore]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }


        public string PasswordHash { get; set; }

        public string Sal { get; set; }

        [Required]
        [NotMapped]
        public string PasswordTextoPlano { get; set; }

        public void SetPassword(string passwordTextoPlano)
        {
            var sal = new byte[16];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(sal);
            }

            PasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwordTextoPlano,
                salt: sal,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 32));

            Sal = Convert.ToBase64String(sal);
        }

        public class IgnorePropertiesSchemaFilter : ISchemaFilter
        {
            public void Apply(OpenApiSchema schema, SchemaFilterContext context)
            {
                if (schema?.Properties == null)
                    return;
                
                var propertiesToRemove = new[] { "passwordHash", "sal" };
                foreach (var prop in propertiesToRemove)
                {
                    if (schema.Properties.ContainsKey(prop))
                        schema.Properties.Remove(prop);
                }
            }
        }
    }
}
