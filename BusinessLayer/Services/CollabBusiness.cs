using BusinessLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollabBusiness:ICollabBusiness
    {
        private readonly ICollabRepo _collabRepo;

        public CollabBusiness(ICollabRepo collabRepo)
        {
            _collabRepo = collabRepo;
        }

        public CollabEntity addCollaborator(long userid, long noteid, string collaboratorEmail)
        {
           return _collabRepo.addCollaborator(userid, noteid, collaboratorEmail);
        }

        public CollabEntity DeleteCollaborator(long userId, long noteId, long collaboratorId)
        {
          return _collabRepo.DeleteCollaborator(userId, noteId, collaboratorId);
        }

        public IEnumerable<CollabEntity> GetCollaborators(long userId)
        {
            return _collabRepo.GetCollaborators(userId);
        }

        public IEnumerable<CollabEntity> GetCollaboratorsByNoteId(long userId, long noteId)
        {
            return _collabRepo.GetCollaboratorsByNoteId(userId, noteId);
        }
    }
}
