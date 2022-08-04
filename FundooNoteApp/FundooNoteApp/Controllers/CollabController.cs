﻿using BussinessLayer.Interface;
using CommonLayer.Modal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Linq;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly FundooContext fundooContext;
        public CollabController(ICollabBL collabBL, FundooContext fundooContext)
        {
            this.collabBL = collabBL;
            this.fundooContext = fundooContext;
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
                        return Ok(new { Success = true, message = "Collaboration Successful", data = result });
                    }
                    else
                    {
                        return BadRequest(new { Sucess = false, message = "Collaboration Unsuccessful" });
                    }
                }
                else
                {
                    return Unauthorized(new { Sucess = false, message = "Failed Collaboration" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
