using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Viewmodels.Tattoo
{
    public class CommentBindingModel
    {
       public string CommentText { get; set; }

       public int TattooId { get; set; }
    }
}
