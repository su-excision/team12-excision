namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Model for comments posted by users. Contains a unique
    /// identifier and the text of the comment.
    /// </summary>
    public class CommentModel
    {
        /// <summary>
        /// Unique Identifier for the comment.
        /// </summary>
        public string Id { get; set; } = System.Guid.NewGuid().ToString();

        /// <summary>
        /// String representation of a comment posted by a user.
        /// </summary>
        public string Comment { get; set; }
    }
}
