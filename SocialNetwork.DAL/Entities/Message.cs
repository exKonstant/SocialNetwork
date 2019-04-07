namespace SocialNetwork.DAL.Entities
{
    public class Message : Entity
    {
        public string Text { get; set; }       
        public User User { get; set; }
        public int UserId { get; set; }
        public Conversation Conversation { get; set; }
        public int ConversationId { get; set; }

    }
}
