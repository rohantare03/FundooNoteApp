using BussinessLayer.Interface;
using CommonLayer.Modal;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class CollabBL : ICollabBL
    {
        private readonly ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
        public CollaboratorEntity AddCollab(CollabModel collabModel)
        {
            try
            {
                return collabRL.AddCollab(collabModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
