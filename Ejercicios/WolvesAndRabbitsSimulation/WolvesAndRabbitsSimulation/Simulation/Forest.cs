using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolvesAndRabbitsSimulation.Engine;

namespace WolvesAndRabbitsSimulation.Simulation
{
    class Forest : World
    {
        public const int PATCH_SIZE = 2;
        private int grassWidth, grassHeight;
        private short[,] grass;

        private int ticks = 0;

        public Forest()
        {
            grassWidth = (Width / PATCH_SIZE) + 1;
            grassHeight = (Height / PATCH_SIZE) + 1;
            grass = new short[grassWidth, grassHeight];

            for (int x = 0; x < grassWidth; x++)
            {
                for (int y = 0; y < grassHeight; y++)
                {
                    grass[x, y] = (short)Random(0, 50);
                }
            }
        }

        public override void DrawOn(Graphics graphics)
        {
            var shouldUpdate = ++ticks > 10;
            if (shouldUpdate) { ticks = 0; }

            for (int x = 0; x < grassWidth; x++)
            {
                for (int y = 0; y < grassHeight; y++)
                {
                    var growth = grass[x, y];
                    if (shouldUpdate)
                    {
                        growth += 10;
                        if (growth > 255) { growth = 255; }
                        else if (growth < 0) { growth = 0; }
                        grass[x, y] = growth;
                    }

                    var rect = new Rectangle(x * PATCH_SIZE, y * PATCH_SIZE, PATCH_SIZE, PATCH_SIZE);
                    graphics.FillRectangle(new Pen(Color.FromArgb(growth, 0, 255, 0)).Brush, rect);
                }
            }
            base.DrawOn(graphics);
        }

        public short GetGrassAt(Point pos)
        {
            return grass[(short)(pos.X / PATCH_SIZE), (short)(pos.Y / PATCH_SIZE)];
        }

        public short RemoveGrassAt(Point pos, short amount)
        {
            var result = GetGrassAt(pos);
            if (result < amount) { amount = result; }

            result -= amount;
            grass[(short)(pos.X / PATCH_SIZE), (short)(pos.Y / PATCH_SIZE)] = result;
            return amount;
        }
    }
}
