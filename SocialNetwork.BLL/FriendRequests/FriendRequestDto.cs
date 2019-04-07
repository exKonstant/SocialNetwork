using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.BLL.FriendRequests
{
    public class FriendRequestDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
