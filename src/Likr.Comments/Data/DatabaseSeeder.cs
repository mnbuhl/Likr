using System;
using System.Collections.Generic;
using Likr.Comments.Entities;
using Raven.Client.Documents;

namespace Likr.Comments.Data
{
    public class DatabaseSeeder
    {
        private readonly IDocumentStore _store;
        private static Random _random = new Random();
        
        public DatabaseSeeder(IDocumentStore documentStore)
        {
            _store = documentStore;
        }

        public void Seed()
        {
            using var session = _store.OpenSession();

            List<Guid> commentIds = new List<Guid>();

            for (int i = 0; i < 200; i++)
            {
                commentIds.Add(Guid.NewGuid());
            }

            foreach (var id in commentIds)        
            {
                session.Store(new Comment
                {
                    Id = id.ToString(),
                    Body = "This is a test comment",
                    LikesCount = _random.Next(0, 100),
                    PostId = _random.Next(5) > 2 ? commentIds[_random.Next(0, 200)] : Guid.NewGuid(),
                    UserId = Guid.NewGuid().ToString()
                }, id.ToString());
            }
            
            session.SaveChanges();
        }
    }
}