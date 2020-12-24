using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mio.API.Repositories;
using Mio.Components;
using Mio.Models.Packets;
using Mio.Models.Relations;
using Mio.Models.Text;

namespace Mio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextContentController : ControllerBase
    {
        private readonly ITextContentRepository textContentRepository;
        private readonly IProductRepository productRepository;

        public TextContentController(ITextContentRepository textContentRepository, IProductRepository productRepository)
        {
            this.textContentRepository = textContentRepository;
            this.productRepository = productRepository;
        }

        // GET: api/TextContent/story
        [HttpGet("story")]
        public async Task<ActionResult<IEnumerable<Story>>> GetStories()
        {
            try
            {
                return Ok(await textContentRepository.GetStories());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error retrieving stories");
            }
        }


        // GET: api/TextContent/story
        [HttpGet("story/{userId}")]
        public async Task<ActionResult<IEnumerable<Story>>> GetStories(string userId)
        {
            try
            {
                return Ok(await textContentRepository.GetStories(userId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error retrieving stories for user with id {userId}");
            }
        }

        
        // GET: api/TextContent
        [HttpGet("{userID}")]
        public async Task<ActionResult<IEnumerable<TextContent>>> GetPosts(string userID)
        {
            try
            {
                return Ok(await textContentRepository.GetPosts(userID));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error retrieving posts for user with id {userID}");
            }
        }


        // GET: api/TextContent/story

        [HttpGet("story/{id:int}")]
        public async Task<ActionResult<Story>> GetStory(int id)
        {
            try
            {
                return Ok(await textContentRepository.GetStory(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error retrieving story with id {id}");
            }
        }

        // GET: api/TextContent/space
        [HttpGet("space/{spaceId:int}")]
        public async Task<ActionResult<IEnumerable<Story>>> GetStoryInSpace(int spaceId)
        {
            try
            {
                return Ok(await textContentRepository.GetStoriesInSpace(spaceId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error retrieving stories in space with id {spaceId}");
            }
        }

        
        // GET: api/TextContent
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TextContent>> GetTextContent(int id)
        {
            try
            {
                return Ok(await textContentRepository.GetTextContent(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error retrieving textcontent with id {id}");
            }
        }

        
        // GET: api/TextContent/comment
        [HttpGet("comment/{parentId:int}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments(int parentId)
        {
            try
            {
                return Ok(await textContentRepository.GetComments(parentId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error retrieving comments for story with id {parentId}");
            }
        }


        // GET: api/TextContent/review
        [HttpGet("review/{id:int}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            try
            {
                return Ok(await textContentRepository.GetReview(id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error retrieving review with id {id}");
            }
        }


        // GET: api/TextContent/review/product
        [HttpGet("review/product/{productId:int}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsOfProduct(int productId)
        {
            try
            {
                return Ok(await textContentRepository.GetReviews(productId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error retrieving reviews of product with id {productId}");
            }
        }
        
        // GET: api/TextContent/interaction
        [HttpGet("interaction/{userID}")]
        public async Task<ActionResult<IEnumerable<UserTextReactions>>> GetUserReactions(string userID)
        {
            try
            {
                return Ok(await textContentRepository.GetUserTextReactions(userID));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error retrieving user text reactions for user with id {userID}");
            }
        }

        // GET: api/TextContent/space
        [HttpGet("space")]
        public async Task<ActionResult<IEnumerable<Space>>> GetSpaces()
        {
            try
            {
                return Ok(await textContentRepository.GetSpaces());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error retrieving spaces");
            }
        }

        
        // POST: api/TextContent/space
        [HttpPost("space")]
        public async Task<ActionResult> AddSpace(Space space)
        {
            try
            {
                if (space == null)
                {
                    return BadRequest();
                }

                return Ok(await textContentRepository.AddSpace(space));
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Space already exists");
            }
        }


        // POST: api/TextContent/comment
        [HttpPost("comment")]
        public async Task<ActionResult> AddComment(Comment comment)
        {
            try
            {
                if (comment == null)
                {
                    return BadRequest();
                }

                return Ok(await textContentRepository.AddComment(comment));
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Comment already exists");
            }
        }

        //Task<Story> AddStory(Story story);

        // POST: api/TextContent/story
        [HttpPost("story")]
        public async Task<ActionResult> AddStory(Story story)
        {
            try
            {
                if (story == null)
                {
                    return BadRequest();
                }

                return Ok(await textContentRepository.AddStory(story));
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Story already exists");
            }
        }


        // POST: api/TextContent/review
        [HttpPost("review")]
        public async Task<ActionResult> AddReview(Review review)
        {
            try
            {
                if (review == null)
                if (review == null)
                {
                    return BadRequest();
                }

                await productRepository.UpdateProductRating(review.ID);
                return Ok(await textContentRepository.AddReview(review));
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Review already exists");
            }
        }

        // PUT: api/TextContent/interact/
        [HttpPut("interact")]
        public async Task<ActionResult<TextContent>> UpdateContentParams(PostControlsPacket postControls)
        {
            try
            {
                var option = postControls.Key;

                switch (option)
                {
                    case "x01":
                        return Ok(await textContentRepository.UpvoteTextContent(postControls.PostID, postControls.Value));
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest, $"unrecognized option received with code = {option}");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 $"Error upvoting content with id = {postControls.PostID}");
            }
        }

        // DELETE: api/TextContent/story/

        [HttpDelete("story/{storyId:int}")]
        public async Task<ActionResult<Story>> DeleteStory(int storyId)
        {
            try
            {
                return Ok(await textContentRepository.DeleteStory(storyId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting story with id {storyId}");
            }
        }


        // DELETE: api/TextContent/comment/
        [HttpDelete("comment/{commentId:int}")]
        public async Task<ActionResult<Comment>> DeleteComment(int commentId)
        {
            try
            {
                return Ok(await textContentRepository.DeleteComment(commentId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting story with id {commentId}");
            }
        }


        // DELETE: api/TextContent/review/
        [HttpDelete("review/{reviewId:int}")]
        public async Task<ActionResult<Review>> DeleteReview(int reviewId)
        {
            try
            {
                await productRepository.DeleteProductRating(reviewId);
                return Ok(await textContentRepository.DeleteReview(reviewId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting story with id {reviewId}");
            }
        }


        // DELETE: api/TextContent
        [HttpPut]
        public async Task<ActionResult<TextContent>> UpdateTextContent(TextContent textContent)
        {
            try
            {
                if (textContent == null) return BadRequest();
                return Ok(await textContentRepository.UpdateContent(textContent));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating username of user with id {textContent.ID}");
            }
        }

    }
}
