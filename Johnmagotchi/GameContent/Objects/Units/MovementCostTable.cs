using Johnmagotchi.Core.tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Johnmagotchi.GameContent.Units.UnitStatBlock;

namespace Johnmagotchi.GameContent.Objects.Units
{
    public static class MovementCostTable
    {
        public static int getMoveCost(MovementType moveType, TileType tileType) {
            switch (moveType) {   
                case MovementType.FOOT:
                    return FootMovement(tileType); 
                case MovementType.WHEELS:
                    return WheelMovement(tileType);
                case MovementType.HORSE:
                    return HorseMovement(tileType);
                case MovementType.FLYING:
                    return FlyingMovement(tileType);
                default:
                    return FootMovement(tileType);
            }
        }

        private static int FootMovement(TileType tileType) {
            switch (tileType) {
                case TileType.GRASS:
                case TileType.ROAD:
                case TileType.BASE:
                case TileType.CITY:
                case TileType.HQ:
                    return 1;
            
                case TileType.RIVER:
                case TileType.MOUNTAIN:
                    return 2;
               
                case TileType.SEA:
                    return 9999;
                default:
                    return 9999;
            }
        }
        private static int WheelMovement(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.GRASS:
                case TileType.ROAD:
                case TileType.BASE:
                case TileType.CITY:
                case TileType.HQ:
                    return 1;

                case TileType.RIVER:
                case TileType.MOUNTAIN:
                    return 2;

                case TileType.SEA:
                    return 9999;
                default:
                    return 9999;
            }
        }
        private static int HorseMovement(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.GRASS:
                case TileType.ROAD:
                case TileType.BASE:
                case TileType.CITY:
                case TileType.HQ:
                    return 1;

                case TileType.RIVER:
                case TileType.MOUNTAIN:
                    return 2;

                case TileType.SEA:
                    return 9999;
                default:
                    return 9999;
            }
        }

        private static int FlyingMovement(TileType tileType)
        {

            switch (tileType)
            {
                case TileType.GRASS:
                case TileType.ROAD:
                case TileType.BASE:
                case TileType.CITY:
                case TileType.HQ:
                case TileType.RIVER:
                case TileType.MOUNTAIN:
                case TileType.SEA:
                    return 1;
                default:
                    return 9999;
            }
        }

    }
}
