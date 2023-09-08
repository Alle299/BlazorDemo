namespace BlazorDemo_WebAPI.Entities
{
    public class Full_User : _User
    {
        // Objects for foreign key relationships
        public virtual List<_Role>? Roles { get; set; }
    }
}
