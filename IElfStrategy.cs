using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.MonsterPackByDD.Elf
{
    interface IElfStrategy
    {
        List<StatPackage> BattleMove(Elf monster);
    
    }
}
