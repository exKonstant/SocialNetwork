using System.Collections.Generic;

namespace SocialNetwork.DAL.Entities
{
    public class Conversation : Entity
    {
        public ICollection<Message> Messages { get; set; }
        public ICollection<UserConversation> UserConversations { get; set; }
        public string ConversationName { get; set; }
        

    }
}
