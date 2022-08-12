using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public NotesRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
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
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.Title = notesModel.Title;
                notesEntity.Description = notesModel.Description;
                notesEntity.Reminder = notesModel.Reminder;
                notesEntity.Color = notesModel.Color;
                notesEntity.Image = notesModel.Image;
                notesEntity.Archive = notesModel.Archive;
                notesEntity.Pin = notesModel.Pin;
                notesEntity.Trash = notesModel.Trash;
                notesEntity.Created = notesModel.Created;
                notesEntity.Edited = notesModel.Edited;
                notesEntity.UserId = userId;

                fundooContext.NotesTable.Add(notesEntity);
                int result = fundooContext.SaveChanges();
                if (result != 0)
                {
                    return notesEntity;
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
        /// Updates the note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="NoteId">The note identifier.</param>
        /// <returns></returns>
        public NotesEntity UpdateNote(NotesModel notesModel, long NoteId)
        {
            try
            {
                var update = fundooContext.NotesTable.Where(x => x.NoteID == NoteId).FirstOrDefault();
                if (update != null)
                {
                    update.Title = notesModel.Title;
                    update.Description = notesModel.Description;
                    update.Reminder = notesModel.Reminder;
                    update.Color = notesModel.Color;
                    update.Image = notesModel.Image;
                    update.Pin = notesModel.Pin;
                    update.Archive = notesModel.Archive;
                    update.Trash = notesModel.Trash;
                    fundooContext.NotesTable.Update(update);
                    fundooContext.SaveChanges();
                    return update;
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
        /// Deletes the notes.
        /// </summary>
        /// <param name="NoteId">The note identifier.</param>
        /// <returns></returns>
        public NotesEntity DeleteNotes(long NoteId)
        {
            try
            {
                var deleteNote = fundooContext.NotesTable.Where(x => x.NoteID == NoteId).FirstOrDefault();
                if (deleteNote != null)
                {
                    fundooContext.NotesTable.Remove(deleteNote);
                    fundooContext.SaveChanges();
                    return deleteNote;
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
        /// Retrieve the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<NotesEntity> ReadNotes(long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(id => id.UserId == userId);
                return result;
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
                var result = fundooContext.NotesTable.Where(r => r.UserId == userId && r.NoteID == NoteID).FirstOrDefault();

                if (result.Pin == true)
                {
                    result.Pin = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Archive and Unarchive the specified note identifier.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public bool Archive(long NoteID, long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(r => r.UserId == userId && r.NoteID == NoteID).FirstOrDefault();

                if (result.Archive == true)
                {
                    result.Archive = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Archive = true;
                    fundooContext.SaveChanges();
                    return true;
                }
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
                var result = fundooContext.NotesTable.Where(r => r.UserId == userId && r.NoteID == NoteID).FirstOrDefault();

                if (result.Trash == true)
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Trash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
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
            var result = fundooContext.NotesTable.Where(r => r.NoteID == NoteId).FirstOrDefault();
            try
            {
                if (result != null)
                {
                    if (color != null)
                    {
                        result.Color = color;
                        fundooContext.NotesTable.Update(result);
                        fundooContext.SaveChanges();
                        return result;
                    }
                    else
                    {
                        return null;
                    }
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
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault();
                if (result != null)
                {
                    Account account = new Account(this.configuration["CloudinaryAccount:CloudName"], this.configuration["CloudinaryAccount:APIKey"], this.configuration["CloudinaryAccount:APISecret"]);

                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParameters = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParameters);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = imagePath;
                    fundooContext.SaveChanges();
                    return "Image Upload Successfully";
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
