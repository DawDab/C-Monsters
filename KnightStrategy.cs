using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.MonsterPackByDD.Knight
{
    class RestoreStaminaStrategy : IKnightStrategy
    {
        public List<StatPackage> BattleMove(Knight monster)
        {
            monster.Stamina += 50;
            monster.Fury -= 5;
            return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Rycerz odpoczywa.") };
        }

    }
    
    class Blootthrist  : IKnightStrategy
    {
        public List<StatPackage> BattleMove(Knight monster)
        {
            monster.Stamina -= 30;
            monster.Health += 35;
            monster.Fury += 5;
            return new List<StatPackage>() { new StatPackage(DmgType.Physical, 30 + monster.Strength, "Upadly Rycerz zadaje cios " + (30 + monster.Strength) + " dmg [fizyczne]) Zdolność krwiopijcy pozwala mu przywrocic 35 punktow zdrowia") };
        }
    }

    class BattleCry : IKnightStrategy
    {
        public List<StatPackage> BattleMove(Knight monster)
        {
            monster.Stamina -= 30;
            monster.Strength += monster.Fury;
            monster.Fury = 0;
            return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Upadly Rycerz wykonuje krzyk bitewny wzmacniajac swoja sile")};
        }
    }
    class Execute : IKnightStrategy
    {
        public List<StatPackage> BattleMove(Knight monster)
        {
            monster.Stamina -= 50;
            monster.Fury += 5;
            return new List<StatPackage>() { new StatPackage(DmgType.Physical, 50+monster.Fury*2, "Upadly rycerz wykonuje egzekucje zadajac "+(50 + monster.Fury * 2)+" dmg [fizyczne])") };
        }
    }
    class BarbarousStrike : IKnightStrategy
    {
        public List<StatPackage> BattleMove(Knight monster)
        {
            monster.Stamina -= 40;
            monster.Fury += 5;
            return new List<StatPackage>() 
            { 
                new StatPackage(DmgType.Physical, 70 + monster.Fury , "Upadly rycerz wykonuje barbarzynskie ciecie " + (70 + monster.Fury ) + " dmg [fizyczne]) "),
                new StatPackage(DmgType.Other, 15 + monster.Strength/5 , "Twoje rany krwawia!" + (15 + monster.Strength/5) + " dmg [inne])")
            };
        }
    }

    class Riposte : IKnightStrategy
    {
        public List<StatPackage> BattleMove(Knight monster)
        {
            monster.Stamina -= 60;
            monster.Fury += 5;
            monster.Riposte = 1;
            return new List<StatPackage>()
            {
                new StatPackage(DmgType.Physical, 100 , "Upadly rycerz wykonuje pchniecie mieczem przygotowujac sie do riposty! " + (100) + " dmg [fizyczne])"),
            };
        }
    }
}


