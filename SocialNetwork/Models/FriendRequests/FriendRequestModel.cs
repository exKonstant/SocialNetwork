using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.API.Models.FriendRequests
{
    public class FriendRequestModel
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
