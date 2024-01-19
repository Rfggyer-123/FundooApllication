using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    public class NoteEntityRepoServicecs : INoteEntityRepo
    {
        private readonly FundooContext _Context;

        public NoteEntityRepoServicecs(FundooContext _context)
        {
            this._Context = _context;
        }

       
        public bool AddNote(NoteRequestcs model, long Userid)
        {
            if (model == null)
            {

                return false;
            }

            NoteEntity note = new NoteEntity();
            //  note.NoteID = model.NoteID;
            note.Title = model.Title;
            note.Description = model.Description;
            note.IsArchieve = model.IsArchieve;
            note.IsTrash = model.IsTrash;
            note.IsPin = model.IsPin;
            note.Reminder = model.Reminder;
            note.Color = model.Color;
            note.Image = model.Image;//new UploadImage().UploadImageMethod(model.Image, model.Title, Userid);//--work is pending
            note.UserId = Userid;
            note.CreatedAt=DateTime.Now;    
            note.ModifiedAt=DateTime.Now;
            _Context.Notes.Add(note);
            _Context.SaveChanges();
             return true;
         
        }


        public List<NoteEntity> GetAllNotes(long Userid)
        {
            // Retrieve all notes for the given UserId
            var totalNote = _Context.Notes.Where(u => u.UserId == Userid).ToList();

            // Check if any notes are found
            if (!totalNote.Any())
            {
                return new List<NoteEntity>(); // Return an empty list
            }

            return totalNote;
        }

        public string DeleteNote(long UserId,long NoteID)
        {
          //  int UserId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);//used for verifing token
            var deleteNote = _Context.Notes.FirstOrDefault(u => u.id == NoteID);
            if (deleteNote == null)
            {
              return null;
            }
            _Context.Notes.Remove(deleteNote);
            _Context.SaveChanges();
            return "NoteEntity Deleted Successfully With NOteID " + deleteNote.id;
        }

        public string UpdateNote(NoteRequestcs model, long Userid)
        {
            if (model == null)
            {
                return null;
            }
            NoteEntity note = new NoteEntity();
            note.Title = model.Title;
            note.Description = model.Description;
            note.IsArchieve = model.IsArchieve;
            note.IsTrash = model.IsTrash;
            note.IsPin = model.IsPin;
            note.Reminder = model.Reminder;
            note.Color = model.Color;
            note.Image = model.Image;//new UploadImage().UploadImageMethod(model.Image, model.Title, Userid);//--work is pending
            note.UserId = Userid;
          //  note.CreatedAt = DateTime.Now;
            note.ModifiedAt = DateTime.Now;
            _Context.SaveChanges();
            return "Note Upated SuccessFully";

        }

        public NoteEntity GetNoteById(long Userid, long NoteId)
        {
            // Retrieve all notes for the given UserId
            var userNotes = _Context.Notes.Where(u => u.UserId == Userid).ToList();

            // Check if any notes are found
            if (userNotes == null || !userNotes.Any())
            {
                return null; // No notes found for the user
            }

            // Find the specific note by NoteId
            var note = userNotes.FirstOrDefault(n => n.id == NoteId);

            return note;
        }

        public NoteEntity DeleteNoteById(long UserId, long NoteId)
        {

            var userNotes = _Context.Notes.Where(u => u.UserId == UserId).ToList();

            // Check if any notes are found
            if (userNotes == null || !userNotes.Any())
            {
                return null ; // No notes found for the user
            }
            // Find the specific note by NoteId
            var note = userNotes.FirstOrDefault(n => n.id == NoteId);

            _Context.Notes.Remove(note);
            _Context.SaveChanges();
            return note;


        }

        public NoteEntity IsNoteExist(long UserId, long NoteId)
        {

            var userNotes = _Context.Notes.Where(u => u.UserId == UserId).ToList();

            // Check if any notes are found
            if (userNotes == null || !userNotes.Any())
            {
                return null; // No notes found for the user
            }
            // Find the specific note by NoteId
            var note = userNotes.FirstOrDefault(n => n.id == NoteId);

            if (note != null)
            {
                return note;
            }
            else
            {
                return note;
            }

        }

        public NoteEntity ToggleTrash(long UserId, long NoteId)
        {

            var userNotes = _Context.Notes.Where(u => u.UserId == UserId).ToList();

            // Check if any notes are found
            if (userNotes == null || !userNotes.Any())
            {
                return null; // No notes found for the user
            }

            // Find the specific note by NoteId
            var note = userNotes.FirstOrDefault(n => n.id == NoteId);


            if (note != null)
            {
                if (note.IsPin == true)
                {
                    note.IsPin = false;
                }

                if (note.IsArchieve == true)
                {
                    note.IsArchieve = false;
                }

                // Toggle the IsTrash property
                 note.IsTrash = !note.IsTrash;
                note.ModifiedAt = DateTime.UtcNow;
                _Context.SaveChanges();
                 return note;
            }
            else 
            {
                return null;
            }

        }

        public NoteEntity TogglePin(long UserId, long NoteId)
        {
            var userNotes = _Context.Notes.Where(u => u.UserId == UserId).ToList();

            // Check if any notes are found
            if (userNotes == null || !userNotes.Any())
            {
                return null; // No notes found for the user
            }

            // Find the specific note by NoteId
            var note = userNotes.FirstOrDefault(n => n.id == NoteId);

            if (note != null)
            {
                // Toggle the IsPin property
                note.IsPin = !note.IsPin;

                // Check if IsPin property has changed before modifying state
                if (_Context.Entry(note).Property("IsPin").IsModified)
                {
                    _Context.Entry(note).State = EntityState.Modified;
                    note.ModifiedAt = DateTime.UtcNow;
                    _Context.SaveChanges();
                }

                return note;
            }
            else
            {
                return null;
            }
        }

        public NoteEntity ToggleArchive(long UserId, long NoteId)
        {
            var userNotes = _Context.Notes.Where(u => u.UserId == UserId).ToList();

            // Check if any notes are found
            if (userNotes == null || !userNotes.Any())
            {
                return null; // No notes found for the user
            }

            // Find the specific note by NoteId
            var userArchive = userNotes.FirstOrDefault(n => n.id == NoteId);

            
                if (userArchive != null)
                {
                    if (userArchive.IsArchieve == true)
                    {
                        if (userArchive.IsPin == true)
                        {
                            userArchive.IsPin = false;
                        }

                        if (userArchive.IsTrash == true)
                        {
                            userArchive.IsTrash = false;
                        }
                        userArchive.IsArchieve = false;
                        _Context.Entry(userArchive).State = EntityState.Modified;
                        userArchive.ModifiedAt = DateTime.UtcNow;
                        _Context.SaveChanges();
                        return userArchive;
                    }
                    else
                    {
                        if (userArchive.IsPin == true)
                        {
                            userArchive.IsPin = false;
                        }

                        if (userArchive.IsTrash == true)
                        {
                            userArchive.IsTrash = false;
                        }

                        userArchive.IsArchieve = true;
                        _Context.Entry(userArchive).State = EntityState.Modified;
                         userArchive.ModifiedAt = DateTime.UtcNow;
                        _Context.SaveChanges();
                        return userArchive;
                    }
                }
                else
                {
                    return userArchive;
                }
        }

        public NoteEntity AddColor(long UserId, long NoteId, string color)
        {

            var userNotes = _Context.Notes.Where(u => u.UserId == UserId).ToList();

            // Check if any notes are found
            if (userNotes == null || !userNotes.Any())
            {
                return null; // No notes found for the user
            }

            // Find the specific note by NoteId
            var note = userNotes.FirstOrDefault(n => n.id == NoteId);

            if (note != null)
            {
                note.Color = color;
                note.ModifiedAt= DateTime.UtcNow;
                _Context.Entry(note).State = EntityState.Modified;
                _Context.SaveChanges();
                return note;
            }
            else
            {
                return note;
            }
        }

        public NoteEntity AddImage(long UserId, long noteId, IFormFile Image)
        {
            var userNotes = _Context.Notes.Where(u => u.UserId == UserId).ToList();

            // Check if any notes are found
            if (userNotes == null || !userNotes.Any())
            {
                return null; // No notes found for the user
            }

            // Find the specific note by NoteId
            var note = userNotes.FirstOrDefault(n => n.id == noteId);

            if (note!=null)
            {
                Account account = new Account("dd2pal6jj", "715731728516283", "5WuFtsDfAMASxfCSGRTqZbE4lqE");
                Cloudinary cloudinary = new Cloudinary(account);

                var uploadParameters = new ImageUploadParams()
                {
                    File = new FileDescription(Image.FileName, Image.OpenReadStream())
                };

                var uploadResult = cloudinary.Upload(uploadParameters);
                string ImagePath = uploadResult.Url.ToString();
                note.Image = ImagePath;
                note.ModifiedAt = DateTime.UtcNow;
                _Context.Entry(note).State = EntityState.Modified;
                
                _Context.SaveChanges();
                return note;

            }
            else
            {
                return null;
            }
        }

        public NoteEntity AddReminder(long UserId, long noteId, DateTime reminder)
        {
            var userNotes = _Context.Notes.Where(u => u.UserId == UserId).ToList();

            // Check if any notes are found
            if (userNotes == null || !userNotes.Any())
            {
                return null; // No notes found for the user
            }

            // Find the specific note by NoteId
            var note = userNotes.FirstOrDefault(n => n.id == noteId);

            if (note != null)
            { 
                DateTime dateTime = DateTime.Now;
                if(reminder > dateTime)
                {
                    note.Reminder= reminder;
                    note.ModifiedAt = DateTime.UtcNow;
                    _Context.Entry(note).State = EntityState.Modified;
                    _Context.SaveChanges();
                    return note;
                }else
                {
                    return null;
                }

            }
            else
            {
                return note;
            }


        }
    }
}
