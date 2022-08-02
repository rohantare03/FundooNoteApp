﻿using CommonLayer.Modal;
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
    }
}
