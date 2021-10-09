using Raven.Client.Documents;

namespace Likr.Comments.Interfaces
{
    public interface IRavenDbStore
    {
        public DocumentStore Store { get; init; }
    }
}