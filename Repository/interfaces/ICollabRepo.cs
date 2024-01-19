using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.interfaces
{
    public interface ICollabRepo
    {
        CollabEntity addCollaborator(long userid, long noteid, String collaboratorEmail);
        CollabEntity DeleteCollaborator(long userId, long noteId, long collaboratorId);

        IEnumerable<CollabEntity> GetCollaborators(long userId);
        IEnumerable<CollabEntity> GetCollaboratorsByNoteId(long userId, long noteId);


    }
}
