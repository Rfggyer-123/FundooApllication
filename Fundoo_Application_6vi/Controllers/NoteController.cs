using BusinessLayer.Interfaces;
using CommonLayer.model;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using System.Drawing;
using System.Text;




//NoteEntity NotesSave(long UserId, NotesModel notesModel);
//IEnumerable<NoteEntity> getAllData();
//NoteEntity GetByid(int Id);
//IEnumerable<NoteEntity> GetByUserid(int Id);
//NoteEntity UpdateNotes(int userId, int noteId, NotesModel notesModel);
//NoteEntity DeleteById(int Id, int noteId);
//NoteEntity IsNoteExist(int userId, int noteId);
//NoteEntity ToggleTrash(int userId, int noteId);
//NoteEntity TogglePin(int userId, int noteId);
//NoteEntity ToggleArchive(int userId, int noteId);

//NoteEntity AddColor(int userId, int noteId, string color);

//NoteEntity AddReminder(int userId, int noteId, DateTime reminder);

//NoteEntity AddImage(int userId, int noteId, IFormFile Image);
//List<NoteEntity> getAllDatas();
//IEnumerable<NoteEntity> getbyDate(int userid, DateTime date);









namespace Fundoo_Application_6vi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteEntityBusiness noteEntityBusiness;
        private string keyname = "guest";
        private readonly IDistributedCache _distributedCache;
        public NoteController(INoteEntityBusiness noteEntityBusiness, IDistributedCache _distributedCache)
        {
            this.noteEntityBusiness = noteEntityBusiness;
            this._distributedCache = _distributedCache;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddNote(NoteRequestcs noteModel)
        {
            try
            {
                long UserId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
                bool result = noteEntityBusiness.AddNote(noteModel, UserId);

                if (result == true)
                {
                    return Ok(new { sucess = result, messgae = "Note Created SuccessFully", data = noteModel });
                }
                else
                {
                    return BadRequest(new { sucess = result, messgae = "Note Mot Created  ", data = "Failure" });
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        //[HttpGet]
        //[Authorize]
        //public async Task<IActionResult> GetAllNotesAsync()
        //{
        //    try
        //    {
        //        string serializelist = string.Empty;
        //        var EncodedList = await _distributedCache.GetAsync(keyname);
        //        if (EncodedList != null)
        //        {

        //            serializelist = Encoding.UTF8.GetString(EncodedList);
        //            var Data = JsonConvert.DeserializeObject<List<NoteEntity>>(serializelist);
        //            return Ok(new { data = Data });
        //        }
        //        else
        //        {
        //            long UserId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);


        //         //   long UserId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);


        //            var result = noteEntityBusiness.GetAllNotes(UserId);

        //            if (result != null)
        //            {
        //                serializelist = JsonConvert.SerializeObject(result);
        //                EncodedList = Encoding.UTF8.GetBytes(serializelist);
        //                var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)).SetAbsoluteExpiration(TimeSpan.FromHours(6));
        //                await _distributedCache.SetAsync(keyname, EncodedList, option);

        //                return Ok(new { success = true, message = "User have list of Notes", data = result });
        //            }
        //            else
        //            {

        //                return BadRequest(new { sucess = false, messgae = "No Notes Found  ", data = result });

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpGet("GetAllNotes")]
        [Authorize]
        public IActionResult GetAllNotesAsync()
        {
            try
            {
                long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);

                var result = noteEntityBusiness.GetAllNotes(userId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "User has a list of Notes", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "No Notes Found", data = result });
                }

            }
            catch (Exception ex)
            {

                return BadRequest(new { success = false, message = "An unexpected error occurred", data = ex.Message });
            }
        }


        [HttpDelete("DeleteNote")]
        [Authorize]
        public IActionResult DeleteNote(long NoteId)
        {
            try
            {

                long UserId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
                var result = noteEntityBusiness.DeleteNote(UserId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = result });
                } else
                {
                    return BadRequest(new { success = false, message = "No Notes To Delete" });

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("UpdateNote")]
        [Authorize]
        public IActionResult UpdateNote(NoteRequestcs note)
        {
            try
            {
                long UserId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
                var Result = noteEntityBusiness.UpdateNote(note, UserId);

                if (Result != null)
                {
                    return Ok(new { success = true, result = Result });
                }
                else
                {
                    return BadRequest(new { success = false, message = Result });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetByID")]
        [Authorize]
        public IActionResult GetNoteById(long noteId)
        {
            try
            {
                long UserId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                var result = noteEntityBusiness.GetNoteById(UserId, noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Is Avialible ", result = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note Is Not Avialible ", result = result });

                }
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteNoteById")]
        [Authorize]
        public IActionResult DeleteNoteById(long noteId)
        {
            try
            {
                long UserId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                var result = noteEntityBusiness.DeleteNoteById(UserId, noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note deleted Successfully ", result = result.id });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note is  Not  Avialible", result = result });
                }
            }

            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("IsNoteExist")]
        [Authorize]
        public IActionResult IaNoteExist(long noteId)
        {
            try
            {
                long UserId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                var result = noteEntityBusiness.IsNoteExist(UserId, noteId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Is Exist", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note Is Not Exist", data = result });

                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ToggleTrash")]
        [Authorize]
        public IActionResult ToggleTrash(long noteId)
        {
            try
            {
                long UserId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                var result = noteEntityBusiness.ToggleTrash(UserId, noteId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Toggle Trash Updated", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Toggle Trash  Is Notupdated ", data = result });

                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPatch("TogglePin")]
        [Authorize]
        public IActionResult TogglePin(long noteId)
        {
            try
            {
                long UserId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                var result = noteEntityBusiness.TogglePin(UserId, noteId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Toggle PIN Updated", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Toggle PIN  Is Notupdated ", data = result });

                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPut("ToggleArchive")]
        [Authorize]
        public IActionResult ToggleArchive(long noteId)
        {
            try
            {
                long UserId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                var result = noteEntityBusiness.ToggleArchive(UserId, noteId);

                if (result != null)
                {
                    return Ok(new { success = true, message = " ToggleArchive Updated", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = " ToggleArchive Is Notupdated ", data = result });

                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPatch("AddColor")]
        [Authorize]
        public IActionResult AddColor(long noteId, String color)
        {
            try
            {
                long UserId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                var result = noteEntityBusiness.AddColor(UserId, noteId, color);
                if (result != null)
                {

                    return Ok(new { success = true, message = " Color Updated Succssfully", data = result });


                } else
                {
                    return BadRequest(new { success = false, message = "Color Not Updated", data = result });
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("AddImage")]
        [Authorize]
        public IActionResult AddImage(int noteId, IFormFile imageUrl)
        {
            try
            {
                long UserId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                var result = noteEntityBusiness.AddImage(UserId, noteId, imageUrl);
                if (result != null)
                {

                    return Ok(new { success = true, message = " Image Added Succssfully", data = result });


                }
                else
                {
                    return BadRequest(new { success = false, message = "Faliure in Ading Image", data = result });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);


            }
        }


        [HttpPatch("AddReminder")]
        [Authorize]
        public IActionResult AddReminder(int noteId, DateTime reminder)
        {
            try
            {
                long UserId = long.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                var result = noteEntityBusiness.AddReminder(UserId, noteId, reminder);
                if (result != null)
                {

                    return Ok(new { success = true, message = " Reminder Added Succssfully", data = result });


                }
                else
                {
                    return BadRequest(new { success = false, message = "Faliure in Adding Reminder", data = result });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);


            }
        }


    }
}
