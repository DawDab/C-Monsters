using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.MonsterPackByDD.Knight
{
    [Serializable]
    class Knight : Monster
    {
        public IKnightStrategy currentStrategy;

        protected int fury;
        public virtual int Fury
        {
            get { return fury; }
            set
            {
                if (value < 0) fury = 0;
                else fury = value;
            }
        }

        protected int riposte;
        public virtual int Riposte
        {
            get { return riposte; }
            set
            {
                if (value < 0) riposte = 0;
                else riposte = value;
            }
        }
        public Knight()
        {
            Riposte = 0;
            Fury = 0;
            Health = 350;
            Strength = 90;
            Armor = 150;
            Precision = 60;
            Stamina = 400;
            XPValue = 200;
            Name = "monster0010";
            BattleGreetings = "Stawaj do walki slabeuszu!";
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
                if (Riposte == 1) { pack.HealthDmg = 0; pack.StrengthDmg = 0; pack.ArmorDmg = 0; pack.PrecisionDmg = 0; pack.PrecisionDmg = 0; pack.MagicPowerDmg = 0; }
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
            Riposte = 0;
            if (Fury > 100) { Fury = 100; }
            if (Stamina > 0)
            {
                if (DmgTest.Magic(ans[ans.Count - 1].DamageType)) //Strategia obierana gdy otrzymuje obrazenia magiczne.
                {
                    int chance = Index.RNG(0, 10);
                    if (Stamina >= 50 && chance == 9)
                    {
                        currentStrategy = new Execute(); //Egzegucja (premia od furii)
                    }
                    else if (Stamina >= 20 && (chance < 3 && chance >= 0))
                    {
                        currentStrategy = new Blootthrist();//Krwiopijec (przywrocenie zycia)
                    }
                    else if (Stamina >= 40 && (chance < 6 && chance >= 3))
                    {
                        currentStrategy = new BarbarousStrike();//Barbarzyńskie uderzenie
                    }
                    else if (Stamina >= 30 && (chance <= 8 && chance > 6))
                    {
                        if (Fury == 0) { currentStrategy = new BarbarousStrike(); }
                        else { currentStrategy = new BattleCry(); } //Bitewny okrzyk (dodaje punkty furii do sily)
                    }
                    else
                    {
                        currentStrategy = new RestoreStaminaStrategy();
                    }

                }
                else if (DmgTest.Physical(ans[ans.Count - 1].DamageType))//Strategia obierana gdy otrzymuje obrazenia fizyczne.
                {
                    int chance = Index.RNG(0, 11);
                    if (Stamina >= 50 && chance == 9)
                    {
                        currentStrategy = new Execute();
                    }
                    else if (Stamina >= 20 && (chance < 3 && chance >= 0))
                    {
                        currentStrategy = new Blootthrist();
                    }
                    else if (Stamina >= 40 && (chance < 6 && chance >= 3))
                    {
                        currentStrategy = new BarbarousStrike();
                    }
                    else if (Stamina >= 30 && (chance < 9 && chance >= 6) )
                    {
                        if (Fury == 0) { currentStrategy = new BarbarousStrike(); }
                        else { currentStrategy = new BattleCry(); }
                    }
                    else if (Stamina >= 30 && chance == 10 )
                    {
                        currentStrategy = new Riposte();
                    }
                    else
                    {
                        currentStrategy = new RestoreStaminaStrategy();
                    }
                }
                else
                {
                    int chance = Index.RNG(0, 11);
                    if (Stamina >= 50 && chance == 9)
                    {
                        currentStrategy = new Execute();
                    }
                    else if (Stamina >= 20 && (chance < 3 && chance >= 0))
                    {
                        currentStrategy = new Blootthrist();
                    }
                    else if (Stamina >= 40 && (chance < 6 && chance >= 3))
                    {
                        currentStrategy = new BarbarousStrike();
                    }
                    else if (Stamina >= 30 && (chance < 9 && chance >= 6))
                    {
                        if (Fury == 0) { currentStrategy = new BarbarousStrike(); }
                        else { currentStrategy = new BattleCry(); }
                    }
                    else if (Stamina >= 30 && chance == 10)
                    {
                        currentStrategy = new Riposte();
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
