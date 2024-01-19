using CommonLayer.model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface INoteEntityBusiness
    {
        public bool AddNote(NoteRequestcs note,long Userid);
        public List<NoteEntity> GetAllNotes(long Userid);

        public string DeleteNote(long UserId, long NoteID);

        public String UpdateNote(NoteRequestcs note,long Userid);

        public NoteEntity GetNoteById(long Userid,long NoteId);

        public NoteEntity DeleteNoteById(long UserId, long NoteId);

        public NoteEntity IsNoteExist(long userId, long noteId);

        public NoteEntity ToggleTrash(long UserId,long NoteId);

        public NoteEntity TogglePin(long UserId, long NoteId);

        public NoteEntity ToggleArchive(long UserId,long NoteId);

        public NoteEntity  AddColor(long userId, long noteId, string color);

        public  NoteEntity AddImage(long userId, long noteId, IFormFile Image);

        public NoteEntity AddReminder(long userId, long noteId, DateTime reminder);



    }
}
