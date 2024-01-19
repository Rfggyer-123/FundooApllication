﻿using CommonLayer.model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IReviewBusiness
    {
        public bool AddReview(ReviewRequest RequestModel);
        public List<ReviewEntity> GetAllReview();
    }
}
