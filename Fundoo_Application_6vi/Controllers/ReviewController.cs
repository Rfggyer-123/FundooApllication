using BusinessLayer.Interfaces;
using CommonLayer.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fundoo_Application_6vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewBusiness reviewBusiness;
       // private readonly INoteEntityBusiness noteEntityBusiness;

        public ReviewController(IReviewBusiness reviewBusiness, INoteEntityBusiness noteEntityBusiness)
        {
            this.reviewBusiness = reviewBusiness;
            //this.noteEntityBusiness = noteEntityBusiness;
        }

        [HttpPost]
        public IActionResult AddReview(ReviewRequest RequestMOdel)
        {
            try
            {
                var result=reviewBusiness.AddReview(RequestMOdel);

                if (result == true)
                { 
                
                 return Ok(new {success=result,message="Review Added Successfully" });
                }
                else
                {
                    return BadRequest(new { success = result, message = "Review Adding Failure" });
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        public IActionResult GetAllReview()
        {

            try
            {
                var result = reviewBusiness.GetAllReview();
                if (result != null)
                {
                    return Ok(new { sucess = true, message = " these are LIst of Reviews ", data = result });
                }else
                {
                    return BadRequest(new { success = false, message = " NO REviews ", data = result });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
