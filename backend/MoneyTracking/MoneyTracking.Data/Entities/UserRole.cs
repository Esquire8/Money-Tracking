using MoneyTracking.Data.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracking.Data.Entities
{
    public class UserRole : IEntity
    {
        public int Id { get; set; }

        public User User { get; set; } = null!;

        public string RolesJson { get; set; } = null!;

        [NotMapped]
        public List<Role> Roles
        {
            get => string.IsNullOrEmpty(RolesJson) ? new List<Role>() : JsonConvert.DeserializeObject<List<Role>>(RolesJson);
            set => RolesJson = JsonConvert.SerializeObject(value);
        }
    }
}