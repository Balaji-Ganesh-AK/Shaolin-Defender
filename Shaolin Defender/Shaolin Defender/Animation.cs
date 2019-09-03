using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Shaolin_Defender
{
    class Animation
    {
        Texture2D animation;
        Rectangle sourceRect;
        Vector2 position;
        float timeElapsed;
        float frameTime;
        int numOfFrames;
        int currentFrame;
        int frameHeight;
        int frameWidth;
        bool isLooping;
        public Animation(ContentManager content, Texture2D asset, float frameSpeed, int numOfFrames, bool isLooping)
        {
            this.frameTime = frameSpeed;
            this.numOfFrames = numOfFrames;
            this.isLooping = isLooping;
            animation = asset;
            frameHeight = animation.Height;
            frameWidth = animation.Width / numOfFrames;
            
        }
        public void Play(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
            sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            if (timeElapsed >= frameTime)
            {
                if (currentFrame >= numOfFrames - 1)
                {
                    if (isLooping)
                    {
                        currentFrame = 0;
                    }
                }
                else
                {
                    currentFrame++;
                }
                timeElapsed = 0;
            }
        }
        public void Draw(SpriteBatch sprite ,Vector2 position , float rotation , Vector2 origin)
        {
            
            sprite.Draw(animation, position, sourceRect,Color.White,rotation,origin, 1.0f,SpriteEffects.None , 0);
        }
    }
}