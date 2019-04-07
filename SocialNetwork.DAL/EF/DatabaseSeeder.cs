using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Entities.Enums;
using System;

namespace SocialNetwork.DAL.EF
{
    public static class DatabaseSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Birthday = new DateTime(2000, 1, 1),
                    Id = 1,
                    FirstName = "Alex",
                    LastName = "Komarov",
                    Email = "alexkomarov@gmail.com",
                    Info = "Good person"
                }, new User
                {
                    Birthday = new DateTime(1999, 1, 1),
                    Id = 2,
                    FirstName = "Dmitriy",
                    LastName = "Adamenko",
                    Email = "dmitriyadamenko@gmail.com",
                    Info = "Nice person"
                }, new User
                {
                    Birthday = new DateTime(1998, 1, 1),
                    Id = 3,
                    FirstName = "Anton",
                    LastName = "Egorov",
                    Email = "antonegorov@gmail.com",
                    Info = "Very nice person"
                }, new User
                {
                    Birthday = new DateTime(1997, 1, 1),
                    Id = 4,
                    FirstName = "Egor",
                    LastName = "Antonov",
                    Email = "egorantonov@gmail.com",
                    Info = "Bad person"
                }, new User
                {
                    Birthday = new DateTime(1996, 1, 1),
                    Id = 5,
                    FirstName = "Alexey",
                    LastName = "Lenin",
                    Email = "alexeylenin@gmail.com",
                    Info = "Nice maaaaan!"
                });

            modelBuilder.Entity<Message>().HasData(
                new Message
                {
                    Id = 1,
                    ConversationId = 1,
                    Text = "Hello!",
                    UserId = 1
                },
                new Message
                {
                    Id = 2,
                    ConversationId = 2,
                    Text = "Good Morning!",
                    UserId = 2
                },
                new Message
                {
                    Id = 3,
                    ConversationId = 3,
                    Text = "Hello there!",
                    UserId = 3
                },
                new Message
                {
                    Id = 4,
                    ConversationId = 4,
                    Text = "Hi!",
                    UserId = 4
                },
                new Message
                {
                    Id = 5,
                    ConversationId = 5,
                    Text = "Hey!",
                    UserId = 5
                });
            modelBuilder.Entity<Conversation>().HasData(
                new Conversation
                {
                    ConversationName = "Work",
                    Id = 1
                },
                new Conversation
                {
                    ConversationName = "Study",
                    Id = 2
                },
                new Conversation
                {
                    ConversationName = "MusicGroup",
                    Id = 3
                },
                new Conversation
                {
                    ConversationName = "University",
                    Id = 4
                },
                new Conversation
                {
                    ConversationName = "Friends",
                    Id = 5
                });
            modelBuilder.Entity<UserConversation>().HasData(
                new UserConversation
                {
                    ConversationId = 1,
                    UserId = 1
                },
                new UserConversation
                {
                    ConversationId = 2,
                    UserId = 2
                },
                new UserConversation
                {
                    ConversationId = 3,
                    UserId = 3
                },
                new UserConversation
                {
                    ConversationId = 4,
                    UserId = 4
                },
                new UserConversation
                {
                    ConversationId = 5,
                    UserId = 5
                });
            modelBuilder.Entity<FriendRequest>().HasData(
                new FriendRequest
                {
                    ReceiverId = 2,
                    SenderId = 1,
                    FriendRequestStatus = FriendRequestStatus.Awaiting
                },
                new FriendRequest
                {
                    ReceiverId = 3,
                    SenderId = 4,
                    FriendRequestStatus = FriendRequestStatus.Awaiting
                },
                new FriendRequest
                {
                    ReceiverId = 4,
                    SenderId = 1,
                    FriendRequestStatus = FriendRequestStatus.Awaiting
                });
            modelBuilder.Entity<UserFriend>().HasData(
                new UserFriend
                {
                    UserId = 1,
                    FriendId = 3
                },
                new UserFriend
                {
                    UserId = 3,
                    FriendId = 1
                },
                new UserFriend
                {
                    UserId = 2,
                    FriendId = 5
                },
                new UserFriend
                {
                    UserId = 5,
                    FriendId = 2
                });
        }
    }
}
