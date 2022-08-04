using CommonLayer.Modal;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity AddNotes(NotesModel notesModel, long userId);
        public NotesEntity UpdateNote(NotesModel notesModel, long NoteId);
        public NotesEntity DeleteNotes(long NoteId);
        public IEnumerable<NotesEntity> ReadNotes(long userId);
        public bool Pinned(long NoteID, long userId);
        public bool Archive(long NoteID, long userId);
        public bool Trash(long NoteID, long userId);
        public NotesEntity NoteColor(long NoteId, string color);
    }
}
