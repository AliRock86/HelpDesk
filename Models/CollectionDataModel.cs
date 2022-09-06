namespace HelpDesk.Models
{
    public class CollectionDataModel
    {
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
    }
}
