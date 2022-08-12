using BussinessLayer.Interface;
using CommonLayer.Modal;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns></returns>
        public LabelEntity CreateLabel(LabelModel labelModel)
        {
            try
            {
                return labelRL.CreateLabel(labelModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <param name="labelID">The label identifier.</param>
        /// <returns></returns>
        public LabelEntity UpdateLabel(LabelModel labelModel, long labelID)
        {
            try
            {
                return labelRL.UpdateLabel(labelModel, labelID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public LabelEntity DeleteLabel(long labelID, long userId)
        {
            try
            {
                return labelRL.DeleteLabel(labelID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<LabelEntity> GetLabels(long userId)
        {
            try
            {
                return labelRL.GetLabels(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
