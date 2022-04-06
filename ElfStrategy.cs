using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.MonsterPackByDD.Elf
{
    
    class RestoreHealthStrategy : IElfStrategy
    {
        public List<StatPackage> BattleMove(Elf monster)
        {
            monster.Stamina -= 5;
            monster.Health += 100;
            monster.HealthPotions -= 1;
            return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Elf uzywa magicznej mikstury przywracajac sobie zdrowie.") };
        }
    }

    class RestoreStaminaStrategy : IElfStrategy
    {
        public List<StatPackage> BattleMove(Elf monster)
        {
            monster.Stamina += 30;
            return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Elf odpoczywa.") };
        }
    }

    class SingleShot : IElfStrategy
    {
        public List<StatPackage> BattleMove(Elf monster)
        {
            if (Index.RNG(0, 100) < monster.Precision)
            {
                monster.Stamina -= 20;
                return new List<StatPackage>() { new StatPackage(DmgType.Physical, 30 + monster.Strength, "Elf strzela do ciebie z luku i trafia! (" + (30 + monster.Strength) + " dmg [fizyczne])") };
            }
            else
            {
                monster.Stamina -= 20;
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Elf strzela do ciebie z luku i nie trafia! ") };
            }
        }
    }

    class DoubleShotStrategy : IElfStrategy
    {
        public List<StatPackage> BattleMove(Elf monster)
        {
            if (Index.RNG(0, 100) < monster.Precision)
            {
                monster.Stamina -= 40;
                return new List<StatPackage>() { new StatPackage(DmgType.Physical, 60 + monster.Strength, "Elf wystrzeliwuje dwie strzaly i trafia! (" + (60 + monster.Strength) + " dmg [fizyczne])") };
            }
            else
            {
                monster.Stamina -= 40;
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Elf wystrzeliwuje dwie strzaly i nie trafia! ") };
            }
        }
    }

    class CriticalShotStrategy : IElfStrategy
    {
        public List<StatPackage> BattleMove(Elf monster)
        {
            if (Index.RNG(0, 100) < 50)
            {
                monster.Stamina -= 50;
                return new List<StatPackage>() { new StatPackage(DmgType.Physical, 100 + monster.Strength, "Elf strzela wykonujac strzal krytyczny i trafia! (" + (100 + monster.Strength) + " dmg [fizyczne])") };
            }
            else
            {
                monster.Stamina -= 50;
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Elf strzela wykonujac strzal krytyczny i nie trafia! ") };
            }
        }
    }

    class DarkArrow : IElfStrategy
    {
        public List<StatPackage> BattleMove(Elf monster)
        {
            if (Index.RNG(0, 100) < monster.Precision)
            {
                monster.Stamina -= 50;
                return new List<StatPackage>() { new StatPackage(DmgType.Psycho, 80 + monster.MagicPower, 0, 0, 10, 10, "Mroczny Elf wystrzeliwuje mroczny pocisk i trafia! (" + (80 + monster.MagicPower) + " dmg [magiczne])") };
            }
            else
            {
                monster.Stamina -= 50;
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Mroczny Elf wystrzeliwuje mroczny pocisk i nie trafia! ") };
            }
        }
    }

    class PoisonedSwordAttack : IElfStrategy
    {
        public List<StatPackage> BattleMove(Elf monster)
        {
            monster.Stamina -= 20;

            return new List<StatPackage>()
            {
                new StatPackage(DmgType.Physical, 30 + monster.Strength, "Mroczny Elf zadaje cios mieczem! (" + (30 + monster.Strength) + " dmg [fizyczne])"),
                new StatPackage(DmgType.Poison, 20 , "Miecz pokryty jest trucizna! (" + 20 + " dmg [trucizna])")
            };



        }
    }

    class Blindness : IElfStrategy
    {
        public List<StatPackage> BattleMove(Elf monster)
        {
            monster.Stamina -= 35;
            return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, 0, 0, 10, 0, "Mroczny Elf rzuca czar oślepienia! (" + 10 + " dmg [precyzja])") };

        }
    }

    class DarkBlow : IElfStrategy
    {
        public List<StatPackage> BattleMove(Elf monster)
        {
            if (Index.RNG(0, 100) < monster.Precision)
            {
                monster.Stamina -= 50;
                return new List<StatPackage>() 
                { 
                    new StatPackage(DmgType.Psycho, 80 + (monster.MagicPower+monster.Strength)/2,"Mroczny Elf wykonuje mroczne uderzenie! (" + (80 + monster.MagicPower) + " dmg [magiczne])"),
                    new StatPackage(DmgType.Other, 0,10,0,0,0 ,"Mroczna moc potwora odbiera Ci sile! (" + 10 + " debuff [sila])")
                };
            }
            else
            {
                monster.Stamina -= 50;
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Mroczny Elf nie trafia mrocznym uderzeniem! ") };
            }
        }
    }
    class ArmorPenetration : IElfStrategy
    {
        public List<StatPackage> BattleMove(Elf monster)
        {
            monster.Stamina -= 35;
            return new List<StatPackage>() { new StatPackage(DmgType.Other, 0,0,10,0,0, "Mroczny Elf rzuca czar zmiejszajacy twoj pancerz! (" + 10 + " debuff[pancerz])") };

        }
    }

}
