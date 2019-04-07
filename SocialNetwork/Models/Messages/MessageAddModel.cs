using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.API.Models.Messages
{
    public class MessageAddModel
    {
        public int ConversationId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
    }
}
