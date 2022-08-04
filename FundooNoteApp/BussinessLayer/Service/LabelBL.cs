﻿using BussinessLayer.Interface;
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
    }
}
