using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mio.API.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductOptions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePaths = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionLabel1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Options1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionLabel2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Options2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionLabel3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Options3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberProducts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Spaces",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberStories = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaces", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Timeslots",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeslots", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    AccountTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_AccountTypes_AccountTypeID",
                        column: x => x.AccountTypeID,
                        principalTable: "AccountTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Zipcode = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    NumberReviews = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductTypeID = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentAvailable = table.Column<bool>(type: "bit", nullable: true),
                    Units = table.Column<int>(type: "int", nullable: true),
                    ProductOptionID = table.Column<int>(type: "int", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_ProductOptions_ProductOptionID",
                        column: x => x.ProductOptionID,
                        principalTable: "ProductOptions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextContents",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Upvotes = table.Column<int>(type: "int", nullable: false),
                    NumComments = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentContentID = table.Column<int>(type: "int", nullable: true),
                    StoryID = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpaceID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextContents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TextContents_Spaces_SpaceID",
                        column: x => x.SpaceID,
                        principalTable: "Spaces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TextContents_TextContents_StoryID",
                        column: x => x.StoryID,
                        principalTable: "TextContents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TextContents_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTimeslots",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EarliestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Repitition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    TimeslotID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTimeslots", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserTimeslots_Timeslots_TimeslotID",
                        column: x => x.TimeslotID,
                        principalTable: "Timeslots",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTimeslots_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductHistories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHistories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductHistories_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRentals",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRentals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductRentals_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTimeslots",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceID = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeslotID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServerID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTimeslots", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ServiceTimeslots_Products_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceTimeslots_Timeslots_TimeslotID",
                        column: x => x.TimeslotID,
                        principalTable: "Timeslots",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTextReactions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Liked = table.Column<bool>(type: "bit", nullable: false),
                    Commented = table.Column<bool>(type: "bit", nullable: false),
                    TextContentID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTextReactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserTextReactions_TextContents_TextContentID",
                        column: x => x.TextContentID,
                        principalTable: "TextContents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 4, "Server" },
                    { 2, "Seller" },
                    { 1, "General" },
                    { 3, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "ID", "ImagePaths", "OptionLabel1", "OptionLabel2", "OptionLabel3", "Options1", "Options2", "Options3" },
                values: new object[,]
                {
                    { 1, "images/product/201d53a8-1efs-41ef-aac4-aadsff8414cd/cup.jpg", "Color", null, null, "Blue,Black", null, null },
                    { 2, "images/product/201d53a8-1efs-41ef-aac4-aadsff8414cd/redbull.jpg", "Size", null, null, "8.0fl,12.0fl", null, null },
                    { 3, "images/product/201d53a8-1efs-41ef-aac4-aadsff8414cd/book.jpg", "Format", null, null, "E-book,Paperback,Hardcover", null, null }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "ID", "Name", "NumberProducts" },
                values: new object[,]
                {
                    { 3, "Book", 0 },
                    { 4, "Legal Help", 0 },
                    { 2, "Energy Drink", 0 },
                    { 1, "Cups", 0 }
                });

            migrationBuilder.InsertData(
                table: "Spaces",
                columns: new[] { "ID", "Name", "NumberStories" },
                values: new object[,]
                {
                    { 1, "N/A", 1 },
                    { 2, "Formula 1", 1 }
                });

            migrationBuilder.InsertData(
                table: "Timeslots",
                columns: new[] { "ID", "Day", "StartTime" },
                values: new object[,]
                {
                    { 230, "Fri", "18:30" },
                    { 229, "Fri", "18:00" },
                    { 228, "Fri", "17:30" },
                    { 227, "Fri", "17:00" },
                    { 226, "Fri", "16:30" },
                    { 225, "Fri", "16:00" },
                    { 224, "Fri", "15:30" },
                    { 223, "Fri", "15:00" },
                    { 219, "Fri", "13:00" },
                    { 221, "Fri", "14:00" },
                    { 220, "Fri", "13:30" },
                    { 231, "Fri", "19:00" },
                    { 218, "Fri", "12:30" },
                    { 217, "Fri", "12:00" },
                    { 216, "Fri", "11:30" },
                    { 215, "Fri", "11:00" },
                    { 222, "Fri", "14:30" },
                    { 232, "Fri", "19:30" },
                    { 236, "Fri", "21:30" },
                    { 234, "Fri", "20:30" },
                    { 251, "Sat", "5:00" },
                    { 250, "Sat", "4:30" },
                    { 249, "Sat", "4:00" },
                    { 248, "Sat", "3:30" },
                    { 247, "Sat", "3:00" },
                    { 246, "Sat", "2:30" },
                    { 245, "Sat", "2:00" },
                    { 244, "Sat", "1:30" },
                    { 243, "Sat", "1:00" }
                });

            migrationBuilder.InsertData(
                table: "Timeslots",
                columns: new[] { "ID", "Day", "StartTime" },
                values: new object[,]
                {
                    { 242, "Sat", "0:30" },
                    { 241, "Sat", "0:00" },
                    { 240, "Fri", "23:30" },
                    { 239, "Fri", "23:00" },
                    { 238, "Fri", "22:30" },
                    { 237, "Fri", "22:00" },
                    { 214, "Fri", "10:30" },
                    { 235, "Fri", "21:00" },
                    { 233, "Fri", "20:00" },
                    { 213, "Fri", "10:00" },
                    { 209, "Fri", "8:00" },
                    { 211, "Fri", "9:00" },
                    { 189, "Thu", "22:00" },
                    { 188, "Thu", "21:30" },
                    { 187, "Thu", "21:00" },
                    { 186, "Thu", "20:30" },
                    { 185, "Thu", "20:00" },
                    { 184, "Thu", "19:30" },
                    { 183, "Thu", "19:00" },
                    { 182, "Thu", "18:30" },
                    { 181, "Thu", "18:00" },
                    { 180, "Thu", "17:30" },
                    { 179, "Thu", "17:00" },
                    { 178, "Thu", "16:30" },
                    { 177, "Thu", "16:00" },
                    { 176, "Thu", "15:30" },
                    { 175, "Thu", "15:00" },
                    { 174, "Thu", "14:30" },
                    { 173, "Thu", "14:00" },
                    { 190, "Thu", "22:30" },
                    { 212, "Fri", "9:30" },
                    { 191, "Thu", "23:00" },
                    { 193, "Fri", "0:00" },
                    { 210, "Fri", "8:30" },
                    { 252, "Sat", "5:30" },
                    { 208, "Fri", "7:30" },
                    { 207, "Fri", "7:00" },
                    { 206, "Fri", "6:30" },
                    { 205, "Fri", "6:00" },
                    { 204, "Fri", "5:30" },
                    { 203, "Fri", "5:00" },
                    { 202, "Fri", "4:30" }
                });

            migrationBuilder.InsertData(
                table: "Timeslots",
                columns: new[] { "ID", "Day", "StartTime" },
                values: new object[,]
                {
                    { 201, "Fri", "4:00" },
                    { 200, "Fri", "3:30" },
                    { 199, "Fri", "3:00" },
                    { 198, "Fri", "2:30" },
                    { 197, "Fri", "2:00" },
                    { 196, "Fri", "1:30" },
                    { 195, "Fri", "1:00" },
                    { 194, "Fri", "0:30" },
                    { 192, "Thu", "23:30" },
                    { 253, "Sat", "6:00" },
                    { 256, "Sat", "7:30" },
                    { 255, "Sat", "7:00" },
                    { 315, "Sun", "13:00" },
                    { 314, "Sun", "12:30" },
                    { 313, "Sun", "12:00" },
                    { 312, "Sun", "11:30" },
                    { 311, "Sun", "11:00" },
                    { 310, "Sun", "10:30" },
                    { 309, "Sun", "10:00" },
                    { 308, "Sun", "9:30" },
                    { 307, "Sun", "9:00" },
                    { 306, "Sun", "8:30" },
                    { 305, "Sun", "8:00" },
                    { 304, "Sun", "7:30" },
                    { 303, "Sun", "7:00" },
                    { 302, "Sun", "6:30" },
                    { 301, "Sun", "6:00" },
                    { 300, "Sun", "5:30" },
                    { 299, "Sun", "5:00" },
                    { 316, "Sun", "13:30" },
                    { 298, "Sun", "4:30" },
                    { 317, "Sun", "14:00" },
                    { 319, "Sun", "15:00" },
                    { 336, "Sun", "23:30" },
                    { 335, "Sun", "23:00" },
                    { 334, "Sun", "22:30" },
                    { 333, "Sun", "22:00" },
                    { 332, "Sun", "21:30" },
                    { 331, "Sun", "21:00" },
                    { 330, "Sun", "20:30" },
                    { 329, "Sun", "20:00" },
                    { 328, "Sun", "19:30" }
                });

            migrationBuilder.InsertData(
                table: "Timeslots",
                columns: new[] { "ID", "Day", "StartTime" },
                values: new object[,]
                {
                    { 327, "Sun", "19:00" },
                    { 326, "Sun", "18:30" },
                    { 325, "Sun", "18:00" },
                    { 324, "Sun", "17:30" },
                    { 323, "Sun", "17:00" },
                    { 322, "Sun", "16:30" },
                    { 321, "Sun", "16:00" },
                    { 320, "Sun", "15:30" },
                    { 318, "Sun", "14:30" },
                    { 297, "Sun", "4:00" },
                    { 296, "Sun", "3:30" },
                    { 295, "Sun", "3:00" },
                    { 272, "Sat", "15:30" },
                    { 271, "Sat", "15:00" },
                    { 270, "Sat", "14:30" },
                    { 269, "Sat", "14:00" },
                    { 268, "Sat", "13:30" },
                    { 267, "Sat", "13:00" },
                    { 266, "Sat", "12:30" },
                    { 265, "Sat", "12:00" },
                    { 264, "Sat", "11:30" },
                    { 263, "Sat", "11:00" },
                    { 262, "Sat", "10:30" },
                    { 261, "Sat", "10:00" },
                    { 260, "Sat", "9:30" },
                    { 259, "Sat", "9:00" },
                    { 258, "Sat", "8:30" },
                    { 257, "Sat", "8:00" },
                    { 172, "Thu", "13:30" },
                    { 273, "Sat", "16:00" },
                    { 274, "Sat", "16:30" },
                    { 275, "Sat", "17:00" },
                    { 276, "Sat", "17:30" },
                    { 294, "Sun", "2:30" },
                    { 293, "Sun", "2:00" },
                    { 292, "Sun", "1:30" },
                    { 291, "Sun", "1:00" },
                    { 290, "Sun", "0:30" },
                    { 289, "Sun", "0:00" },
                    { 288, "Sat", "23:30" },
                    { 287, "Sat", "23:00" },
                    { 254, "Sat", "6:30" }
                });

            migrationBuilder.InsertData(
                table: "Timeslots",
                columns: new[] { "ID", "Day", "StartTime" },
                values: new object[,]
                {
                    { 286, "Sat", "22:30" },
                    { 284, "Sat", "21:30" },
                    { 283, "Sat", "21:00" },
                    { 282, "Sat", "20:30" },
                    { 281, "Sat", "20:00" },
                    { 280, "Sat", "19:30" },
                    { 279, "Sat", "19:00" },
                    { 278, "Sat", "18:30" },
                    { 277, "Sat", "18:00" },
                    { 285, "Sat", "22:00" },
                    { 171, "Thu", "13:00" },
                    { 168, "Thu", "11:30" },
                    { 169, "Thu", "12:00" },
                    { 60, "Tue", "5:30" },
                    { 59, "Tue", "5:00" },
                    { 58, "Tue", "4:30" },
                    { 57, "Tue", "4:00" },
                    { 56, "Tue", "3:30" },
                    { 55, "Tue", "3:00" },
                    { 54, "Tue", "2:30" },
                    { 53, "Tue", "2:00" },
                    { 52, "Tue", "1:30" },
                    { 51, "Tue", "1:00" },
                    { 50, "Tue", "0:30" },
                    { 49, "Tue", "0:00" },
                    { 48, "Mon", "23:30" },
                    { 47, "Mon", "23:00" },
                    { 46, "Mon", "22:30" },
                    { 45, "Mon", "22:00" },
                    { 44, "Mon", "21:30" },
                    { 61, "Tue", "6:00" },
                    { 43, "Mon", "21:00" },
                    { 62, "Tue", "6:30" },
                    { 64, "Tue", "7:30" },
                    { 81, "Tue", "16:00" },
                    { 80, "Tue", "15:30" },
                    { 79, "Tue", "15:00" },
                    { 78, "Tue", "14:30" },
                    { 77, "Tue", "14:00" },
                    { 76, "Tue", "13:30" },
                    { 75, "Tue", "13:00" },
                    { 74, "Tue", "12:30" }
                });

            migrationBuilder.InsertData(
                table: "Timeslots",
                columns: new[] { "ID", "Day", "StartTime" },
                values: new object[,]
                {
                    { 73, "Tue", "12:00" },
                    { 72, "Tue", "11:30" },
                    { 71, "Tue", "11:00" },
                    { 70, "Tue", "10:30" },
                    { 69, "Tue", "10:00" },
                    { 68, "Tue", "9:30" },
                    { 67, "Tue", "9:00" },
                    { 66, "Tue", "8:30" },
                    { 65, "Tue", "8:00" },
                    { 63, "Tue", "7:00" },
                    { 42, "Mon", "20:30" },
                    { 41, "Mon", "20:00" },
                    { 40, "Mon", "19:30" },
                    { 17, "Mon", "8:00" },
                    { 16, "Mon", "7:30" },
                    { 15, "Mon", "7:00" },
                    { 14, "Mon", "6:30" },
                    { 13, "Mon", "6:00" },
                    { 12, "Mon", "5:30" },
                    { 11, "Mon", "5:00" },
                    { 10, "Mon", "4:30" },
                    { 9, "Mon", "4:00" },
                    { 8, "Mon", "3:30" },
                    { 7, "Mon", "3:00" },
                    { 6, "Mon", "2:30" },
                    { 5, "Mon", "2:00" },
                    { 4, "Mon", "1:30" },
                    { 3, "Mon", "1:00" },
                    { 2, "Mon", "0:30" },
                    { 1, "Mon", "0:00" },
                    { 18, "Mon", "8:30" },
                    { 19, "Mon", "9:00" },
                    { 20, "Mon", "9:30" },
                    { 21, "Mon", "10:00" },
                    { 39, "Mon", "19:00" },
                    { 38, "Mon", "18:30" },
                    { 37, "Mon", "18:00" },
                    { 36, "Mon", "17:30" },
                    { 35, "Mon", "17:00" },
                    { 34, "Mon", "16:30" },
                    { 33, "Mon", "16:00" },
                    { 32, "Mon", "15:30" }
                });

            migrationBuilder.InsertData(
                table: "Timeslots",
                columns: new[] { "ID", "Day", "StartTime" },
                values: new object[,]
                {
                    { 82, "Tue", "16:30" },
                    { 31, "Mon", "15:00" },
                    { 29, "Mon", "14:00" },
                    { 28, "Mon", "13:30" },
                    { 27, "Mon", "13:00" },
                    { 26, "Mon", "12:30" },
                    { 25, "Mon", "12:00" },
                    { 24, "Mon", "11:30" },
                    { 23, "Mon", "11:00" },
                    { 22, "Mon", "10:30" },
                    { 30, "Mon", "14:30" },
                    { 170, "Thu", "12:30" },
                    { 83, "Tue", "17:00" },
                    { 85, "Tue", "18:00" },
                    { 145, "Thu", "0:00" },
                    { 144, "Wed", "23:30" },
                    { 143, "Wed", "23:00" },
                    { 142, "Wed", "22:30" },
                    { 141, "Wed", "22:00" },
                    { 140, "Wed", "21:30" },
                    { 139, "Wed", "21:00" },
                    { 138, "Wed", "20:30" },
                    { 137, "Wed", "20:00" },
                    { 136, "Wed", "19:30" },
                    { 135, "Wed", "19:00" },
                    { 134, "Wed", "18:30" },
                    { 133, "Wed", "18:00" },
                    { 132, "Wed", "17:30" },
                    { 131, "Wed", "17:00" },
                    { 130, "Wed", "16:30" },
                    { 129, "Wed", "16:00" },
                    { 146, "Thu", "0:30" },
                    { 128, "Wed", "15:30" },
                    { 147, "Thu", "1:00" },
                    { 149, "Thu", "2:00" },
                    { 167, "Thu", "11:00" },
                    { 165, "Thu", "10:00" },
                    { 164, "Thu", "9:30" },
                    { 163, "Thu", "9:00" },
                    { 162, "Thu", "8:30" },
                    { 161, "Thu", "8:00" },
                    { 160, "Thu", "7:30" }
                });

            migrationBuilder.InsertData(
                table: "Timeslots",
                columns: new[] { "ID", "Day", "StartTime" },
                values: new object[,]
                {
                    { 159, "Thu", "7:00" },
                    { 158, "Thu", "6:30" },
                    { 157, "Thu", "6:00" },
                    { 156, "Thu", "5:30" },
                    { 155, "Thu", "5:00" },
                    { 154, "Thu", "4:30" },
                    { 153, "Thu", "4:00" },
                    { 152, "Thu", "3:30" },
                    { 151, "Thu", "3:00" },
                    { 150, "Thu", "2:30" },
                    { 148, "Thu", "1:30" },
                    { 127, "Wed", "15:00" },
                    { 126, "Wed", "14:30" },
                    { 125, "Wed", "14:00" },
                    { 102, "Wed", "2:30" },
                    { 101, "Wed", "2:00" },
                    { 100, "Wed", "1:30" },
                    { 99, "Wed", "1:00" },
                    { 98, "Wed", "0:30" },
                    { 97, "Wed", "0:00" },
                    { 96, "Tue", "23:30" },
                    { 95, "Tue", "23:00" },
                    { 94, "Tue", "22:30" },
                    { 93, "Tue", "22:00" },
                    { 92, "Tue", "21:30" },
                    { 91, "Tue", "21:00" },
                    { 90, "Tue", "20:30" },
                    { 89, "Tue", "20:00" },
                    { 88, "Tue", "19:30" },
                    { 87, "Tue", "19:00" },
                    { 86, "Tue", "18:30" },
                    { 103, "Wed", "3:00" },
                    { 104, "Wed", "3:30" },
                    { 105, "Wed", "4:00" },
                    { 106, "Wed", "4:30" },
                    { 124, "Wed", "13:30" },
                    { 123, "Wed", "13:00" },
                    { 122, "Wed", "12:30" },
                    { 121, "Wed", "12:00" },
                    { 120, "Wed", "11:30" },
                    { 119, "Wed", "11:00" },
                    { 118, "Wed", "10:30" }
                });

            migrationBuilder.InsertData(
                table: "Timeslots",
                columns: new[] { "ID", "Day", "StartTime" },
                values: new object[,]
                {
                    { 117, "Wed", "10:00" },
                    { 84, "Tue", "17:30" },
                    { 116, "Wed", "9:30" },
                    { 114, "Wed", "8:30" },
                    { 113, "Wed", "8:00" },
                    { 112, "Wed", "7:30" },
                    { 111, "Wed", "7:00" },
                    { 110, "Wed", "6:30" },
                    { 109, "Wed", "6:00" },
                    { 108, "Wed", "5:30" },
                    { 107, "Wed", "5:00" },
                    { 115, "Wed", "9:00" },
                    { 166, "Thu", "10:30" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "AccountTypeID", "BloodType", "DOB", "FirstName", "ImagePath", "LastName", "Rating", "UserName" },
                values: new object[,]
                {
                    { "201d53a8-1fe6-41ef-aac4-aadsff8414cd", 1, "O+", new DateTime(2001, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mikasa", "images/mikasa.jpg", "Ackerman", 0f, "mk44" },
                    { "201d53a8-1efs-41ef-aac4-aadsff8414cd", 2, "B+", new DateTime(1987, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Miles", "images/miles.jpg", "Morales", 0f, "miles4" },
                    { "201d53a8-1fe6-41ef-aac4-aad4af8414cd", 3, "A+", new DateTime(1998, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yohan", "images/yohan.jpg", "Ninan", 0f, "yn05" },
                    { "6f942aca-57a7-4adb-a6b5-5d9796cdf10b", 4, "B+", new DateTime(1976, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sherlock", "images/sherlock.jpg", "Holmes", 0f, "sherlocked" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "ID", "AddressLine1", "AddressLine2", "City", "Country", "Default", "State", "UserID", "Zipcode" },
                values: new object[,]
                {
                    { 2, "400 South Orange Ave", "", "South Orange", "USA", false, "New Jersey", "201d53a8-1fe6-41ef-aac4-aad4af8414cd", 7079 },
                    { 1, "22B Baker Street", "", "London", "England", false, "London", "6f942aca-57a7-4adb-a6b5-5d9796cdf10b", 5201 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "Discriminator", "Name", "NumberReviews", "Price", "ProductOptionID", "ProductTypeID", "Rating", "RentAvailable", "Units", "UserID" },
                values: new object[,]
                {
                    { 1, "Cup", "Commodity", "Cup", 0, 4.0, 1, 1, 2.0, false, 4, "201d53a8-1efs-41ef-aac4-aadsff8414cd" },
                    { 2, "Energy Drink", "Commodity", "Redbull", 1, 2.5, 2, 2, 4.0, false, 10, "201d53a8-1efs-41ef-aac4-aadsff8414cd" },
                    { 4, "New book!", "Commodity", "Book", 0, 9.0, 3, 3, 4.0, true, 20, "201d53a8-1efs-41ef-aac4-aadsff8414cd" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "Discriminator", "ImagePath", "Name", "NumberReviews", "Price", "ProductTypeID", "Rating", "UserID" },
                values: new object[] { 3, "Solves crimes", "Service", "images/sherlock.jpg", "Consulting Detective", 1, 20.0, 4, 5.0, "6f942aca-57a7-4adb-a6b5-5d9796cdf10b" });

            migrationBuilder.InsertData(
                table: "TextContents",
                columns: new[] { "ID", "Content", "DateTime", "Discriminator", "NumComments", "ParentContentID", "StoryID", "Upvotes", "UserID" },
                values: new object[,]
                {
                    { 4, "Oh please O_O", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comment", 0, 3, null, 2, "201d53a8-1fe6-41ef-aac4-aad4af8414cd" },
                    { 5, "- _ -", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comment", 0, 2, null, 1, "201d53a8-1fe6-41ef-aac4-aad4af8414cd" }
                });

            migrationBuilder.InsertData(
                table: "TextContents",
                columns: new[] { "ID", "Content", "DateTime", "Discriminator", "NumComments", "ProductID", "Rating", "Upvotes", "UserID" },
                values: new object[,]
                {
                    { 6, "Liked the taste", new DateTime(2020, 12, 17, 22, 8, 25, 854, DateTimeKind.Local).AddTicks(2822), "Review", 0, 2, 4.0, 0, "201d53a8-1fe6-41ef-aac4-aad4af8414cd" },
                    { 7, "Solved an impossible crime!", new DateTime(2020, 12, 17, 22, 8, 25, 860, DateTimeKind.Local).AddTicks(2146), "Review", 0, 3, 5.0, 0, "201d53a8-1fe6-41ef-aac4-aad4af8414cd" }
                });

            migrationBuilder.InsertData(
                table: "TextContents",
                columns: new[] { "ID", "Content", "DateTime", "Discriminator", "NumComments", "SpaceID", "Title", "Upvotes", "UserID" },
                values: new object[] { 1, "Test Content", new DateTime(2020, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Story", 1, 1, "Test 1", 1, "201d53a8-1fe6-41ef-aac4-aad4af8414cd" });

            migrationBuilder.InsertData(
                table: "TextContents",
                columns: new[] { "ID", "Content", "DateTime", "Discriminator", "NumComments", "ParentContentID", "StoryID", "Upvotes", "UserID" },
                values: new object[] { 3, "what's a test", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comment", 1, 1, null, 0, "6f942aca-57a7-4adb-a6b5-5d9796cdf10b" });

            migrationBuilder.InsertData(
                table: "TextContents",
                columns: new[] { "ID", "Content", "DateTime", "Discriminator", "NumComments", "SpaceID", "Title", "Upvotes", "UserID" },
                values: new object[] { 2, "F1 race this week! Grazzie Ragazzi", new DateTime(2020, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Story", 1, 2, "Test 2", 2, "6f942aca-57a7-4adb-a6b5-5d9796cdf10b" });

            migrationBuilder.InsertData(
                table: "UserTimeslots",
                columns: new[] { "ID", "Available", "EarliestDate", "EndDate", "Repitition", "TimeslotID", "UserID" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2020, 12, 17, 22, 8, 25, 861, DateTimeKind.Local).AddTicks(7921), new DateTime(2020, 12, 22, 22, 8, 25, 862, DateTimeKind.Local).AddTicks(3320), "weekly", 10, "6f942aca-57a7-4adb-a6b5-5d9796cdf10b" },
                    { 2, true, new DateTime(2020, 12, 17, 22, 8, 25, 862, DateTimeKind.Local).AddTicks(5221), new DateTime(2021, 1, 17, 22, 8, 25, 862, DateTimeKind.Local).AddTicks(5340), "monthly", 34, "6f942aca-57a7-4adb-a6b5-5d9796cdf10b" }
                });

            migrationBuilder.InsertData(
                table: "ProductHistories",
                columns: new[] { "ID", "Date", "ProductID", "Type", "UserID" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "sale", "201d53a8-1fe6-41ef-aac4-aad4af8414cd" },
                    { 2, new DateTime(2021, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "service", "201d53a8-1fe6-41ef-aac4-aad4af8414cd" }
                });

            migrationBuilder.InsertData(
                table: "ProductRentals",
                columns: new[] { "ID", "ExpirationDate", "ProductID", "UserID" },
                values: new object[] { 1, new DateTime(2021, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "201d53a8-1fe6-41ef-aac4-aad4af8414cd" });

            migrationBuilder.InsertData(
                table: "ServiceTimeslots",
                columns: new[] { "ID", "CustomerID", "Date", "Duration", "ServerID", "ServiceID", "TimeslotID" },
                values: new object[] { 1, "201d53a8-1fe6-41ef-aac4-aad4af8414cd", new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.5, "6f942aca-57a7-4adb-a6b5-5d9796cdf10b", 3, 260 });

            migrationBuilder.InsertData(
                table: "UserTextReactions",
                columns: new[] { "ID", "Commented", "Liked", "TextContentID", "UserID" },
                values: new object[,]
                {
                    { 1, false, true, 1, "201d53a8-1fe6-41ef-aac4-aadsff8414cd" },
                    { 2, true, false, 1, "6f942aca-57a7-4adb-a6b5-5d9796cdf10b" },
                    { 3, true, true, 3, "201d53a8-1fe6-41ef-aac4-aad4af8414cd" },
                    { 4, true, false, 2, "201d53a8-1fe6-41ef-aac4-aad4af8414cd" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserID",
                table: "Addresses",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistories_ProductID",
                table: "ProductHistories",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRentals_ProductID",
                table: "ProductRentals",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductOptionID",
                table: "Products",
                column: "ProductOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeID",
                table: "Products",
                column: "ProductTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserID",
                table: "Products",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTimeslots_ServiceID",
                table: "ServiceTimeslots",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTimeslots_TimeslotID",
                table: "ServiceTimeslots",
                column: "TimeslotID");

            migrationBuilder.CreateIndex(
                name: "IX_TextContents_SpaceID",
                table: "TextContents",
                column: "SpaceID");

            migrationBuilder.CreateIndex(
                name: "IX_TextContents_StoryID",
                table: "TextContents",
                column: "StoryID");

            migrationBuilder.CreateIndex(
                name: "IX_TextContents_UserID",
                table: "TextContents",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountTypeID",
                table: "Users",
                column: "AccountTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTextReactions_TextContentID",
                table: "UserTextReactions",
                column: "TextContentID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTimeslots_TimeslotID",
                table: "UserTimeslots",
                column: "TimeslotID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTimeslots_UserID",
                table: "UserTimeslots",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "ProductHistories");

            migrationBuilder.DropTable(
                name: "ProductRentals");

            migrationBuilder.DropTable(
                name: "ServiceTimeslots");

            migrationBuilder.DropTable(
                name: "UserTextReactions");

            migrationBuilder.DropTable(
                name: "UserTimeslots");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "TextContents");

            migrationBuilder.DropTable(
                name: "Timeslots");

            migrationBuilder.DropTable(
                name: "ProductOptions");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "Spaces");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AccountTypes");
        }
    }
}
