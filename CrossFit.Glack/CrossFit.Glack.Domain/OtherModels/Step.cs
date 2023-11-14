namespace CrossFit.Glack.Domain.OtherModels
{
    public class Step
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public bool IncludeInReview { get; set; } = true;
    }
}