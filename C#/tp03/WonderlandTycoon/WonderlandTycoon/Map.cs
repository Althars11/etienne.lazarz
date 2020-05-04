namespace WonderlandTycoon
{
public class Map
{
    private static Tile[,] _map;
        public Map(string name)
        {
            _map = TycoonIO.ParseMap(name);
        }

        public bool Build(int i, int j, ref long money, Building.BuildingType type)
        {
            _map[i, j].Build(ref money, type);
        }
        
        public bool Upgrade(int i, int j, ref long money)
        {
            
        }
        
        
        public long GetAttractiveness()
        {
            //TODO
        }
        
        public long GetHousing()
        {
            //TODO
        }

        public long GetPopulation()
        {
            //TODO
        }
        
        
        public long GetIncome(long population)
        {
            //TODO
        }
    }

}