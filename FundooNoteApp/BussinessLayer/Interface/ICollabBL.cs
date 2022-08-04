﻿using CommonLayer.Modal;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollaboratorEntity AddCollab(CollabModel collabModel);
        public string RemoveCollab(long collabID, long userId);
        public IEnumerable<CollaboratorEntity> GetCollab(long noteId, long userId);
    }
}
