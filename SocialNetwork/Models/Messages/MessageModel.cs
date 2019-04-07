using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.API.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ConversationId { get; set; }
        public int UserId { get; set; }
    }
}
