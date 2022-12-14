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
    public class NotesController : ControllerBase
    {
        private readonly INotesBL iNotesBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        private readonly ILogger<NotesController> logger;

        public NotesController(INotesBL iNotesBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext, ILogger<NotesController> logger)
        {
            this.iNotesBL = iNotesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
            this.logger = logger;
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateNote(NotesModel noteData)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var result = iNotesBL.AddNotes(noteData, userId);
                if (result != null)
                {
                    logger.LogInformation("Notes Created Successfully ");
                    return Ok(new { success = true, message = "Notes Created Successfully", data = result });
                }
                else
                {
                    logger.LogError("Notes Not Created");
                    return BadRequest(new { success = false, message = "Notes not Created" });
                }
            }
            catch (Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="NoteId">The note identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteNotes(long NoteId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var delete = iNotesBL.DeleteNotes(NoteId);
                if (delete != null)
                {
                    logger.LogInformation("Notes Deleted Successfully ");
                    return this.Ok(new { Success = true, message = "Notes Deleted Successfully" });
                }
                else
                {
                    logger.LogError("Notes Delete Unsuccessful");
                    return this.BadRequest(new { Success = false, message = "Notes Delete Unsuccessful" });
                }
            }
            catch (Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="NoteId">The note identifier.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNotes(NotesModel notesModel, long NoteId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var result = iNotesBL.UpdateNote(notesModel, NoteId);
                if (result != null)
                {
                    logger.LogInformation("Notes Updated Successfully ");
                    return this.Ok(new { Success = true, message = "Notes Updated Successfully", data = result });
                }
                else
                {
                    logger.LogError("Notes Update Unsuccessful");
                    return this.BadRequest(new { Success = false, message = "Notes Update Unsuccessful" });
                }
            }
            catch (Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }

        /// <summary>
        /// Retrieves the notes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Retrieve")]
        public IActionResult ReadNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var result = iNotesBL.ReadNotes(userId);
                if (result != null)
                {
                    logger.LogInformation("Notes Retrieved Successfully ");
                    return this.Ok(new { Success = true, message = "Notes Retrieved Successfully", data = result });
                }
                else
                {
                    logger.LogError("Notes Retrieve Unsuccessful");
                    return this.BadRequest(new { Success = false, message = "Notes Retrieve Unsuccessful" });
                }
            }
            catch (Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }


        /// <summary>
        /// Pin and Unpin the specified note identifier.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Pin")]
        public IActionResult Pinned(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var result = iNotesBL.Pinned(NoteID, userID);
                if (result == true)
                {
                    logger.LogInformation("Note Pinned Successfully ");
                    return Ok(new { success = true, message = "Note Pinned Successfully" });
                }
                else if (result == false)
                {
                    logger.LogInformation("Note Unpinned successfully ");
                    return Ok(new { success = true, message = "Note Unpinned successfully" });
                }
                else
                {
                    logger.LogError("Cannot perform operation");
                    return BadRequest(new { success = false, message = "Cannot perform operation." });
                }
            }
            catch (Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }

        /// <summary>
        /// Archives and Unarchives the specified note identifier.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Archive")]
        public IActionResult Archive(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var result = iNotesBL.Archive(NoteID, userID);
                if (result == true)
                {
                    logger.LogInformation("Note Archived successfully ");
                    return Ok(new { success = true, message = "Note Archived successfully" });
                }
                else if (result == false)
                {
                    logger.LogInformation("Note UnArchived successfully");
                    return Ok(new { success = true, message = "Note UnArchived successfully" });
                }
                else
                {
                    logger.LogError("Cannot perform operation");
                    return BadRequest(new { success = false, message = "Cannot perform operation" });
                }
            }
            catch (Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }


        /// <summary>
        /// Trash and Untrash the specified note identifier.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Trash")]
        public IActionResult Trash(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var result = iNotesBL.Trash(NoteID, userID);
                if (result == true)
                {
                    logger.LogInformation("Note Trashed Successfully ");
                    return Ok(new { success = true, message = "Note Trashed Successfully" });
                }
                else if (result == false)
                {
                    logger.LogInformation("Note UnTrashed Successfully ");
                    return Ok(new { success = true, message = "Note UnTrashed Successfully" });
                }
                else
                {
                    logger.LogError("Cannot perform operation");
                    return BadRequest(new { success = false, message = "Cannot perform operation" });
                }
            }
            catch (Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }

        /// <summary>
        /// Change Notes color.
        /// </summary>
        /// <param name="NoteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Color")]
        public IActionResult NoteColor(long NoteId, string color)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var colors = iNotesBL.NoteColor(NoteId, color);
                if (colors != null)
                {
                    logger.LogInformation("Color Added Successfully ");
                    return this.Ok(new { Success = true, message = "Color Added Successfully", data = colors });
                }
                else
                {
                    logger.LogError("Color Add Unsuccessful");
                    return this.BadRequest(new { Success = false, message = " Color Add Unsuccessful" });
                }
            }
            catch (Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns></returns>
/        [HttpPut]
        [Route("Image")]
        public IActionResult AddImage(long noteId, IFormFile image)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var result = iNotesBL.AddImage(noteId, userID, image);
                if (result != null)
                {
                    logger.LogInformation("Image Uploaded Successfully ");
                    return this.Ok(new { Success = true, Message = "Image Uploaded Successfully" });
                }
                else
                {
                    logger.LogError("Image Upload Unsuccessful");
                    return this.BadRequest(new { Success = true, Message = "Image Upload Unsuccessful" });
                }
            }
            catch (Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }

        /// <summary>
        /// Gets all notes using redis cache.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = fundooContext.NotesTable.ToList();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }
    }
}
