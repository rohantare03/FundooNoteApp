using BussinessLayer.Interface;
using CommonLayer.Modal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        private readonly ILogger<CollabController> logger;

        public CollabController(ICollabBL collabBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext, ILogger<CollabController> logger)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult AddCollab(CollabModel collabModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var collab = fundooContext.NotesTable.Where(r => r.NoteID == collabModel.NoteID).FirstOrDefault();
                if (collab.UserId == userId)
                {
                    var result = collabBL.AddCollab(collabModel);
                    if (result != null)
                    {
                        logger.LogInformation("Collaboration Successful");
                        return Ok(new { Success = true, message = "Collaboration Successful", data = result });
                    }
                    else
                    {
                        logger.LogError("Collaboration Unsuccessful");
                        return BadRequest(new { Sucess = false, message = "Collaboration Unsuccessful" });
                    }
                }
                else
                {
                    logger.LogError("Failed Collaboration");
                    return Unauthorized(new { Sucess = false, message = "Failed Collaboration" });
                }
            }
            catch (Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult RemoveCollab(long collabID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var delete = collabBL.RemoveCollab(collabID, userId);
                if (delete != null)
                {
                    logger.LogInformation("Collaboration Removed Successfully");
                    return Ok(new { Success = true, message = "Collaboration Removed Successfully" });
                }
                else
                {
                    logger.LogError("Collaboration  Remove Unsuccessful");
                    return BadRequest(new { Success = false, message = "Collaboration  Remove Unsuccessful" });
                }
            }
            catch (Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }

        [HttpGet]
        [Route("Retrieve")]
        public IActionResult GetCollab(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var notes = collabBL.GetCollab(noteId, userId);
                if (notes != null)
                {
                    logger.LogInformation("Collaborations Found Successfully");
                    return Ok(new { Success = true, message = "Collaborations Found Successfully", data = notes });

                }
                else
                {
                    logger.LogError("No Collaborations  Found");
                    return BadRequest(new { Success = false, message = "No Collaborations  Found" });
                }
            }
            catch (Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }

        [HttpGet]
        [Route("Redis")]
        public async Task<IActionResult> GetAllCollaboratorUsingRedisCache()
        {
            var cacheKey = "CollaboratorList";
            string serializedCollabList;
            var CollaboratorList = new List<CollaboratorEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                CollaboratorList = JsonConvert.DeserializeObject<List<CollaboratorEntity>>(serializedCollabList);
            }
            else
            {
                CollaboratorList = fundooContext.CollaboratorTable.ToList();
                serializedCollabList = JsonConvert.SerializeObject(CollaboratorList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(CollaboratorList);
        }
    }
}
