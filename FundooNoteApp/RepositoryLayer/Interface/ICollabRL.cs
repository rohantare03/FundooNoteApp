﻿using CommonLayer.Modal;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        public CollaboratorEntity AddCollab(CollabModel collabModel);
        public string RemoveCollab(long collabID, long userId);
    }
}
