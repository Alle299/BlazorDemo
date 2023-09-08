using FluentMigrator.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BlazorDemo_WebAPI.Entities
{
    public class _UserRole
    {
        public Guid _UserID { get; set; }
        public Guid _RoleID { get; set; }
        // Metadata fields
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? Removed { get; set; }
        public Guid? RemovedBy { get; set; }
    }
}
