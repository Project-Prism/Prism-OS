using System;
namespace PrismProject
{
    public class io
    {
        public static  string[] input = new string[128]; // an array that saves the last 128 user input
        public static int inputPosition = 0; // indicates the next free slot in the array
        public static int inpPosCur = 0; 
        struct cmdWindow
        {
            int posX;
            int posY;
            int height;
            int widht;
            // it is supposed to be something like this: https://appliedgo.net/media/tui/dashboard.gif

        }
        io()
        {

        }
        #region scroller
        public static void inputUpdater(string Input) 
        {
            
            if(inputPosition >= 128)
            {
                for (int i = 0; i < 128; i++)
                {
                    input[i] = input[i + 1];
                }
                input[128] = Input;
            }
            else
            {
                input[inputPosition] = Input;
                inputPosition++;                
            }
        }

        public static string inputScroller(bool direction) // 1 up, 0 down
        {
            inpPosCur = inputPosition;
            if(direction)
            {
                inpPosCur--;
                return input[inpPosCur];
            }
                
            else
            {
                if(inpPosCur++ > 128)
                {
                    inpPosCur = 0;
                    return input[inpPosCur];
                }                   
                else
                {
                    inpPosCur++;
                    return input[inpPosCur];
                }                
            }
        }
        #endregion
    }
}

