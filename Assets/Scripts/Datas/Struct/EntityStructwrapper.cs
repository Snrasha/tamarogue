
using Assets.Scripts.Datas.Members;
using Assets.Scripts.Datas.Save;

namespace Assets.Scripts.Datas.Struct
{
    [System.Serializable]
    public struct EntityStructwrapper
    {
        public int x;
        public int y;
        public string entityname;
        public MonsterStatsStructWrapper monster;
        public EntityStructwrapper(EntitySaved entity)
        {
            this.x = entity.x;
            this.y = entity.y;
            this.entityname = entity.entityname;
            this.monster = new MonsterStatsStructWrapper(entity.monster);

        }

    }
}