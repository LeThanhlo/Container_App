namespace Container_App.Model
{
    public class RoleMenuAccess
    {
        public int AccessId { get; set; }
        public int RoleGroupId { get; set; }
        public int MenuId { get; set; }
        public bool CanAccess { get; set; }
    }
}
