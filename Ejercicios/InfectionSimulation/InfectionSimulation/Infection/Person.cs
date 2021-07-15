using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace InfectionSimulation
{
    class Person : GameObject
    {
        public bool Infected { get; set; }



        public IEnumerable<Person> near(World world)
        {

            return world.GameObjectsNear(Position).Cast<Person>();
        }
        

       

        public override void UpdateOn(World world)
        {
            if (Infected)
            {

                Color = Color.Red;

                foreach (Person p in near(world))
                {
                    p.Infected = true;

                }

            }
            else
            {
                Color = Color.Green;
            }

            Forward(world.Random(1, 2));
            Turn(world.Random(-25, 25));
        }
        
    }
}
