using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    public abstract class RoleAbstract
    {
        public abstract string Name { get; }
        public abstract int Money { get; set; }
        public abstract void Action(Player player, Game game);
    }
}
