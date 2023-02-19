using CompetitionLibrary.Repositories;
namespace CompetitionLibrary.Models
{
    public partial class TaskCompetition : IObjStatus
    {
        public int TaskId { get; set; }

        public int TaskTypeId { get; set; }

        public string TaskName { get; set; } = null!;

        public string TaskDescription { get; set; } = null!;

        public TimeSpan TaskSolutionTime { get; set; }

        public int TaskPoint { get; set; }

        public TimeSpan? TaskTimeCompleted { get; set; }

        public int? TaskPointReceived { get; set; }

        public int? MainTaskId { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int CreateUserId { get; set; }

        public int ObjStatusId { get; set; }

        public int UpdateUserId { get; set; }

        public virtual ICollection<CompetitionTaskCompet> CompetitionTasksCompets { get; } = new List<CompetitionTaskCompet>();

        public virtual User CreateUser { get; set; } = null!;

        public virtual StatusObj ObjStatus { get; set; } = null!;

        public virtual TaskType TaskType { get; set; } = null!;

        public virtual User UpdateUser { get; set; } = null!;
    }
}
