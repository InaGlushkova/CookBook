using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Helpers
{
    interface IClosableViewModel
    {
        event EventHandler CloseWindowEvent;
    }
}
