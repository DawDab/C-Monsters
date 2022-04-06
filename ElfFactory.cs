
using Game.Engine.Monsters.MonsterPackByDD.Elf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.MonsterFactories
{
    [Serializable]
    class ElfFactory : MonsterFactory
    {
        private int encounterNumber = 0;
        public override Monster Create()
        {
            if (encounterNumber == 0)
            {
                encounterNumber++;
                return new Elf();
            }
       
            else return null;
       
        }
        public override System.Windows.Controls.Image Hint()
        {
            if (encounterNumber == 0) return new Elf().GetImage();
            else return null;
        }
    }
}
