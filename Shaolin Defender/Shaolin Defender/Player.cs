using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaolin_Defender
{
    class Player
    {
        public  Vector2 playerPos;
        public  Rectangle playerRectangle;

        Vector2 perpendicualr;
        int speed = 4;
        float dragSpeed = 0.01f;
        
        float angle = 0;
        public float turnAngle = (float)(270 * (Math.PI / 180));
        public float turnMax;
        public void playerMovement()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                playerPos.Y -= speed;
                turnAngle = (float)( 180 * (Math.PI/180));
               

            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                playerPos.Y += speed;
                turnAngle = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && playerPos.X > 153)
            {
                playerPos.X -= speed;
                turnAngle = (float)(90 * (Math.PI / 180));
            }
            // ATTENTION: Need to find end screen position to prevent player from going off of it
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                playerPos.X += speed;
                turnAngle = (float)(270 * (Math.PI / 180));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                angle += 0.1f;
            }
        }
        public void playerRotation(Vector2 dir)
        {

            perpendicualr = new Vector2(-dir.Y, dir.X);
            playerPos -= perpendicualr * dragSpeed;
        }
        public float getDircetion(float turnMax)
        {

            return 0;
        }
        public Vector2 getPlayerPos()
        {
            return playerPos;
        }
    }
}
