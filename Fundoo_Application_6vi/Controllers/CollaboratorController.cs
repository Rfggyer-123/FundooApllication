using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace Fundoo_Application_6vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollabBusiness _collabBusiness;
        private readonly FundooContext _fundooContext;
        public CollaboratorController(ICollabBusiness collabBusiness,FundooContext fundooContext)
        {
            this._collabBusiness = collabBusiness;
            this._fundooContext = fundooContext;
        }

        [HttpPost("Add Collborator")]
        [Authorize]
        public IActionResult addCollaborator(long noteid, string collabortorEmail)
        {
            try
            {
                long Userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                var result = _collabBusiness.addCollaborator(Userid, noteid, collabortorEmail);

                if (result != null)
                {
                    return Ok(new { sucess = true, messgae = "Collaborator Created SuccessFully", data = result });

                }
                else
                {
                    return BadRequest(new { success = false, message = "Collaborator Not added", Data = result });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteCollaborator")]
        [Authorize]
        public IActionResult DeleteCollaborator(long noteid, long CollaboratorId)
        {
            try
            {
                long Userid = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);


                var result = _collabBusiness.DeleteCollaborator(Userid, noteid, CollaboratorId);


                if (result != null)
                {
                    return Ok(new { success = true, message = "Collborator Deleted Successfully", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Collborator not deleted", data = result });

                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByUserId")]
        [Authorize]
        public ActionResult GetAlllable()
        {
            long userId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            IEnumerable<CollabEntity> resutl = _collabBusiness.GetCollaborators(userId);
            if (resutl != null)
            {

                return Ok(new { success = true, message = " Date return sucessfully", Data = resutl });
            }
            else
            {

                return BadRequest(new { success = false, message = "getAllData Failed", Data = resutl });
            }
        }

        [HttpGet("GetByNoteID")]
        [Authorize]
        public IActionResult GetCollaboratorsByNoteId(long noteId)
        {


            long userId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            IEnumerable<CollabEntity> resutl = _collabBusiness.GetCollaboratorsByNoteId(userId, noteId);
            int count = _fundooContext.Collab.Where(x => x.UserId == userId && x.NoteId == noteId).Count();

            if (resutl != null)
            {
                var response = new
                {
                    User = resutl,
                    TotalNoteID = count
                };
                return Ok(new  { success = true, message = " Date return sucessfully", Data = response });
            }else
            {
                return BadRequest(new  { success = false, message = "Failed getting data by noteID", Data = resutl });

            }
            try
            {

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }





    }
}
