using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodLawSoftware.Infrastructure.Migrations
{
    public partial class seedAdminAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[AspNetUsers]
                                       ([Id]
                                       ,[FirstName]
                                       ,[LastName]
                                       ,[Password]
                                       ,[UserName]
                                       ,[NormalizedUserName]
		                               ,Email
		                               ,EmailConfirmed
		                               ,PhoneNumber
		                               ,PhoneNumberConfirmed
		                               ,TwoFactorEnabled
		                               ,LockoutEnabled
		                               ,AccessFailedCount
                                       )
                                 VALUES
                                       ('840D45D6-E511-450D-8911-4CF10A0305A9'
                                       ,'admin'
                                       ,'admin'
                                       ,'@dmin123'
                                       ,'admin'
		                               ,'admin@admin.com'
                                       ,'ADMIN'
		                               ,1
		                               ,123
		                               ,1
		                               ,0
		                               ,0
		                               ,0
                                       )
                            GO");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"delete   from [dbo].[AspNetUsers] where Id='840D45D6-E511-450D-8911-4CF10A0305A9'");
        }
    }
}
