using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    public class Ship
    {
        public string Cargo = null;
        public int Quantity { get { return _quantity; } }
        public int MaxCargoQuantity { get { return _maxCargoQuantity; } }

        private int _quantity;
        private int _maxCargoQuantity;
        static private List<Ship> ships = new List<Ship>();

        public Ship(int maxCargoQuantity)
        {
            _maxCargoQuantity = maxCargoQuantity;
            ships.Add(this);
        }
        
        static public void CheckCargo()
        {
            ShowCargo();
            foreach (Ship ship in ships)
            {
                if (ship._quantity >= ship._maxCargoQuantity)
                {
                    ship.Cargo = null;
                    ship._quantity = 0;
                    Console.WriteLine($"***Ship{ship.GetHashCode()} has been clear***");
                }
            }
            Console.WriteLine("");
            ShowCargo();
        }
        static private void ShowCargo()
        {
            foreach (Ship ship in ships)
            {
                Console.Write($"{ship.GetHashCode()}\t");
            }
            Console.WriteLine("\n");
            foreach (Ship ship in ships)
            {
                Console.Write($"{ship.Cargo}\t");
            }
            Console.WriteLine("\n");
            foreach (Ship ship in ships)
            {
                Console.Write($"{ship._quantity}\t");
            }
            Console.WriteLine("\n");

        }
    }
}
