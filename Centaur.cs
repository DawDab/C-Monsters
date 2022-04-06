using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.MonsterPackByDD.Centaur
{
    [Serializable]
    class Centaur : Monster
    {
        public ICentaurStrategy currentStrategy;
        protected int armourPotions;
        public virtual int ArmourPotions
        {
            get { return armourPotions; }
            set
            {
                if (value < 0) armourPotions = 0;
                else armourPotions = value;
            }
        }

        protected int magicRune;
        public virtual int MagicRune
        {
            get { return magicRune; }
            set
            {
                if (value < 0) magicRune = 0;
                else magicRune = value;
            }
        }
        public Centaur()
        {
            currentStrategy = new AxeSlash();
            ArmourPotions = 2;
            MagicRune = 1;
            Health = 300;
            Strength = 100;
            Armor = 80;
            Precision = 60;
            Stamina = 400;
            XPValue = 200;
            Name = "monster0009";
            BattleGreetings = "ihahahaha!";
        }
        public override List<StatPackage> BattleMove()
        {
            return currentStrategy.BattleMove(this);
        }

        public override List<StatPackage> React(List<StatPackage> packs)
        {
            List<StatPackage> ans = new List<StatPackage>();
            foreach (StatPackage pack in packs)
            {   //redukcje obrazen przez pancerz (różne dla ataku magicznego i fizycznego)
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
                if (DmgTest.Magic(ans[ans.Count - 1].DamageType)) //Strategie obierane gdy otrzymuje obrazenia magiczne.
                {
                    int chance = Index.RNG(0, 10); //random number generator (wprowadzenie losowości) 
                    if (Stamina >= 20 && chance == 9 )
                    {
                        if (MagicRune > 0) { currentStrategy = new MagicReductionRune(); } //Obnizenie mocy przeciwnika (jedno uzycie)
                        else { currentStrategy = new StrenghtBuff(); }
                        
                    }
                    else if (Stamina >= 20 && (chance < 3 && chance >= 0))
                    {
                        currentStrategy = new AxeSlash(); //Podstawowy atak 
                    }
                    else if (Stamina >= 40 && (chance < 6 && chance >= 3))
                    {
                        currentStrategy = new Charge(); //Szarża (zmniejsza pancerz przeciwnika)
                    }
                    else if (Stamina >= 5 && (chance < 9 && chance >= 6))
                    {
                        if (ArmourPotions > 0) { currentStrategy = new ArmourPotions(); }//Zwiekszenie pancerza (dwa uzycia)
                        else { currentStrategy = new AxeSlash(); } //Gdy nie ma mikstur atakuje normalnie
                    }
                    else
                    {
                        currentStrategy = new RestoreStaminaStrategy();//gdy nie ma staminy na wykonanie czynnosci odpoczywa regenerujac sie
                    }

                }
                else if (DmgTest.Physical(ans[ans.Count - 1].DamageType))//Strategia obierana gdy otrzymuje obrazenia fizyczne.
                {
                    int chance = Index.RNG(0, 10); //random number generator (wprowadzenie losowości) 
                    if (Stamina >= 10 && chance == 9)
                    {
                        currentStrategy = new StrenghtBuff(); //Buff do sily
                    }
                    else if (Stamina >= 20 && (chance < 3 && chance >= 0))
                    {
                        currentStrategy = new AxeSlash(); //Podstawowy atak
                    }
                    else if (Stamina >= 40 && (chance < 6 && chance >= 3))
                    {
                        currentStrategy = new Charge(); //Szarża (zmniejszenie pancerza)
                    }
                    else if (Stamina >= 5 && (chance < 9 && chance >= 6))
                    {
                        if (ArmourPotions > 0) { currentStrategy = new ArmourPotions(); }//Zwiekszenie pancerza (dwa uzycia)
                        else { currentStrategy = new AxeSlash(); }//Gdy nie ma mikstur atakuje normalnie
                    }
                    else
                    {
                        currentStrategy = new RestoreStaminaStrategy();//gdy nie ma staminy na wykonanie czynnosci odpoczywa regenerujac sie
                    }
                }
                else //inny typ obrazen (stategie jak dla obrazen fizycznych)
                {
                    int chance = Index.RNG(0, 10); 
                    if (Stamina >= 10 && chance == 9)
                    {
                        currentStrategy = new StrenghtBuff(); //Buff do sily
                    }
                    else if (Stamina >= 20 && (chance < 3 && chance >= 0))
                    {
                        currentStrategy = new AxeSlash(); 
                    }
                    else if (Stamina >= 40 && (chance < 6 && chance >= 3))
                    {
                        currentStrategy = new Charge(); 
                    }
                    else if (Stamina >= 5 && (chance < 9 && chance >= 6))
                    {
                        if (ArmourPotions > 0) { currentStrategy = new ArmourPotions(); }
                        else { currentStrategy = new AxeSlash(); }
                    }
                    else
                    {
                        currentStrategy = new RestoreStaminaStrategy();
                    }
                }
            }
            else
            {
                currentStrategy = new RestoreStaminaStrategy();//Przywrocenie staminy
            }
            return ans;
        }
    }
}

