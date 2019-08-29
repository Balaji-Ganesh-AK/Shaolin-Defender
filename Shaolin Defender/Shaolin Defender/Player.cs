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
        float maxDragSpeed;
        float angle = 0;
      //  float turnAngle = .5f;
        public void playerMovement()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))//&& Player.playerPos.Y>0 + player.Height/2)
                                                      playerPos.Y -= speed;
            //{
            //    playerPos.X += dragSpeed;
            //    playerPos.Y -= dragSpeed;
            //}
            if (Keyboard.GetState().IsKeyDown(Keys.S))//&& Player.playerPos.Y < 390)
                playerPos.Y += speed;

            if (Keyboard.GetState().IsKeyDown(Keys.A)) //&& Player.playerPos.X>0+player.Width/2)
              playerPos.X -= speed;
          
            if (Keyboard.GetState().IsKeyDown(Keys.D))// && Player.playerPos.X < 390)
                playerPos.X += speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                //playerPos.X = playerPos.X - 664 * (float)Math.Cos(turnAngle)*0.01f;
                //playerPos.Y =  playerPos.Y - 502* (float)Math.Sin(turnAngle) * 0.01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                angle += 0.1f;
        }
        public void playerRotation(Vector2 dir)
        {

            perpendicualr = new Vector2(-dir.Y, dir.X);
            playerPos -= perpendicualr * dragSpeed;
        }

        public Vector2 getPlayerPos()
        {
            return playerPos;
        }
    }
}
