using CommonLayer.model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.interfaces
{
    public interface IReviewRepo
    {
        public bool AddReview(ReviewRequest RequestModel);
        public List<ReviewEntity> GetAllReview();
    }
}
