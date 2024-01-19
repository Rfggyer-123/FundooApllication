using CommonLayer.model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class ReviewRepoService : IReviewRepo
    {
        private readonly FundooContext _Context;

        public ReviewRepoService(FundooContext _Context)
        {
            this._Context = _Context;
        }

        public bool AddReview(ReviewRequest RequestModel)
        {
           if(RequestModel == null)
            {
                return false;
            }
            ReviewEntity ReviewEntity = new ReviewEntity();
            ReviewEntity.Comment = RequestModel.Comment;
            _Context.Reviews.Add(ReviewEntity);
            ReviewEntity.Rating=RequestModel.Rating;
            _Context.SaveChanges();
            return true;
        }

        public List<ReviewEntity> GetAllReview()
        {
           
            var res= _Context.Reviews.ToList();
            if(res.Count() == 0 || res==null)
            {
                return new List<ReviewEntity>();
            }

            return res;

        }
    }
}
