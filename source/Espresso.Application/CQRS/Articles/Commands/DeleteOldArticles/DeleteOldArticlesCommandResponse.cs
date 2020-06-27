namespace Espresso.Application.CQRS.Articles.Commands.DeleteOldArticles
{
    public class DeleteOldArticlesCommandResponse
    {
        public int NumberOfDeletedAricles { get; }

        public DeleteOldArticlesCommandResponse(int numberOfDeletedAricles)
        {
            NumberOfDeletedAricles = numberOfDeletedAricles;
        }

        public override string ToString()
        {
            return $"{nameof(NumberOfDeletedAricles)}:{NumberOfDeletedAricles}";
        }
    }
}
