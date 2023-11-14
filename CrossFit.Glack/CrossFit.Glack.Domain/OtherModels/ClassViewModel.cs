namespace CrossFit.Glack.Domain.OtherModels
{
    public class ClassViewModel
    {
        public long ClassId { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateTimeEnd { get; set; }

        public string? InstructorName { get; set; }

        public int? SlotsTaken { get; set; }

        public int MaxAttendees { get; set; }
    }
}