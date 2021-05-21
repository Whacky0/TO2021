using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolvesAndRabbitsSimulation.Engine
{
    class World
    {
        private Random rnd = new Random();

        private const int width = 255;
        private const int height = 255;
        private Size size = new Size(width, height);
        private HashSet<GameObject> objects = new HashSet<GameObject>();
        private List<GameObject>[,] grid = new List<GameObject>[width, height];


        public IEnumerable<GameObject> GameObjects
        {
            get
            {
                return objects.ToArray();
            }
        }

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public float Random()
        {
            return (float)rnd.NextDouble();
        }

        public Point RandomPoint()
        {
            return new Point(rnd.Next(width), rnd.Next(height));
        }

        public int Random(int min, int max)
        {
            return rnd.Next(min, max);
        }

        private List<GameObject> GetBucketAt(Point pos)
        {
            return grid[PositiveMod(pos.X, width), PositiveMod(pos.Y, height)];
        }

        private List<GameObject> InitBucketAt(Point pos)
        {
            var bucket = new List<GameObject>();
            grid[PositiveMod(pos.X, width), PositiveMod(pos.Y, height)] = bucket;
            return bucket;
        }

        public void Add(GameObject obj)
        {
            objects.Add(obj);
            var bucket = GetBucketAt(obj.Position);
            if (bucket == null)
            {
                bucket = InitBucketAt(obj.Position);
            }
            bucket.Add(obj);
        }

        public void Remove(GameObject obj)
        {
            objects.Remove(obj);
            var bucket = GetBucketAt(obj.Position);
            if (bucket != null)
            {
                bucket.Remove(obj);
            }
        }

        public virtual void Update()
        {
            foreach (GameObject obj in GameObjects)
            {
                Point old = obj.Position;
                obj.UpdateOn(this);
                obj.Position = PositiveMod(obj.Position, size);
                if (!obj.Position.Equals(old))
                {
                    GetBucketAt(old).Remove(obj);
                    if (objects.Contains(obj))
                    {
                        Add(obj);
                    }
                }
            }
        }

        public virtual void DrawOn(Graphics graphics)
        {
            foreach (GameObject obj in GameObjects)
            {
                graphics.FillRectangle(new Pen(obj.Color).Brush, obj.Bounds);
            }
        }

        // http://stackoverflow.com/a/10065670/4357302
        private static int PositiveMod(int a, int n)
        {
            int result = a % n;
            if ((a < 0 && n > 0) || (a > 0 && n < 0))
                result += n;
            return result;
        }
        private static Point PositiveMod(Point p, Size s)
        {
            return new Point(PositiveMod(p.X, s.Width), PositiveMod(p.Y, s.Height));
        }

        public double Dist(PointF a, PointF b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public IEnumerable<GameObject> ObjectsAt(Point pos)
        {
            var bucket = GetBucketAt(pos);
            if (bucket == null) return new GameObject[0];
            return bucket;
        }
    }
}
