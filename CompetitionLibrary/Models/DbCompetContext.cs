using Microsoft.EntityFrameworkCore;

namespace CompetitionLibrary.Models
{
    public partial class DbCompetContext : DbContext
    {
        public DbCompetContext()
        {
        }

        public DbCompetContext(DbContextOptions<DbCompetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Competition> Competitions { get; set; }

        public virtual DbSet<CompetitionStatus> CompetitionStatuses { get; set; }

        public virtual DbSet<CompetitionTaskCompet> CompetitionTasksCompets { get; set; }

        public virtual DbSet<CompetitionTeam> CompetitionTeams { get; set; }

        public virtual DbSet<CompetitionTeamTask> CompetitionTeamTasks { get; set; }

        public virtual DbSet<CompetitionTeamTaskStatus> CompetitionTeamTaskStatuses { get; set; }

        public virtual DbSet<CompetitionUser> CompetitionUsers { get; set; }

        public virtual DbSet<CompetitionUserCompetTeamTask> CompetitionUserCompetTeamTasks { get; set; }

        public virtual DbSet<StatusObj> StatusObjs { get; set; }

        public virtual DbSet<TaskType> TaskTypes { get; set; }

        public virtual DbSet<TaskCompetition> TasksCompetitions { get; set; }

        public virtual DbSet<Team> Teams { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=localhost;Database=DbCompet;Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.HasOne(d => d.CompetitionStatus).WithMany(p => p.Competitions)
                    .HasForeignKey(d => d.CompetitionStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Competitions_CompetitionStatus");

                entity.HasOne(d => d.CreateUser).WithMany(p => p.CompetitionCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Competitions_CreateUser");

                entity.HasOne(d => d.UpdateUser).WithMany(p => p.CompetitionUpdateUsers)
                    .HasForeignKey(d => d.UpdateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Competitions_UpdateUser");
            });

            modelBuilder.Entity<CompetitionTaskCompet>(entity =>
            {
                entity.HasKey(e => e.CompetitionTaskCompetId);

                entity.ToTable("CompetitionTasksCompet");

                entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionTasksCompet)
                    .HasForeignKey(d => d.CompetitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTasksCompet_Competition");

                entity.HasOne(d => d.CreateUser).WithMany(p => p.CompetitionTasksCompetCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTasksCompet_CreateUser");

                entity.HasOne(d => d.ObjStatus).WithMany(p => p.CompetitionTasksCompets)
                    .HasForeignKey(d => d.ObjStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTasksCompet_StatusObj");

                entity.HasOne(d => d.Task).WithMany(p => p.CompetitionTasksCompets)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTasksCompet_TaskCompetition");

                entity.HasOne(d => d.UpdateUser).WithMany(p => p.CompetitionTasksCompetUpdateUsers)
                    .HasForeignKey(d => d.UpdateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTasksCompet_UpateUser");
            });

            modelBuilder.Entity<CompetitionTeam>(entity =>
            {
                entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionTeams)
                    .HasForeignKey(d => d.CompetitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTeams_Competition");

                entity.HasOne(d => d.CreateUser).WithMany(p => p.CompetitionTeamCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTeams_CreateUser");

                entity.HasOne(d => d.ObjStatus).WithMany(p => p.CompetitionTeams)
                    .HasForeignKey(d => d.ObjStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTeams_StatusObj");

                entity.HasOne(d => d.Team).WithMany(p => p.CompetitionTeams)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_CompetitionTeams_Teams");

                entity.HasOne(d => d.UpdateUser).WithMany(p => p.CompetitionTeamUpdateUsers)
                    .HasForeignKey(d => d.UpdateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTeams_UpdateUser");
            });

            modelBuilder.Entity<CompetitionTeamTask>(entity =>
            {
                entity.HasOne(d => d.CompetitionTaskCompet).WithMany(p => p.CompetitionTeamTasks)
                    .HasForeignKey(d => d.CompetitionTaskCompetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTeamTasks_CompetitionTaskCompet");

                entity.HasOne(d => d.CompetitionTeam).WithMany(p => p.CompetitionTeamTasks)
                    .HasForeignKey(d => d.CompetitionTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTeamTasks_CompetitionTeam");

                entity.HasOne(d => d.CompetitionTeamTaskCheckingUser).WithMany(p => p.CompetitionTeamTaskCompetitionTeamTaskCheckingUsers)
                    .HasForeignKey(d => d.CompetitionTeamTaskCheckingUserId)
                    .HasConstraintName("FK_CompetitionTeamTasks_CheckingUser");

                entity.HasOne(d => d.CompetitionTeamTaskStatus).WithMany(p => p.CompetitionTeamTasks)
                    .HasForeignKey(d => d.CompetitionTeamTaskStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTeamTasks_CompetitionTeamTaskStatus");

                entity.HasOne(d => d.CreateUser).WithMany(p => p.CompetitionTeamTaskCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTeamTasks_CreateUser");

                entity.HasOne(d => d.ObjStatus).WithMany(p => p.CompetitionTeamTasks)
                    .HasForeignKey(d => d.ObjStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTeamTasks_StatusObj");

                entity.HasOne(d => d.UpdateUser).WithMany(p => p.CompetitionTeamTaskUpdateUsers)
                    .HasForeignKey(d => d.UpdateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionTeamTasks_UpdateUser");
            });

            modelBuilder.Entity<CompetitionUser>(entity =>
            {
                entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionUsers)
                    .HasForeignKey(d => d.CompetitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionUsers_Competition");

                entity.HasOne(d => d.CompetitionTeam).WithMany(p => p.CompetitionUsers)
                    .HasForeignKey(d => d.CompetitionTeamId)
                    .HasConstraintName("FK_CompetitionUsers_CompetitionTeam");

                entity.HasOne(d => d.CreateUser).WithMany(p => p.CompetitionUserCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionUsers_CreateUser");

                entity.HasOne(d => d.ObjStatus).WithMany(p => p.CompetitionUsers)
                    .HasForeignKey(d => d.ObjStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionUsers_StatusObj");

                entity.HasOne(d => d.UpdateUser).WithMany(p => p.CompetitionUserUpdateUsers)
                    .HasForeignKey(d => d.UpdateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionUsers_UpdateUser");

                entity.HasOne(d => d.User).WithMany(p => p.CompetitionUserUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionUsers_User");
            });

            modelBuilder.Entity<CompetitionUserCompetTeamTask>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.CompetitionTeamTask).WithMany()
                    .HasForeignKey(d => d.CompetitionTeamTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionUserCompetTeamTasks_CompetitionTeamTasks");

                entity.HasOne(d => d.CompetitionUser).WithMany()
                    .HasForeignKey(d => d.CompetitionUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionUserCompetTeamTasks_CompetitionUser");
            });

            modelBuilder.Entity<StatusObj>(entity =>
            {
                entity.HasKey(e => e.ObjStatusId).HasName("PK_StatusObj_1");

                entity.ToTable("StatusObj");

                entity.Property(e => e.ObjStatusId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TaskCompetition>(entity =>
            {
                entity.HasKey(e => e.TaskId);

                entity.HasOne(d => d.CreateUser).WithMany(p => p.TasksCompetitionCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TasksCompetitions_CreateUser");

                entity.HasOne(d => d.ObjStatus).WithMany(p => p.TasksCompetitions)
                    .HasForeignKey(d => d.ObjStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TasksCompetitions_StatusObj");

                entity.HasOne(d => d.TaskType).WithMany(p => p.TasksCompetitions)
                    .HasForeignKey(d => d.TaskTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TasksCompetitions_TasksType");

                entity.HasOne(d => d.UpdateUser).WithMany(p => p.TasksCompetitionUpdateUsers)
                    .HasForeignKey(d => d.UpdateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TasksCompetitions_UpdateUser");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasOne(d => d.CreateUser).WithMany(p => p.TeamCreateUsers)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teams_CreateUser");

                entity.HasOne(d => d.ObjStatus).WithMany(p => p.Teams)
                    .HasForeignKey(d => d.ObjStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teams_StatusObj");

                entity.HasOne(d => d.UpdateUser).WithMany(p => p.TeamUpdateUsers)
                    .HasForeignKey(d => d.UpdateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teams_UpdateUser");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(d => d.ObjStatus).WithMany(p => p.Users)
                    .HasForeignKey(d => d.ObjStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_StatusObj");

                entity.HasOne(d => d.Team).WithMany(p => p.Users)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_Users_Teams");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
