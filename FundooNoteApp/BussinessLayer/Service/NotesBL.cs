using BussinessLayer.Interface;
using CommonLayer.Modal;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL iNotesRL;

        public NotesBL(INotesRL iNotesRL)
        {
            this.iNotesRL = iNotesRL;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public NotesEntity AddNotes(NotesModel notesModel, long userId)
        {
            try
            {
                return iNotesRL.AddNotes(notesModel, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="NoteId">The note identifier.</param>
        /// <returns></returns>
        public NotesEntity UpdateNote(NotesModel notesModel, long NoteId)
        {
            try
            {
                return iNotesRL.UpdateNote(notesModel, NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="NoteId">The note identifier.</param>
        /// <returns></returns>
        public NotesEntity DeleteNotes(long NoteId)
        {
            try
            {
                return iNotesRL.DeleteNotes(NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<NotesEntity> ReadNotes(long userId)
        {
            try
            {
                return iNotesRL.ReadNotes(userId);
            }
            catch (Exception)
            { 
                throw;
            }
        }

        /// <summary>
        /// Pin and Unpin the specified note identifier.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public bool Pinned(long NoteID, long userId)
        {
            try
            {
                return iNotesRL.Pinned(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Archives and Unarchives the specified note identifier.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public bool Archive(long NoteID, long userId)
        {
            try
            {
                return iNotesRL.Archive(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Trash and Untrash the specified note identifier.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public bool Trash(long NoteID, long userId)
        {
            try
            {
                return iNotesRL.Trash(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Change Notes color.
        /// </summary>
        /// <param name="NoteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public NotesEntity NoteColor(long NoteId, string color)
        {
            try
            {
                return iNotesRL.NoteColor(NoteId, color);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public string AddImage(long NoteID, long userId, IFormFile image)
        {
            try
            {
                return iNotesRL.AddImage(NoteID, userId, image);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
