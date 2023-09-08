namespace BlazorDemo_BlazorUI.Models
{
    [Newtonsoft.Json.JsonObject(Title = "_Role")]
    public class _Role
    {
        public Guid _RoleID { get; set; }
        public string Name { get; set; }
        // Metadata fields
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? Modified { get; set; } = null!;
        public Guid? ModifiedBy { get; set; } = null!;
        public DateTime? Removed { get; set; } = null!;
        public Guid? RemovedBy { get; set; } = null!;
    }
}
