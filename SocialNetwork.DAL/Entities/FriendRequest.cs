using SocialNetwork.DAL.Entities.Enums;

namespace SocialNetwork.DAL.Entities
{
    public class FriendRequest
    {
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }

        public FriendRequestStatus FriendRequestStatus { get; set; }
    }
}
