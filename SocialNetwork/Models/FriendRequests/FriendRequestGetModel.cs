using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.DAL.Entities.Enums;

namespace SocialNetwork.API.Models.FriendRequests
{
    public class FriendRequestGetModel
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public FriendRequestStatus FriendRequestStatus { get; set; }
    }
}
