using Microsoft.EntityFrameworkCore;
using Mio.API.Context;
using Mio.Models.Packets;
using Mio.Models.Relations;
using Mio.Models.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.API.Repositories
{
    public interface ITextContentRepository
    {
        Task<IEnumerable<Story>> GetStories();
        Task<IEnumerable<Story>> GetStories(string userID);
        Task<IEnumerable<TextContent>> GetPosts(string userID);
        Task<PostPacket> GetStory(int id);
        Task<IEnumerable<Story>> GetStoriesInSpace(int spaceId);
        Task<TextContent> GetTextContent(int id);
        Task<IEnumerable<Comment>> GetComments(int parentId);
        Task<Review> GetReview(int id);
        Task<IEnumerable<Review>> GetReviews(int productId);
        Task<IEnumerable<UserTextReactions>> GetUserTextReactions(string userID);
        Task<IEnumerable<Space>> GetSpaces();
        Task<Space> AddSpace(Space space);
        Task<Comment> AddComment(Comment comment);
        Task<Story> AddStory(Story story);
        Task<Review> AddReview(Review review);
        Task<TextContent> UpvoteTextContent(int Id, int val);
        Task<Story> DeleteStory(int storyId);
        Task<Comment> DeleteComment(int commentId);
        Task<Review> DeleteReview(int reviewId);
        Task<bool> DeleteReviews(int productId);
        Task<Space> DeleteSpace(int spaceId);
        Task<TextContent> UpdateContent(TextContent textContent);
    }

    public class TextContentRepository : ITextContentRepository
    {
        private readonly MioContext _context;

        public TextContentRepository(MioContext mioContext)
        {
            this._context = mioContext;
        }


        private async Task UpdateNumberComments(TextContent content, int val)
        {
            if (content.NumComments > 0) { content.NumComments += val; }

            _context.Entry(content).CurrentValues.SetValues(content);
            await _context.SaveChangesAsync();
        }



        public async Task<Comment> AddComment(Comment comment)
        {
            var result = await _context.Comments.AddAsync(comment);
            UserTextReactions userTextInteractions = new UserTextReactions { Commented = true, TextContentID = comment.ParentContentID, UserID = comment.UserID };
            await _context.UserTextReactions.AddAsync(userTextInteractions);
            var parent = await _context.TextContents.FindAsync(comment.ParentContentID);
            await this.UpdateNumberComments(parent, 1);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Story> AddStory(Story story)
        {
            if(story.SpaceID == 0)
                await AddSpace(story.Space);

            var result = await _context.Stories.AddAsync(story);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Comment> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {

                var userTextInteraction = await _context.UserTextReactions.FirstOrDefaultAsync
                    (x => x.TextContentID == comment.ParentContentID && x.UserID == comment.UserID);
                _context.UserTextReactions.Remove(userTextInteraction);


                var parent = await _context.TextContents.FindAsync(comment.ParentContentID);
                await this.UpdateNumberComments(parent, -1);

                var comments = await _context.Comments.Where(x => x.ParentContentID == id).Select(x => x.ID).ToListAsync();
                comments.ForEach(async x => await DeleteComment(x));

                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
            return comment;
        }

        public async Task<Story> DeleteStory(int id)
        {
            var result = await _context.Stories.Include(x => x.Space).FirstOrDefaultAsync(e => e.ID == id);
            if (result != null)
            {
                var comments = await _context.Comments.Where(x => x.ParentContentID == id).Select(x => x.ID).ToListAsync();
                comments.ForEach(async x => await DeleteComment(x));


                var space = result.Space;
                space.NumberStories -= 1;
                if (space.NumberStories > 0)
                {
                    _context.Spaces.Attach(space);
                    _context.Entry(space).Property(x => x.NumberStories).IsModified = true;
                }else{
                    await this.DeleteSpace(space.ID);
                }

                    _context.Stories.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<Comment>> GetComments(int parentId)
        {
            return await _context.Comments.Where(c => c.ParentContentID == parentId).ToListAsync();
        }

        public async Task<IEnumerable<TextContent>> GetPosts(string userID)
        {
            return await _context.TextContents.Where(c => c.UserID == userID).ToListAsync();
        }

        public async Task<IEnumerable<Story>> GetStories()
        {
            return await _context.Stories.ToListAsync();
        }

        public async Task<IEnumerable<Story>> GetStories(string userID)
        {
            return await _context.Stories.Where(c => c.UserID == userID).ToListAsync();
        }

        public async Task<PostPacket> GetStory(int id)
        {
            var packet = new PostPacket
            {
                Story = _context.Stories.Find(id),
                Comments = await _context.Comments.Where(x => x.ParentContentID == id).ToListAsync()
            };
            return packet;
        }

        public async Task<IEnumerable<UserTextReactions>> GetUserTextReactions(string userID)
        {
            return await _context.UserTextReactions.Where(x => x.UserID == userID).ToListAsync();
        }

        public async Task<TextContent> UpdateContent(TextContent textContent)
        {
            var entity = _context.TextContents.Find(textContent.ID);

            if (entity == null)
            {
                return null;
            }

            _context.Entry(entity).CurrentValues.SetValues(textContent);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TextContent> UpvoteTextContent(int Id, int val)
        {
            var entity = _context.TextContents.Find(Id);

            if (entity == null)
            {
                return null;
            }

            if (entity.Upvotes > 0) { entity.Upvotes += val; }

            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TextContent> GetTextContent(int id)
        {
            return await _context.TextContents.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<Story>> GetStoriesInSpace(int spaceId)
        {
            return await _context.Stories.Where(x => x.SpaceID == spaceId).ToListAsync();
        }

        public async Task<Review> GetReview(int id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<Review>> GetReviews(int productId)
        {
            return await _context.Reviews.Where(x => x.ProductID == productId).ToListAsync();
        }

        public async Task<IDictionary<int, UserTextReactions>> GetUserTextInteractions(string userID)
        {
            Dictionary<int, UserTextReactions> dict = new Dictionary<int, UserTextReactions>();
            var userTextInteractions = await _context.UserTextReactions.Where(x => x.UserID == userID).ToListAsync();
            userTextInteractions.ForEach(x => dict.Add(x.TextContentID, x));
            return dict;
        }

        public async Task<IEnumerable<Space>> GetSpaces()
        {
            return await _context.Spaces.ToListAsync();
        }

        public async Task<Space> AddSpace(Space space)
        {
            var result = await _context.Spaces.AddAsync(space);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Review> AddReview(Review review)
        {
            if(await _context.Reviews.FirstOrDefaultAsync(x => x.ProductID == review.ProductID && x.UserID == review.UserID)  != null)
            {
                return null;
            }
            var result = await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Review> DeleteReview(int reviewId)
        {
            var result = await _context.Reviews.FirstOrDefaultAsync(e => e.ID == reviewId);
            if (result != null)
            {
                _context.Reviews.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Space> DeleteSpace(int spaceId)
        {
            var result = await _context.Spaces.FirstOrDefaultAsync(e => e.ID == spaceId);
            if (result != null)
            {
                _context.Spaces.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        private async Task DeleteReview(Review review)
        {
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteReviews(int productId)
        {
            var reviews = await _context.Reviews.Where(x => x.ProductID == productId).ToListAsync();
            foreach(var review in reviews)
            {
                await this.DeleteReview(review);
            }
            return true;
        }
    }
}
