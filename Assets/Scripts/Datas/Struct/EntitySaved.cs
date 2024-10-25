
using Assets.Scripts.Datas.Members;
using Assets.Scripts.Datas.Save;

namespace Assets.Scripts.Datas.Struct
{
    public struct EntitySaved
    {
        public int x;
        public int y;
        public string entityname;
        public MonsterStats monster;

        public EntitySaved(string tama)
        {
            x = 0;
            y = 0;
            entityname = "Player";
            monster = new MonsterStats(true, tama);
        }


        public EntitySaved(EntityStructwrapper entity)
        {
            this.x = entity.x;
            this.y = entity.y;
            this.entityname = entity.entityname;
            this.monster = new MonsterStats(entity.monster);
        }

    }
}