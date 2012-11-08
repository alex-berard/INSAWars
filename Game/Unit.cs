using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Game
{
    public interface Unit
    {
        public virtual void NextTurn();
        public virtual void Die();
        
        // For the HashSet
        //public virtual bool Equal();
        //public virtual int GetHashCode();
    }
}
