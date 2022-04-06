using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.MonsterPackByDD.Elf
{
    [Serializable]
    class Elf : Monster
    {
        public IElfStrategy currentStrategy;
        protected int healthPotions;
        public virtual int HealthPotions
        {
            get { return healthPotions; }
            set
            {
                if (value < 0) healthPotions = 0;
                else healthPotions = value;
            }
        }
        public Elf()
        {
            currentStrategy = new SingleShot();
            HealthPotions = 3;
            Health = 200;
            Strength = 20;
            Armor = 50;
            Precision = 75;
            Stamina = 300;
            XPValue = 100;
            Name = "monster0007";
            BattleGreetings = "Posmakuj mojej strzaly!";
        }
        public override List<StatPackage> BattleMove()
        {
            return currentStrategy.BattleMove(this);
        }

        public override List<StatPackage> React(List<StatPackage> packs)
        {
            List<StatPackage> ans = new List<StatPackage>();
            foreach (StatPackage pack in packs)
            {
                //redukcje obrazen przez pancerz (różne dla ataku magicznego i fizycznego)
                if (DmgTest.Magic(pack.DamageType)) { Health -= 1 * (100 * pack.HealthDmg) / (100 + Armor / 2); }
                else { Health -= 1 * (100 * pack.HealthDmg) / (100 + Armor); }
                Strength -= pack.StrengthDmg;
                Armor -= pack.ArmorDmg;
                Precision -= pack.PrecisionDmg;
                MagicPower -= pack.MagicPowerDmg;
                if (DmgTest.Magic(pack.DamageType)) { pack.HealthDmg = (100 * pack.HealthDmg) / (100 + Armor / 2); }
                else { pack.HealthDmg = (100 * pack.HealthDmg) / (100 + Armor); }
                ans.Add(pack);
            }
            if (Stamina > 0)
            {   
                int chance = Index.RNG(0, 11);
                
                if(Health < 100 && HealthPotions > 0 && Stamina >5)
                {
                    currentStrategy = new RestoreHealthStrategy(); //Gdy poziom zdrowia spada ponizej 50% elf uzywa 1 z 2 mikstur leczniczych
                }
                else if((chance == 10 && Stamina > 50)||(Stamina>50 &&Health<50)) //Gdy poziom zdrowia spada ponizej 25% elf ryzukuje strzelajac krytycznie
                {
                    currentStrategy = new CriticalShotStrategy();
                }
                else if (chance > 5 && chance < 10 && Stamina > 40)
                {
                    currentStrategy = new DoubleShotStrategy();
                }
                else if (chance <= 5 && Stamina>20)
                {
                    currentStrategy = new SingleShot();
                }
                else 
                {
                    currentStrategy = new RestoreStaminaStrategy();
                }
            }
            else 
            {
                currentStrategy = new RestoreStaminaStrategy();
            }
            return ans;
        }
    }
}

