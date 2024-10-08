namespace Container_App.Model
{
    public class ProjectUserInvite
    {
        public int InviteId { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; } // "Pending", "Accepted", "Rejected"
    }
}
