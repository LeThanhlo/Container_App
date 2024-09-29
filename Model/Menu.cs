namespace Container_App.Model
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public int? ParentMenuId { get; set; }
        public bool IsVisible { get; set; }
    }
}
