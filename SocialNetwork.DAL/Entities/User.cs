using System;
using System.Collections.Generic;

namespace SocialNetwork.DAL.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<UserConversation> UserConversations { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Info { get; set; }
        public ICollection<Message> Messages { get; set; }        
        public ICollection<UserFriend> Friends { get; set; }
        public ICollection<FriendRequest> FriendRequestSent { get; set; }
        public ICollection<FriendRequest> FriendRequestReceived { get; set; }
    }
}
