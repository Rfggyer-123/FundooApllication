using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.model
{
    public class NoteRequestcs
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool IsArchieve { get; set; }
        public bool IsPin { get; set; }
        public bool IsTrash { get; set; }
      //  public DateTime CreatedAt { get; set; }

        //public DateTime ModifiedAt { get; set; }

    }
}
