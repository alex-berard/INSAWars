using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INSAWars.Game;

namespace UI
{
    public class CaseSelectionEventArgs : EventArgs
    {
        private readonly Case _case;

        public CaseSelectionEventArgs(Case c)
        {
            _case = c;
        }

        public Case SelectedCase {
          get { return _case; }
        }

        public bool IsDeselection
        {
            get { return _case == null; }
        }
    }
}
