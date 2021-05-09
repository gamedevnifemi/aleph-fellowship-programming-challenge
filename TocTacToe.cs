using System;

namespace TicTacToe
{
    class Program
    {
        // Using the mini max algorithm rather than the random algorithm gives us contol of the chances of X winning
        static void Main()
        {
            // X always starts from the middle
            int[] gameBoard = { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 };
            for (int turn = 1; turn < 10 && win(gameBoard) == 0; turn++)
            {
                if (turn % 2 == 0)
                    computerInput(gameBoard);
                else
                {
                    playerInput(gameBoard);
                }
            }
            switch (win(gameBoard))
            {
                case 0:
                    Console.WriteLine("It's a draw. \n");
                    break;
                case 1:
                    Console.WriteLine("You lose. \n");
                    break;
                case -1:
                    Console.WriteLine("You win. \n");
                    break;

            }
        }

        static int win(int[] gameBoard)
        {
            int[,] possibleWins = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 }, { 1, 5, 9 }, { 3, 5, 7 } };
            for (int i = 0; i < 8; i++)
            {
                int first = possibleWins[i, 0];
                int second = possibleWins[i, 1];
                int third = possibleWins[i, 2];
                if (gameBoard[first] != 0 && gameBoard[first] == gameBoard[second] && gameBoard[second] == gameBoard[third])
                {
                    return gameBoard[first];
                }
            }
            return 0;
        }

        // the minimax function helps us optimize the best possible ways of X winning
        static int minimax(int[] gameBoard, int user)
        {
            int winner = win(gameBoard);
            if (winner != 0) return winner * user;
            int input = -1;
            int score = -2;
            for (int i = 0; i < 9; i++)
            {
                if (gameBoard[i] == 0)
                {
                    gameBoard[i] = user;
                    int thisScore = -minimax(gameBoard, user * -1);
                    if (thisScore > score)
                    {
                        score = thisScore;
                        input = i;
                    }
                    gameBoard[i] = 0;
                }
            }
            if (input == -1)
            {
                return 0;
            }
            else
            {
                return score;
            }
        }

        static void computerInput(int[] gameBoard)
        {
            int input = -1;
            int score = -2;
            for (int i = 1; i < 10; i++)
            {
                if (gameBoard[i] == 0)
                {
                    gameBoard[i] = 1;
                    int temporaryScore = -minimax(gameBoard, -1);
                    gameBoard[i] = 0;
                    if (temporaryScore > score)
                    {
                        score = temporaryScore;
                        input = i;
                    }
                }
            }
            gameBoard[input] = 1;
            Console.WriteLine("Computer played " + input);
        }

        static void playerInput(int[] gameBoard)
        {
            string value;
            int input = 0;
            do
            {
                Console.WriteLine("\nEnter input ([1,2,3,4,6,7,8,9)]: ");
                value = Console.ReadLine();
                input = Convert.ToInt32(value);
                Console.WriteLine("\n");
            } while (input >= 9 || input < 0 || gameBoard[input] == 1 || gameBoard[input] == -1);
            gameBoard[input] = -1;
        }

    }
}
