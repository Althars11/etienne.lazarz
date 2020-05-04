namespace WonderlandTycoon
{
    public class Shop : Building
    {
        public const long BUILD_COST = 300;
        public static readonly long[] UPGRADE_COST = { 2500, 10000, 50000 };
        public static readonly long[] INCOME = {7, 8, 9, 10};
        private static int lvl;

        public Shop()
        { 
            type = BuildingType.SHOP;
            lvl = Lvl;
        }

        public long Income(long population)
        {
            return INCOME[lvl];
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