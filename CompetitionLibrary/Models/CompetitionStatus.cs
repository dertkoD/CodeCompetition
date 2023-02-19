namespace CompetitionLibrary.Models
{
    public partial class CompetitionStatus
    {
        public int CompetitionStatusId { get; set; }

        public string CompetitionStatusName { get; set; } = null!;

        public virtual ICollection<Competition> Competitions { get; } = new List<Competition>();
    }
}
