using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Pelicula.Migrations
{
    public partial class AdminData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region script
            migrationBuilder.Sql(@"INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
                                    VALUES (N'20201025021630_addUsuario', N'3.1.9');

                                    GO

                                    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
                                        SET IDENTITY_INSERT [AspNetRoles] ON;
                                    INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
                                    VALUES (N'9aae0b6d-d50c-4d0a-9b90-2a6873e3845d', N'd1603459-73ea-4beb-b366-5963f5aedcd5', N'Admin', N'Admin');
                                    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
                                        SET IDENTITY_INSERT [AspNetRoles] OFF;

                                    GO

                                    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
                                        SET IDENTITY_INSERT [AspNetUsers] ON;
                                    INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
                                    VALUES (N'5673b8cf-12de-44f6-92ad-fae4a77932ad', 0, N'c3275906-6a1f-4bd3-9dbb-1287d0e6b7a7', N'felipe@hotmail.com', CAST(0 AS bit), CAST(0 AS bit), NULL, N'felipe@hotmail.com', N'felipe@hotmail.com', N'AQAAAAEAACcQAAAAEJ4Cq/PU+JBTZoz6ZTbjHj6yxWsvuWlDO1ePRqPztQ0t20rY4uqbETmW+cD6VASzCQ==', NULL, CAST(0 AS bit), N'2f91523d-b9bf-4b22-bded-853d234cd941', CAST(0 AS bit), N'felipe@hotmail.com');
                                    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
                                        SET IDENTITY_INSERT [AspNetUsers] OFF;

                                    GO

                                    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClaimType', N'ClaimValue', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserClaims]'))
                                        SET IDENTITY_INSERT [AspNetUserClaims] ON;
                                    INSERT INTO [AspNetUserClaims] ([Id], [ClaimType], [ClaimValue], [UserId])
                                    VALUES (1, N'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', N'Admin', N'5673b8cf-12de-44f6-92ad-fae4a77932ad');
                                    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClaimType', N'ClaimValue', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserClaims]'))
                                        SET IDENTITY_INSERT [AspNetUserClaims] OFF;

                                    GO
                                    ");
            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9aae0b6d-d50c-4d0a-9b90-2a6873e3845d");

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5673b8cf-12de-44f6-92ad-fae4a77932ad");
        }
    }
}
