namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IPostRepository PostRepository{get;}
        IPostLikeRepository PostLikeRepository{get;}
        IFollowRepository FollowRepository{get;}
        ICommentRepository CommentRepository{get;}
        Task<bool> SaveChangesAsync();
    }
}