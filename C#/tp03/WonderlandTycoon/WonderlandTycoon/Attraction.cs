namespace WonderlandTycoon
{
    public class Attraction :Building
    {
        public const long BUILD_COST = 10000;
        public static readonly long[] UPGRADE_COST = { 5000, 10000, 45000 };
        public static readonly long[] ATTRACTIVENESS = { 500, 1000, 1300, 1500 };
        private static int lvl;

        public Attraction()
        {
            type = BuildingType.ATTRACTION;
            lvl = Lvl;
        }
        
        public long Attractiveness()
        {
            return ATTRACTIVENESS[lvl];
        }


        public bool Upgrade(ref int money)
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