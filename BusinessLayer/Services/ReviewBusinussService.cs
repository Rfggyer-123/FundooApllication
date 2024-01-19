using BusinessLayer.Interfaces;
using CommonLayer.model;
using RepositoryLayer.Entity;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ReviewBusinussService : IReviewBusiness
    {

        public readonly IReviewRepo ReviewRepo;

        public ReviewBusinussService(IReviewRepo ReviewRepo)
        {
            this.ReviewRepo = ReviewRepo;
        }

        public bool AddReview(ReviewRequest RequestModel)
        {
           return ReviewRepo.AddReview(RequestModel);
        }

        public List<ReviewEntity> GetAllReview()
        {
           return ReviewRepo.GetAllReview();
        }
    }
}
