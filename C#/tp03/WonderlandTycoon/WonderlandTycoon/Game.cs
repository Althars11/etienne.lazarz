namespace WonderlandTycoon
{
public class Game
    {

        public Game(string name, int nbRound, long initialMoney)
        {
            TycoonIO.GameInit(name, nbRound, initialMoney);
            //TODO
        }


        public long Launch(Bot bot)
        {
            //TODO
        }
        
        
        public void Update()
        {
            //TODO
        }


        public bool Build(int i, int j, Building.BuildingType type)
        {
            //TODO
        }
        
        public bool Upgrade(int i, int j)
        {
            //TODO
        }
        
        
        public long Money
        {
            //TODO
        }


        public int NbRound
        {
            //TODO
        }


        public int Round
        {
            //TODO
        }
        
        public long Score
        {
            //TODO
        }

        public Map Map
        {
            //TODO
        }
    }

}