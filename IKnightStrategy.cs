using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.MonsterPackByDD.Knight
{
    interface IKnightStrategy
    {
        List<StatPackage> BattleMove(Knight monster);

    }
}
