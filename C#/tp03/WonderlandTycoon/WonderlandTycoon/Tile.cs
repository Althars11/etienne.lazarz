using System;

namespace WonderlandTycoon
{
    public class Tile
    {
        public enum Biome
        {
            SEA,
            MOUNTAIN,
            PLAIN
        }
        private Biome biome; //1st attribute
        private Building _building;//2nd attribute
        
        public Tile(Biome b)
        {
            _building = null;
            b = GetBiome;
        }
        
        public bool Build(ref long money, Building.BuildingType type)
        {
            if (Biome.PLAIN == biome && type == Building.BuildingType.NONE)
            {
                switch (type)
                {
                    case Building.BuildingType.SHOP:
                        if (money>= Shop.BUILD_COST)
                        {
                            money -= Shop.BUILD_COST;
                            _building = new Shop();
                            return true;
                        }
                        break;
                    case Building.BuildingType.HOUSE:
                        if (money>= House.BUILD_COST)
                        {
                            money -= House.BUILD_COST;
                            _building = new House();
                            return true;
                        }
                        break;
                    case Building.BuildingType.ATTRACTION:
                        if (money>= Attraction.BUILD_COST)
                        {
                            money -= Attraction.BUILD_COST;
                            _building = new Attraction();
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        public bool Upgrade(ref long money)
        {
            
            switch (_building.Type)
            {
                case Building.BuildingType.SHOP:
                    if (money>= Shop.BUILD_COST)
                    {
                        money -= Shop.BUILD_COST;
                        _building = new Shop();
                        return true;
                    }
                    break;
                case Building.BuildingType.HOUSE:
                    if (money>= House.BUILD_COST)
                    {
                        money -= House.BUILD_COST;
                        _building = new House();
                        return true;
                    }
                    break;
                case Building.BuildingType.ATTRACTION:
                    if (money>= Attraction.BUILD_COST)
                    {
                        money -= Attraction.BUILD_COST;
                        _building = new Attraction();
                        return true;
                    }
                    break;
            }
            return false;
        }


        public long GetHousing()
        {
            
            if (_building!=null && _building.Type == Building.BuildingType.HOUSE)
            {
                return (_building as House).Housing();
            }

            return 0;
        }
        
        
        public long GetAttractiveness()
        {
            if (_building!=null && _building.Type == Building.BuildingType.ATTRACTION)
            {
                return (_building as Attraction).Attractiveness();
            }

            return 0;
        }
        
        
        public long GetIncome(long population)
        {
            if (_building!=null && _building.Type == Building.BuildingType.SHOP)
            {
                return (_building as Shop).Income(population);
            }

            return 0;
        }

        public Biome GetBiome
        {
            get { return biome; }
        }
    }
}