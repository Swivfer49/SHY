using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHY.Classes.Rendering
{

    //this class is for drawing a regular image to the screen
    internal class ImageDrawObject : DrawObject
    {
        public ImageDrawObject(Texture2D texture,Rectangle rect)
        {
            _texture = texture;
            Width = rect.Width;
            Height = rect.Height; 
            XOff = rect.X; 
            YOff = rect.Y;
        }

        private Texture2D _texture;
        private int Width, Height;
        private int XOff, YOff;

        public void Draw(int x, int y)
        {
            //draws the image
            DrawObject.SpriteBatch.Draw(_texture, new Rectangle(x + XOff, y + YOff, Width, Height), Color.White);
        }
    }
}
