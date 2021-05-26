using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSimulation
{
    class World
    {
        private Random rnd = new Random();

        private const int width = 125;
        private const int height = 125;
        private Size size = new Size(width, height);
        private HashSet<GameObject> objects = new HashSet<GameObject>();
        private List<GameObject>[,] grid = new List<GameObject>[width, height];

        public IEnumerable<GameObject> GameObjects { get { return objects.ToArray(); } }

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public Point Center { get { return new Point(width / 2, height / 2); } }

        public bool IsInside(PointF p)
        {
            return p.X >= 0 && p.X < width
                && p.Y >= 0 && p.Y < height;
        }
        
        public Point RandomPoint()
        {
            return new Point(rnd.Next(width), rnd.Next(height));
        }

        public float Random()
        {
            return (float)rnd.NextDouble();
        }

        public float Random(float min, float max)
        {
            return (float)rnd.NextDouble() * (max - min) + min;
        }

        public void Add(GameObject obj)
        {
            objects.Add(obj);
            var bucket = GetbucketAt(obj.Position);
            if (bucket==null)
            {
                bucket = InitBacketAt(obj.Position);
            }
            bucket.Add(obj);
        }

        public void Remove(GameObject obj)
        {
            objects.Remove(obj);
            var bucket = GetbucketAt(obj.Position);
            if (bucket!=null)
            {
                bucket.Remove(obj);
            }

        }

        public void Update()
        {
            foreach (GameObject obj in GameObjects)
            {
                Point old = obj.Position;
                obj.UpdateOn(this);
                obj.Position = Mod(obj.Position, size);
                obj.InternalUpdateOn(this);
                obj.Position = Mod(obj.Position, size);
                if (!obj.Position.Equals(old))
                {
                    GetbucketAt(old).Remove(obj);
                    if (objects.Contains(obj))
                    {
                        Add(obj);
                    }
                }
            }
        }

        public void DrawOn(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.White, 0, 0, width, height);
            foreach (GameObject obj in GameObjects)
            {
                graphics.FillRectangle(new Pen(obj.Color).Brush, obj.Bounds);
            }
        }

        public double Dist(PointF a, PointF b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public double Dist(float x1, float y1, float x2, float y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        // http://stackoverflow.com/a/10065670/4357302
        private static int Mod(int a, int n)
        {
            int result = a % n;
            if ((a < 0 && n > 0) || (a > 0 && n < 0))
                result += n;
            return result;
        }
        private static Point Mod(Point p, Size s)
        {
            return new Point(Mod(p.X, s.Width), Mod(p.Y, s.Height));
        }
        
        public IEnumerable<GameObject> GameObjectsNear(Point pos, float dist = 1)
        {
            var bucket = GetbucketAt(pos);
            if (bucket == null) return new GameObject[0];
            return bucket;
        }

        private List<GameObject> GetbucketAt(Point pos)
        {
            return grid[Mod(pos.X, width), Mod(pos.Y, height)];
        }
        private List<GameObject> InitBacketAt(Point pos)
        {
            var bucket = new List<GameObject>();
            grid[Mod(pos.X, width), Mod(pos.Y, height)] = bucket;
            return bucket;
        }



    }
}
