using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Commands
{
    public abstract class BasicCommand
    {
        public abstract void Execute();
        public abstract bool CanExecute();
    }
}
