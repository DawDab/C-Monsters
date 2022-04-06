using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.MonsterPackByDD.Centaur
{

    class RestoreStaminaStrategy : ICentaurStrategy
    {
        public List<StatPackage> BattleMove(Centaur monster)
        {
            monster.Stamina += 40;
            return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Centaur odpoczywa.") };
        }
    }

    class AxeSlash : ICentaurStrategy
    {
        public List<StatPackage> BattleMove(Centaur monster)
        {
            monster.Stamina -= 20;
            return new List<StatPackage>() { new StatPackage(DmgType.Physical, 30 + monster.Strength, "Centaur wykonuje ciecie toporem " + (30 + monster.Strength) + " dmg [fizyczne])") };
        }
    }

    class Charge: ICentaurStrategy
    {
        public List<StatPackage> BattleMove(Centaur monster)
        {
            monster.Stamina -= 40;
            return new List<StatPackage>() { new StatPackage(DmgType.Physical, 40 + monster.Strength,0,10,0,0, "Centaur szarzuje! " + (40 + monster.Strength) + " dmg [fizyczne]) oraz uszkadza twoj pancerz! 10 debuff[pancerz]") };
        }
    }

    class StrenghtBuff : ICentaurStrategy
    {
        public List<StatPackage> BattleMove(Centaur monster)
        {
            monster.Stamina -= 10;
            monster.Strength += 10;
            return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Centaur motywuje się do walki wzmacniajac swoja sile! 10 buff[sila]") };
        }
    }

    class ArmourPotions : ICentaurStrategy
    {
        public List<StatPackage> BattleMove(Centaur monster)
        {
            monster.ArmourPotions -= 1;
            monster.Stamina -= 5;
            monster.Armor += 10;
            return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Centaur wypija eliksir zelaznej skory! 10 buff[pancerz]") };
        }
    }

    class MagicReductionRune : ICentaurStrategy
    {
        public List<StatPackage> BattleMove(Centaur monster)
        {
            monster.MagicRune -= 1;
            monster.Stamina -= 20;
            return new List<StatPackage>() { new StatPackage(DmgType.Psycho, 0,0,0,0,30, "Centaur uzywa pradawnej runy zmiejszajac twoja sile magiczna! 30 debuff[moc]") };
        }
    }

}
