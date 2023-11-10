namespace ContosoCrafts.WebSite.Models
{
    public class CommentModel
    {
        // The ID for this comment, use a Guid so it is always unique
        public string Id { get; set; } = System.Guid.NewGuid().ToString();

        // The Comment getter and setter
        public string Comment { get; set; }
    }
}
