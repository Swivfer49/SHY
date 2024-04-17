using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHY.Classes.Rendering
{
    // stores the Transform information
    internal static class Camera
    {
        //the area viewed by the camera
        public static Rectangle CameraRectangle;


        //turns a rectangle in world space into a rectangle relative to the camera
        public static Rectangle RemapRectangle(Rectangle rectangle, Vector2 worldDimentions)
        {
            FRectangle rect = new(rectangle);
            Rectangle result = new Rectangle(
                (int)(  (   (rect.X - CameraRectangle.X)    /   CameraRectangle.Width)  *   worldDimentions.X),
                (int)(  (   (rect.Y - CameraRectangle.Y)    /   CameraRectangle.Height) *   worldDimentions.Y),
                (int)(  (rect.Width / CameraRectangle.Width)    *   worldDimentions.X),
                (int)(  (rect.Height / CameraRectangle.Height)  *   worldDimentions.Y)
            );
            return result;
        }

        //rectangle using floats to avoid silly int division situations
        struct FRectangle
        {
            public float X; 
            public float Y; 
            public float Width;
            public float Height;
            public FRectangle(Rectangle r)
            {
                X = r.X;
                Y = r.Y;
                Width = r.Width;
                Height = r.Height;
            }
        }
    }
}
