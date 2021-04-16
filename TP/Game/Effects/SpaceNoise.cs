using Engine;
using Engine.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class SpaceNoise : GameObject
    {
        private Image image;
        float speed;
        float scale;
        bool flipX;
        bool flipY;

        public SpaceNoise(Image image, float speed, float scale, bool flipX, bool flipY)
        {
            this.speed = speed;
            this.scale = scale;
            this.flipX = flipX;
            this.flipY = flipY;
            this.image = image;

            Extent = new SizeF(image.Width * scale, image.Height * scale);
        }

        public override void Update(float deltaTime)
        {
            MoveLeft(deltaTime);
            KeepInsideScreen();
        }

        private void MoveLeft(float deltaTime)
        {
            X -= speed * deltaTime;
        }

        private void KeepInsideScreen()
        {
            X = X.Mod(Parent.Width);
            Y = Y.Mod(Parent.Height);
        }

        public override void DrawOn(Graphics graphics)
        {
            FillScreenTiled(graphics);
        }

        public void FillScreenTiled(Graphics graphics)
        {
            int w = Width.RoundedToInt();
            int h = Height.RoundedToInt();
            int x = Position.X.RoundedToInt();
            int y = Position.Y.RoundedToInt();
            while (x >= Parent.Left) { x -= w; }
            while (y >= Parent.Top) { y -= h; }

            for (int x1 = x; x1 <= Parent.Right; x1 += w)
            {
                for (int y1 = y; y1 <= Parent.Bottom; y1 += h)
                {
                    var img = new Bitmap(image, new Size(w, h));
                    if (flipX) { img.RotateFlip(RotateFlipType.RotateNoneFlipX); }
                    if (flipY) { img.RotateFlip(RotateFlipType.RotateNoneFlipY); }
                    graphics.DrawImage(img, new Point(x1, y1));
                }
            }
        }
    }
}
