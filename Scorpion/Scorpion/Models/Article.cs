namespace Scorpion.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public int SubsectionId { get; set; }
    }
}