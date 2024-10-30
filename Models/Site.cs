namespace PO_Assignment_Project.Models
{
    public class Site
    {
        public int SiteId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? TypeOfSite {  get; set; }
        public string? Status {  get; set; }
        public DateOnly StartedOn { get; set; }
        public DateOnly EndedOn { get; set; }
        public Material? Material { get; set; }

    }
}
