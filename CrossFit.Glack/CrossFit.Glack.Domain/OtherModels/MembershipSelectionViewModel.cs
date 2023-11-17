using CrossFit.Glack.Domain.Models;

namespace CrossFit.Glack.Domain.OtherModels
{
    public class MembershipSelectionViewModel
    {
        public Membership Membership { get; set; }

        public List<MembershipType> MembershipTypes { get; set; }
    }
}