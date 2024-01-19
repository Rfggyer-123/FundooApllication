using BusinessLayer.Interfaces;
using CommonLayer.model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{


    public class NoteEntityBusinessService : INoteEntityBusiness
    {
        private readonly INoteEntityRepo NoteEntityRepo;
        public NoteEntityBusinessService(INoteEntityRepo NoteEntityRepo)
        {
            this.NoteEntityRepo = NoteEntityRepo;
        }

        public bool AddNote(NoteRequestcs note, long Userid)
        {
             return NoteEntityRepo.AddNote(note, Userid);
        }
        public List<NoteEntity> GetAllNotes(long Userid)
        {
            return NoteEntityRepo.GetAllNotes(Userid);
        }

        public string DeleteNote(long UserId, long NoteID)
        {
            return NoteEntityRepo.DeleteNote(UserId, NoteID);
        }

        public string UpdateNote(NoteRequestcs note, long Userid)
        {
            return NoteEntityRepo.UpdateNote(note, Userid);
        }

        public NoteEntity GetNoteById(long Userid, long NoteId)
        {
            return NoteEntityRepo.GetNoteById(Userid, NoteId);
        }

        public NoteEntity DeleteNoteById(long UserId, long NoteId)
        {
            return NoteEntityRepo.DeleteNoteById(UserId, NoteId);
        }

        public NoteEntity IsNoteExist(long userId, long noteId)
        {
            return NoteEntityRepo.IsNoteExist(userId, noteId);
        }

        public NoteEntity ToggleTrash(long UserId, long NoteId)
        {
            return NoteEntityRepo.ToggleTrash(UserId, NoteId);
        }

        public NoteEntity TogglePin(long UserId, long NoteId)
        {
            return NoteEntityRepo.TogglePin(UserId, NoteId);
        }

        public NoteEntity ToggleArchive(long UserId, long NoteId)
        {
            return NoteEntityRepo.ToggleArchive(UserId, NoteId);    
        }

        public NoteEntity AddColor(long userId, long noteId, string color)
        {
           return NoteEntityRepo.AddColor(userId, noteId, color);
        }

        public NoteEntity AddImage(long userId, long noteId, IFormFile Image)
        {
            return NoteEntityRepo.AddImage(userId, noteId, Image);  
        }

        public NoteEntity AddReminder(long userId, long noteId, DateTime reminder)
        {
            return  NoteEntityRepo.AddReminder(userId, noteId, reminder);
        }
    }
}
