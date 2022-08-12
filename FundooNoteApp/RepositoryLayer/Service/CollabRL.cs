﻿using CommonLayer.Modal;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabRL : ICollabRL
    {
        public readonly FundooContext fundooContext;

        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        /// <summary>
        /// Adds the collab.
        /// </summary>
        /// <param name="collabModel">The collab model.</param>
        /// <returns></returns>
        public CollaboratorEntity AddCollab(CollabModel collabModel)
        {
            try
            {
                var userData = fundooContext.UserTable.Where(r => r.Email == collabModel.CollabEmail).FirstOrDefault();
                var noteData = fundooContext.NotesTable.Where(r => r.NoteID == collabModel.NoteID).FirstOrDefault();
                if (userData != null && noteData != null)
                {
                    CollaboratorEntity collaboratorEntity = new CollaboratorEntity()
                    {
                        CollabEmail = collabModel.CollabEmail,
                        NoteID = collabModel.NoteID,
                        UserId = userData.UserId
                    };
                    fundooContext.CollaboratorTable.Add(collaboratorEntity);
                    fundooContext.SaveChanges();
                    return collaboratorEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes the collab.
        /// </summary>
        /// <param name="collabID">The collab identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public string RemoveCollab(long collabID, long userId)
        {
            var collab = fundooContext.CollaboratorTable.Where(r => r.CollabID == collabID).FirstOrDefault();
            if (collab != null)
            {
                fundooContext.CollaboratorTable.Remove(collab);
                fundooContext.SaveChanges();
                return "Removed Successfully";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the collab.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<CollaboratorEntity> GetCollab(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.CollaboratorTable.ToList().Where(r => r.NoteID == noteId);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
