using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.MonsterPackByDD.Elf
{
    [Serializable]
    class DarkElf : Elf
    {
        
       
        public DarkElf()
        {
            currentStrategy = new PoisonedSwordAttack();
            HealthPotions = 2;
            Health = 300;
            Strength = 50;
            Armor = 50;
            Precision = 80;
            MagicPower = 50;
            Stamina = 300;
            XPValue = 200;
            Name = "monster0008";
            BattleGreetings = "Poznasz szybka i bezbolesna porażke!";
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
                
                if( Health<Health/2 && HealthPotions>0 && Stamina >=5)
                {
                    currentStrategy = new RestoreHealthStrategy();//Gdy poziom zdrowia spada ponizej 50% elf uzywa 1 z 2 mikstur leczniczych
                }
                else if (DmgTest.Magic(ans[ans.Count - 1].DamageType)) //Strategia obierana gdy otrzymuje obrazenia magiczne.
                {
                    int chance = Index.RNG(0, 10);
                    if (Stamina >= 50 && chance >=8) 
                    {
                        currentStrategy = new DarkArrow(); //Zmniejszenie mocy wroga
                    }
                    else if (Stamina >= 20 && (chance < 3 && chance >=0))
                    {
                        currentStrategy = new PoisonedSwordAttack(); //Zatruty Cios
                    }
                    else if (Stamina >= 40 && (chance <6  && chance >= 3))
                    {
                        currentStrategy = new DoubleShotStrategy(); //Podwojny strzal z luku
                    }
                    else if (Stamina >= 35 && (chance < 8 && chance >= 6))
                    {
                        currentStrategy = new Blindness(); //Czar oslepienia (debuff precyzji)
                    }
                    else
                    {
                        currentStrategy = new RestoreStaminaStrategy();//przywrocenie staminy
                    }
                }
                else if (DmgTest.Physical(ans[ans.Count - 1].DamageType))//Strategia obierana gdy otrzymuje obrazenia fizyczne.
                {
                    
                    int chance = Index.RNG(0, 10);
                    if (Stamina >= 50 && chance >= 8)
                    {
                        currentStrategy = new DarkBlow(); //Mroczna strzala (debuff sily)
                    }
                    else if (Stamina >= 20 && (chance < 3 && chance >= 0))
                    {
                        currentStrategy = new PoisonedSwordAttack(); //Zatruty cios
                    }
                    else if (Stamina >= 40 && (chance < 6 && chance >= 3))
                    {
                        currentStrategy = new DoubleShotStrategy(); //podwojny strzal z luku
                    }
                    else if (Stamina >= 35 && (chance < 8 && chance >= 6))
                    {
                        currentStrategy = new ArmorPenetration(); //debuff pancerza
                    }
                    else
                    {
                        currentStrategy = new RestoreStaminaStrategy(); //przywrocenie staminy
                    }
                }
                else
                {
                    int chance = Index.RNG(0, 10);
                    if (Stamina >= 50 && chance >= 8)
                    {
                        currentStrategy = new DarkBlow(); //Mroczna strzala (debuff sily)
                    }
                    else if (Stamina >= 20 && (chance < 3 && chance >= 0))
                    {
                        currentStrategy = new PoisonedSwordAttack(); //Zatruty cios
                    }
                    else if (Stamina >= 40 && (chance < 6 && chance >= 3))
                    {
                        currentStrategy = new DoubleShotStrategy(); //podwojny strzal z luku
                    }
                    else if (Stamina >= 35 && (chance < 8 && chance >= 6))
                    {
                        currentStrategy = new ArmorPenetration(); //debuff pancerza
                    }
                    else
                    {
                        currentStrategy = new RestoreStaminaStrategy(); //przywrocenie staminy
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

