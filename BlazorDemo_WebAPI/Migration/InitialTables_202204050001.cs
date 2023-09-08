using FluentMigrator;

namespace BlazorDemo_WebAPI.Migration
{
    [Migration(202207050001)]
    public class InitialTables_20220705001 : FluentMigrator.Migration
    {
        /// <summary>
        /// Downgrade database
        /// </summary>
        public override void Down()
        {
            Delete.Table("_Build");
            Delete.Table("_UserRole");
            Delete.Table("_User");
            Delete.Table("_Role");
        }

        /// <summary>
        /// Upgrade database
        /// </summary>
        public override void Up()
        {
            Create.Table("_Build")
                .WithColumn("_BuildID").AsGuid().PrimaryKey().Indexed()
                .WithColumn("ApplicationID").AsGuid().Nullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsGuid().NotNullable()
                .WithColumn("CompletedDate").AsDateTime().NotNullable()
                .WithColumn("BuildVersion").AsString(50).NotNullable()
                .WithColumn("Stages").AsString(50).NotNullable()
                .WithColumn("OrderState").AsString(50).NotNullable();

            Create.Table("_Role")
                .WithColumn("_RoleID").AsGuid().PrimaryKey().Indexed() 
                .WithColumn("Name").AsString(50).NotNullable()
                // MetadataFields
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsGuid().NotNullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsGuid().Nullable()
                .WithColumn("Removed").AsDateTime().Nullable()
                .WithColumn("RemovedBy").AsGuid().Nullable();

            Create.Table("_User")
                .WithColumn("_UserID").AsGuid().PrimaryKey().Indexed()
                .WithColumn("UserName").AsString(50).NotNullable()
                .WithColumn("PasswordHash").AsString(50).Nullable()
                .WithColumn("FailedLogins").AsInt16().WithDefaultValue(0)
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(50).NotNullable()
                .WithColumn("LastLogin").AsDateTime().Nullable()
                .WithColumn("Email").AsString(50).Nullable()
                // MetadataFields
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsGuid().NotNullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsGuid().Nullable()
                .WithColumn("Removed").AsDateTime().Nullable()
                .WithColumn("RemovedBy").AsGuid().Nullable();

            Create.Table("_UserRole")
                .WithColumn("_UserID").AsGuid().PrimaryKey()
                .WithColumn("_RoleID").AsGuid().PrimaryKey()
                // MetadataFields
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsGuid().NotNullable()
                .WithColumn("Modified").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsGuid().Nullable()
                .WithColumn("Removed").AsDateTime().Nullable()
                .WithColumn("RemovedBy").AsGuid().Nullable();
        }
    }
}
