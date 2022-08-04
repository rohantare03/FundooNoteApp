using BussinessLayer.Interface;
using CommonLayer.Modal;
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
    }
}
