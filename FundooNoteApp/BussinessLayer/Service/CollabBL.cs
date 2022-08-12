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

        /// <summary>
        /// Adds the collab.
        /// </summary>
        /// <param name="collabModel">The collab model.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Removes the collab.
        /// </summary>
        /// <param name="collabID">The collab identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public string RemoveCollab(long collabID, long userId)
        {
            try
            {
                return collabRL.RemoveCollab(collabID, userId);
            }
            catch (Exception)
            {
                throw;
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
                return collabRL.GetCollab(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
