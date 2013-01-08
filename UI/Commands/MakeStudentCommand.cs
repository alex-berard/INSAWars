using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INSAWars.Game;

namespace UI.Commands
{
    public class MakeStudentCommand : BasicCommand
    {
        private Case _case;

        public MakeStudentCommand(Case c)
        {
            _case = c;
        }

        public override void Execute()
        {
            _case.City.MakeStudent();
        }

        public override bool CanExecute()
        {
            return _case.HasCity && _case.City.CanMakeStudent();
        }
    }
}
