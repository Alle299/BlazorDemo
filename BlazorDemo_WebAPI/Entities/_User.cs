using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorDemo_WebAPI.Entities
{
    [Newtonsoft.Json.JsonObject(Title = " _User")]
    public class _User
    {
        public Guid _UserID { get; set; }
        public string Email { get; set; } = null!;
        [JsonIgnore]
        public string PasswordHash { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? LastLogin { get; set; } = null!;
        public int FailedLogins { get; set; } = 0;
        // Metadata fields
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? Modified { get; set; } = null!;
        public Guid? ModifiedBy { get; set; } = null!;
        public DateTime? Removed { get; set; } = null!;
        public Guid? RemovedBy { get; set; } = null!;
    }
}

