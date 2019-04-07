using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.BLL.Messages
{
    public class MessageDtoForCreate
    {
        public string Text { get; set; }
        public int ConversationId { get; set; }
        public int UserId { get; set; }
    }
}
