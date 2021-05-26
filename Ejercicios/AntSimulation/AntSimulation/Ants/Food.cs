using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSimulation
{
    class Food : GameObject
    {
        public static void SpawnOn(World world, Point center, float amount = 100)
        {
            int radius = (int)Math.Round(Math.Sqrt(amount) / 2);
            for (int x = center.X - radius; x <= center.X + radius; x++)
            {
                for (int y = center.Y - radius; y <= center.Y + radius; y++)
                {
                    Food f = new Food();
                    f.Position = new Point(x, y);
                    world.Add(f);
                }
            }
        }

        public Food()
        {
            Color = Color.Green;
        }
    }
}
