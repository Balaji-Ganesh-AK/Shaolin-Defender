using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaolin_Defender
{
    class GameController
    {
        public int score;
        public float timer;
        public GameController()
        {
            score = 0;
            timer = 20.00f;
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
            if (score == 4)
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
    }
}
