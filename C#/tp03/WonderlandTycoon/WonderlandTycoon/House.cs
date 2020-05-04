namespace WonderlandTycoon
{
    public class House : Building
    {
        public const long BUILD_COST = 250;
        public static readonly long[] UPGRADE_COST ={ 750, 3000, 10000 };
        public static readonly long[] HOUSING = {300, 500, 650, 750};
        private static int lvl;
        
        public House()
        {
            type = BuildingType.HOUSE;
            lvl = Lvl;
        }
        
        public long Housing()
        {
            return HOUSING[lvl];
        }


        public bool Upgrade(ref long money)
        {
            if (money>=UPGRADE_COST[lvl]&& lvl<2)
            {
                money -= (int)UPGRADE_COST[lvl];
                lvl += 1;
                return true;
            }

            return false;
        }
        
        
        public int Lvl
        {
            get { return lvl; }
        }
    }
}