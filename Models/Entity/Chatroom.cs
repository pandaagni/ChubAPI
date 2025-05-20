using Microsoft.EntityFrameworkCore;

namespace ChubAPI.Models.Entity
{
    [PrimaryKey(nameof(ChatroomId))]
    public class Chatroom
    {
        public Guid ChatroomId { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
