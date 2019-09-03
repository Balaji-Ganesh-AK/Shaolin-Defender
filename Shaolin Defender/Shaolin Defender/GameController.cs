using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaolin_Defender
{
    class GameController
    {
        Game1 game1;
        public int score;
        public float timer;
        public GameController()
        {
            score = 0;
            timer = 100.00f;
        }
        public void increaseScore()
        {
            score++;
        }
        public int getScore()
        {
            return score;
        }
        public int checkWinState()
        {
            if (score == 5)
            {
                return 1;
            }
            else
                return 0;
        }
        public void reset()
        {
            score = 0;
        }
        public void resetTimer()
        {
            timer = 100.00f;
        }
        bool checkCondition()
        {

            return true;
        }
        
    }
}
