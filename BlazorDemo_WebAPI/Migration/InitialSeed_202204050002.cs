using FluentMigrator;
using System.Text;
using BlazorDemo_WebAPI.Entities;

namespace BlazorDemo_WebAPI.Migration
{
    [Migration(202207050002)]
    public class InitialSeed_202207050002 : FluentMigrator.Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            // _Role .WithIdentityInsert()
            Insert.IntoTable("_Role")
                .Row(new _Role
				{
					_RoleID = new Guid("E57C02CE-3665-4762-BAE7-95537ADE44B8"),
					Name = "User",
                    // Metadata
                    CreatedBy = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
                    Created = DateTime.UtcNow,
                });

			Insert.IntoTable("_Role")
                .Row(new _Role
				{
					_RoleID = new Guid("CBAAE59C-127E-475B-9D17-05AAE1520607"),
                    Name = "Admin",
                    // Metadata
                    CreatedBy = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
                    Created = DateTime.UtcNow,
                });

			Insert.IntoTable("_Role")
                .Row(new _Role
				{
					_RoleID = new Guid("01B07380-AF9A-47DA-B5D8-DA02BAC157BB"),
					Name = "SuperAdmin",
                    // Metadata
                    CreatedBy = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
                    Created = DateTime.UtcNow,
                });

			// _User
			Insert.IntoTable("_User")
                .Row(new _User
				{
					_UserID = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
					PasswordHash = Convert.ToBase64String(Encoding.ASCII.GetBytes("299792")),
                    FirstName = "Alex",
					LastName = "Wahl",
					UserName = "AleWah",
					FailedLogins = 0,
					Email ="a.b@c.se",
                    // Metadata
                    CreatedBy = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
                    Created = DateTime.UtcNow,
                });
            Insert.IntoTable("_User")
                .Row(new _User
                {
                    _UserID = new Guid("063BEED4-8848-4441-8AE9-2CAA5FE1CF42"),
                    PasswordHash = Convert.ToBase64String(Encoding.ASCII.GetBytes("299792")),
                    FirstName = "Alex",
                    LastName = "Wahl",
                    UserName = "AleWah2",
                    FailedLogins = 0,
                    Email = "b.b@c.se",
                    // Metadata
                    CreatedBy = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
                    Created = DateTime.UtcNow,
                });
            Insert.IntoTable("_User")
                .Row(new _User
                {
                    _UserID = new Guid("E426D014-282F-4C5F-AAD6-E60177EED783"),
                    PasswordHash = Convert.ToBase64String(Encoding.ASCII.GetBytes("299792")),
                    FirstName = "Alex",
                    LastName = "Wahl",
                    UserName = "AleWah3",
                    FailedLogins = 0,
                    Email = "c.b@c.se",
                    // Metadata
                    CreatedBy = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
                    Created = DateTime.UtcNow,
                });

            // _UserRole
            Insert.IntoTable("_UserRole")
                    .Row(new _UserRole
                    {
                        _UserID = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
                        _RoleID = new Guid("E57C02CE-3665-4762-BAE7-95537ADE44B8"),
                        // Metadata
                        CreatedBy = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
                        Created = DateTime.UtcNow,
                    });
            Insert.IntoTable("_UserRole")
                    .Row(new _UserRole
					{
						_UserID = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
                        _RoleID = new Guid("CBAAE59C-127E-475B-9D17-05AAE1520607"),
                        // Metadata
                        CreatedBy = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
                        Created = DateTime.UtcNow,
                    });
            Insert.IntoTable("_UserRole")
                    .Row(new _UserRole
                    {
                        _UserID = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
                        _RoleID = new Guid("01B07380-AF9A-47DA-B5D8-DA02BAC157BB"),
                        // Metadata
                        CreatedBy = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
                        Created = DateTime.UtcNow,
                    });
            Insert.IntoTable("_UserRole")
                    .Row(new _UserRole
                    {
                        _UserID = new Guid("063BEED4-8848-4441-8AE9-2CAA5FE1CF42"),
                        _RoleID = new Guid("CBAAE59C-127E-475B-9D17-05AAE1520607"),
                        // Metadata
                        CreatedBy = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
                        Created = DateTime.UtcNow,
                    });
            Insert.IntoTable("_UserRole")
                    .Row(new _UserRole
                    {
                        _UserID = new Guid("E426D014-282F-4C5F-AAD6-E60177EED783"),
                        _RoleID = new Guid("E57C02CE-3665-4762-BAE7-95537ADE44B8"),
                        // Metadata
                        CreatedBy = new Guid("EFE31600-FAB2-4212-A6DE-8220F44B8064"),
                        Created = DateTime.UtcNow,
                    });
        }
    }
}
