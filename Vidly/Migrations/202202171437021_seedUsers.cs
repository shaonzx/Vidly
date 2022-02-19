namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'694d47c4-b80b-4e6f-a068-4deaf7ff01c1', N'guest@vidly.com', 0, N'ADeWmf+f1bzY/VFKgKuE5azgaoiM0JdXNf+jz4JY4OnAhWz/76wJ7AOSMqCmKHC02w==', N'dc1d5b39-eb73-4ee0-96c6-52735890e842', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                  INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e97bc91a-f6a1-46f2-9e66-3b54d5182cb2', N'admin@vidly.com', 0, N'AOfPZo+d4sXTSNi7E87wnGHzoJy3n4idsznxs4u1WDYE1ADa7IBUxOXIzwhJufFK1A==', N'26c70ec1-e1d5-43e6-8236-5a27ba781c6e', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'65a51503-a2b1-417d-9467-6b8ddc0707c5', N'CanManageMovies')
                  INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e97bc91a-f6a1-46f2-9e66-3b54d5182cb2', N'65a51503-a2b1-417d-9467-6b8ddc0707c5')");
        }
        
        public override void Down()
        {
        }
    }
}
