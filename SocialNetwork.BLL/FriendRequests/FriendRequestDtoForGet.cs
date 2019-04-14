using System;
using System.Collections.Generic;
using System.Text;
using SocialNetwork.DAL.Entities.Enums;

namespace SocialNetwork.BLL.FriendRequests
{
    public class FriendRequestDtoForGet
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public FriendRequestStatus FriendRequestStatus { get; set; }
    }
}
