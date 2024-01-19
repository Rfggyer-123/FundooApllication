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


    
    public class CollabRepo : ICollabRepo
    {
        private readonly FundooContext _fundooContext;

        public CollabRepo(FundooContext fundooContext)
        {
            _fundooContext = fundooContext;
        }

        public CollabEntity addCollaborator(long userId, long noteId, string CollaboratorEmail)
        {
            var user = _fundooContext.Notes.Where(x => x.UserId == userId && x.id == noteId).FirstOrDefault();
            if (user != null)
            {
                CollabEntity entity = new CollabEntity();
                entity.UserId = userId;
                entity.NoteId = noteId;
                entity.Email = CollaboratorEmail;
                _fundooContext.Collab.Add(entity);
                _fundooContext.SaveChanges();
                return entity;
            }
            return null;
        }

        public CollabEntity DeleteCollaborator(long userId, long noteId, long collaboratorId)
        {
            var user = _fundooContext.Collab.Where(x => x.UserId == userId && x.NoteId == noteId && x.Id == collaboratorId).FirstOrDefault();
            if (user != null)
            {
                _fundooContext.Remove(user);
                _fundooContext.SaveChanges();
                return user;
            }
            return null;
        }

        public IEnumerable<CollabEntity> GetCollaborators(long userId)
        {
            IEnumerable<CollabEntity> user = _fundooContext.Collab.Where(x => x.UserId == userId).ToList();
            if (user != null)
            {
                return user.ToList();
            }
            return null;
        }

        public IEnumerable<CollabEntity> GetCollaboratorsByNoteId(long userId, long noteId)
        {
            IEnumerable<CollabEntity> user = _fundooContext.Collab.Where(x => x.UserId == userId && x.NoteId == noteId).ToList();
            if (user != null)
            {
                return user.ToList();
            }
            return null;
        }
    }
}
