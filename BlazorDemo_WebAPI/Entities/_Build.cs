namespace BlazorDemo_WebAPI.Entities
{
    public class _Build
    {
        public Guid ApplicationID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string BuildVersion { get; set; }
        public string Stages { get; set; }
        public string OrderState { get; set; }

    }
}
