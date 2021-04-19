using System.Collections.Generic;

namespace DDari.Models
{
    public class ChatRoom
    {
        public int id { get; set; }
     
    //private Utilisateur first { get; set; }
    //private Utilisateur second { get; set; }

    private List<Message> messageList { get; set; }
    }
}