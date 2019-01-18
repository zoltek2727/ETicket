using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ETicket.Data.Migrations
{
    public partial class ModelAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAddress",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UserBirthdate",
                table: "AspNetUsers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCity",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UserCreated",
                table: "AspNetUsers",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserFirstname",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UserLastLog",
                table: "AspNetUsers",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserPhone",
                table: "AspNetUsers",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserSex",
                table: "AspNetUsers",
                unicode: false,
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserSurname",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeliveryType = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(9, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.DeliveryId);
                });

            migrationBuilder.CreateTable(
                name: "PerformerCategories",
                columns: table => new
                {
                    PerformerCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PerformerCategoryName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformerCategories", x => x.PerformerCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Reliefs",
                columns: table => new
                {
                    ReliefId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReliefType = table.Column<string>(maxLength: 50, nullable: false),
                    ReliefPercent = table.Column<decimal>(type: "decimal(3, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reliefs", x => x.ReliefId);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    TransportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TransportName = table.Column<string>(maxLength: 20, nullable: false),
                    TransportDescription = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.TransportId);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(maxLength: 50, nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cities_ToCountries",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Performers",
                columns: table => new
                {
                    PerformerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PerformerName = table.Column<string>(maxLength: 50, nullable: false),
                    PerformerCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performers", x => x.PerformerId);
                    table.ForeignKey(
                        name: "FK_Performers_ToPerformerCategories",
                        column: x => x.PerformerCategoryId,
                        principalTable: "PerformerCategories",
                        principalColumn: "PerformerCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HotelName = table.Column<string>(maxLength: 50, nullable: false),
                    HotelDescription = table.Column<string>(maxLength: 500, nullable: true),
                    HotelAddress = table.Column<string>(maxLength: 50, nullable: false),
                    HotelPhoneNumber = table.Column<string>(maxLength: 12, nullable: false),
                    HotelRoomsNumber = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelId);
                    table.ForeignKey(
                        name: "FK_Hotels_ToCities",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PlaceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlaceName = table.Column<string>(maxLength: 50, nullable: false),
                    PlaceDescription = table.Column<string>(maxLength: 500, nullable: false),
                    PlaceCapacity = table.Column<int>(nullable: false),
                    PlaceAddress = table.Column<string>(maxLength: 100, nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PlaceId);
                    table.ForeignKey(
                        name: "FK_Places_ToCities",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    TourId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TourName = table.Column<string>(maxLength: 50, nullable: false),
                    TourDescription = table.Column<string>(maxLength: 500, nullable: true),
                    PerformerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.TourId);
                    table.ForeignKey(
                        name: "FK_Tours_ToPerformers",
                        column: x => x.PerformerId,
                        principalTable: "Performers",
                        principalColumn: "PerformerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoomName = table.Column<string>(maxLength: 50, nullable: false),
                    RoomDescription = table.Column<string>(maxLength: 500, nullable: true),
                    RoomBeds = table.Column<int>(nullable: false),
                    RoomPriceForNight = table.Column<decimal>(type: "decimal(9, 2)", nullable: false),
                    RoomAvailable = table.Column<bool>(nullable: false),
                    HotelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_ToHotels",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    SectorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SectorName = table.Column<string>(maxLength: 20, nullable: false),
                    SectorAvailability = table.Column<int>(nullable: false),
                    SectorCapacity = table.Column<int>(nullable: true),
                    SectorRows = table.Column<int>(nullable: true),
                    SectorRowPlaces = table.Column<int>(nullable: true),
                    PlaceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.SectorId);
                    table.ForeignKey(
                        name: "FK_Sectors_ToPlaces",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportReservations",
                columns: table => new
                {
                    TransportReservationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TransportReservationStart = table.Column<DateTime>(type: "datetime", nullable: false),
                    TransportReservationEnd = table.Column<DateTime>(type: "datetime", nullable: true),
                    TransportReservationPrice = table.Column<int>(nullable: false),
                    TransportReservationAddress = table.Column<string>(maxLength: 50, nullable: false),
                    TransportId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportReservations", x => x.TransportReservationId);
                    table.ForeignKey(
                        name: "FK_TransportReservations_ToCities",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransportReservations_ToTransports",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "TransportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PurchaseTicketDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PurchaseSelectedRow = table.Column<string>(maxLength: 10, nullable: true),
                    PurchaseSelectedRowSeat = table.Column<string>(maxLength: 10, nullable: true),
                    Id = table.Column<string>(nullable: true),
                    TicketId = table.Column<int>(nullable: false),
                    DeliveryId = table.Column<int>(nullable: false),
                    ReliefId = table.Column<int>(nullable: false),
                    HotelReservationId = table.Column<int>(nullable: true),
                    TransportReservationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_Purchases_ToDeliveries",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "DeliveryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_ToUsers",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_ToReliefs",
                        column: x => x.ReliefId,
                        principalTable: "Reliefs",
                        principalColumn: "ReliefId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_ToTransportReservations",
                        column: x => x.TransportReservationId,
                        principalTable: "TransportReservations",
                        principalColumn: "TransportReservationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HotelReservations",
                columns: table => new
                {
                    HotelReservationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HotelReservationStart = table.Column<DateTime>(type: "datetime", nullable: false),
                    HotelReservationEnd = table.Column<DateTime>(type: "datetime", nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelReservations", x => x.HotelReservationId);
                    table.ForeignKey(
                        name: "FK_HotelReservations_ToRooms",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventName = table.Column<string>(maxLength: 50, nullable: false),
                    EventDescription = table.Column<string>(maxLength: 500, nullable: true),
                    EventStart = table.Column<DateTime>(type: "datetime", nullable: false),
                    EventEnd = table.Column<DateTime>(type: "datetime", nullable: true),
                    EventTicketPurchaseLimit = table.Column<int>(nullable: false),
                    PlaceId = table.Column<int>(nullable: false),
                    TourId = table.Column<int>(nullable: true),
                    HotelReservationId = table.Column<int>(nullable: true),
                    TransportReservationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_ToHotelReservations",
                        column: x => x.HotelReservationId,
                        principalTable: "HotelReservations",
                        principalColumn: "HotelReservationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_ToPlaces",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_ToTours",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_ToTransportReservations",
                        column: x => x.TransportReservationId,
                        principalTable: "TransportReservations",
                        principalColumn: "TransportReservationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TicketName = table.Column<string>(maxLength: 50, nullable: false),
                    TicketDescription = table.Column<string>(maxLength: 500, nullable: true),
                    TicketPrice = table.Column<decimal>(type: "decimal(9, 2)", nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    SectorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_ToEvents",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_ToSectors",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "SectorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CountryId",
                table: "AspNetUsers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_HotelReservationId",
                table: "Events",
                column: "HotelReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_PlaceId",
                table: "Events",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TourId",
                table: "Events",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TransportReservationId",
                table: "Events",
                column: "TransportReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelReservations_EventId",
                table: "HotelReservations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelReservations_RoomId",
                table: "HotelReservations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CityId",
                table: "Hotels",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Performers_PerformerCategoryId",
                table: "Performers",
                column: "PerformerCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_CityId",
                table: "Places",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_DeliveryId",
                table: "Purchases",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_HotelReservationId",
                table: "Purchases",
                column: "HotelReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_Id",
                table: "Purchases",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ReliefId",
                table: "Purchases",
                column: "ReliefId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_TicketId",
                table: "Purchases",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_TransportReservationId",
                table: "Purchases",
                column: "TransportReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_PlaceId",
                table: "Sectors",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventId",
                table: "Tickets",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SectorId",
                table: "Tickets",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_PerformerId",
                table: "Tours",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportReservations_CityId",
                table: "TransportReservations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportReservations_EventId",
                table: "TransportReservations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportReservations_TransportId",
                table: "TransportReservations",
                column: "TransportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ToCountries",
                table: "AspNetUsers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportReservations_ToEvents",
                table: "TransportReservations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_ToHotelReservations",
                table: "Purchases",
                column: "HotelReservationId",
                principalTable: "HotelReservations",
                principalColumn: "HotelReservationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_ToTickets",
                table: "Purchases",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "TicketId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_ToEvents",
                table: "HotelReservations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ToCountries",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_ToCountries",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_ToHotelReservations",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_ToPlaces",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_ToTours",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_ToTransportReservations",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Reliefs");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "HotelReservations");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Performers");

            migrationBuilder.DropTable(
                name: "PerformerCategories");

            migrationBuilder.DropTable(
                name: "TransportReservations");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CountryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserAddress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserBirthdate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserCity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserFirstname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserLastLog",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserPhone",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserSex",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserSurname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
