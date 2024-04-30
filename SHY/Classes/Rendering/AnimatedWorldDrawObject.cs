using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHY.Classes.Rendering
{
    internal class AnimatedWorldDrawObject : DrawObject
    {
        public AnimationManager manager;
        private Texture2D _texture;
        private int Width, Height;
        private int XOff, YOff;

        public AnimatedWorldDrawObject(Texture2D texture, Rectangle rect, string animationFilePath)
        {
            _texture = texture;
            Width = rect.Width;
            Height = rect.Height;
            XOff = rect.X;
            YOff = rect.Y;

            manager = new(animationFilePath);
        }

        public void Update(double milliseconds)
        {
            manager.Update(milliseconds);
        }

        public void Draw(int x, int y)
        {
            //draws the image
            DrawObject.SpriteBatch.Draw(
                _texture,
                //remap rectangle to camera
                Camera.RemapRectangle(
                    new Rectangle(x + XOff, y + YOff, Width, Height)
                ),
                //the rectangle of the texture used (according to the animationManager)
                manager.CurrentFrame(),
                Color.White
            );
        }
    }
}
