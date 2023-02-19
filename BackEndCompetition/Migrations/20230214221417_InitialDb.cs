using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndCompetition.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    CompetitionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompetitionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompetitionStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompetitionEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompetitionCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompetitionStatusId = table.Column<int>(type: "int", nullable: false),
                    NumberOfUsers = table.Column<int>(type: "int", nullable: true),
                    CompetitionMaxCountOfTeamMembers = table.Column<int>(type: "int", nullable: true),
                    CompetitionMinCountOfTeamMembers = table.Column<int>(type: "int", nullable: true),
                    CompetitionMaxCountOfCompetitionMembers = table.Column<int>(type: "int", nullable: true),
                    CompetitionMinCountOfCompetitionMembers = table.Column<int>(type: "int", nullable: true),
                    CreateUserId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObjStatusId = table.Column<int>(type: "int", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.CompetitionId);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionStatuses",
                columns: table => new
                {
                    CompetitionStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionStatuses", x => x.CompetitionStatusId);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionTasksCompet",
                columns: table => new
                {
                    CompetitionTaskCompetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    CompetitionTaskSolutionTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    CompetitionTaskPoint = table.Column<int>(type: "int", nullable: true),
                    CompetitionTaskTimeCompleted = table.Column<TimeSpan>(type: "time", nullable: true),
                    CompetitionTaskPointReceived = table.Column<int>(type: "int", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUserId = table.Column<int>(type: "int", nullable: false),
                    ObjStatusId = table.Column<int>(type: "int", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionTasksCompet", x => x.CompetitionTaskCompetId);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionTeams",
                columns: table => new
                {
                    CompetitionTeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    CompetitionTeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompetitionTeamAvatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObjStatusId = table.Column<int>(type: "int", nullable: false),
                    TeamTwitterPoint = table.Column<bool>(type: "bit", nullable: false),
                    CompetitionTeamPoint = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionTeams", x => x.CompetitionTeamId);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionTeamTasks",
                columns: table => new
                {
                    CompetitionTeamTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionTeamId = table.Column<int>(type: "int", nullable: false),
                    CompetitionTaskId = table.Column<int>(type: "int", nullable: false),
                    CompetitionTeamTaskStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompetitionTeamTaskEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompetitionTeamTaskStatusId = table.Column<int>(type: "int", nullable: false),
                    CompetitionTeamTaskUrlGitHub = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompetitionTeamTaskCheckingUserId = table.Column<int>(type: "int", nullable: true),
                    CreateUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObjStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionTeamTasks", x => x.CompetitionTeamTaskId);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionTeamTaskStatuses",
                columns: table => new
                {
                    CompetitionTeamTaskStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionTeamTaskStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionTeamTaskStatuses", x => x.CompetitionTeamTaskStatusId);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionUserCompetTeamTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionUserId = table.Column<int>(type: "int", nullable: false),
                    CompetitionTeamTaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionUserCompetTeamTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionUsers",
                columns: table => new
                {
                    CompetitionUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompetitionTeamId = table.Column<int>(type: "int", nullable: true),
                    CreateUserId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObjStatusId = table.Column<int>(type: "int", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionUsers", x => x.CompetitionUserId);
                });

            migrationBuilder.CreateTable(
                name: "StatusObj",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjStatusId = table.Column<int>(type: "int", nullable: false),
                    ObjStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusObj", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TasksCompetitions",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskTypeId = table.Column<int>(type: "int", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskSolutionTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    TaskPoint = table.Column<int>(type: "int", nullable: false),
                    TaskTimeCompleted = table.Column<TimeSpan>(type: "time", nullable: true),
                    TaskPointReceived = table.Column<int>(type: "int", nullable: true),
                    MainTaskId = table.Column<int>(type: "int", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUserId = table.Column<int>(type: "int", nullable: false),
                    ObjStatusId = table.Column<int>(type: "int", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksCompetitions", x => x.TaskId);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypes",
                columns: table => new
                {
                    TaskTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => x.TaskTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObjStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserSecondName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAvatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObjStatusId = table.Column<int>(type: "int", nullable: false),
                    UserBirthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserBirthdayVisibility = table.Column<bool>(type: "bit", nullable: false),
                    UserGitHub = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    UserAuth0Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "CompetitionStatuses");

            migrationBuilder.DropTable(
                name: "CompetitionTasksCompet");

            migrationBuilder.DropTable(
                name: "CompetitionTeams");

            migrationBuilder.DropTable(
                name: "CompetitionTeamTasks");

            migrationBuilder.DropTable(
                name: "CompetitionTeamTaskStatuses");

            migrationBuilder.DropTable(
                name: "CompetitionUserCompetTeamTasks");

            migrationBuilder.DropTable(
                name: "CompetitionUsers");

            migrationBuilder.DropTable(
                name: "StatusObj");

            migrationBuilder.DropTable(
                name: "TasksCompetitions");

            migrationBuilder.DropTable(
                name: "TaskTypes");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
