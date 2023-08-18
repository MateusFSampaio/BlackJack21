using System;
using System.Threading;
using System.Linq;
using System.Net.NetworkInformation;

namespace BlackJack
{
    // enumeration for each card
    public enum CardValues
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }
    class Program
    {

        public static string player = Environment.UserName;
        public static string cpu = "CPU";
        public static int playerScore = 0;
        public static int cpuScore = 0;
        public static int playerWins = 0;
        public static int cpuWins = 0;
        public static int round = 1;
        public static bool win = false;

 /*   public static void CardValueDictionary()
    {
        IDictionary<string, int> numberNames = new Dictionary<string, int>();
        numberNames.Add("Ace", 1);
        numberNames.Add("Two", 2);
        numberNames.Add("Three", 3);
        numberNames.Add("Four", 4);
        numberNames.Add("Five", 5);
        numberNames.Add("Six", 6);
        numberNames.Add("Seven", 7);
        numberNames.Add("Eight", 8);
        numberNames.Add("Nine", 9);
        numberNames.Add("Ten", 10);
        numberNames.Add("Jack", 11);
        numberNames.Add("Queen", 12);
        numberNames.Add("King", 13);

            foreach (KeyValuePair<string, int> kvp in numberNames)
            Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
        }*/


    static void TypeMessage(string msg)
        {
            {
                Console.Write(msg);

            }

        }

        static void Main(string[] args)
        {

            // terminal size and configuration
            Console.WindowHeight = 25;
            Console.WindowWidth = 65;

            string title = "\nBlackJack\n";

            /*Type type = typeof(CardValues);

            Array values = type.GetEnumValues();

            for (int i = 0; i < 1; i++)
            {
                int index = random.Next(values.Length);
                CardValues value = (CardValues)values.GetValue(index);
            }*/

            TypeMessage(title);
            Start();

        }

        public static void Start()
        {
            Console.WriteLine($"{player} VS CPU\n");


            while (true)
            {
                // array for enumeration of cards
                Array values = Enum.GetValues(typeof(CardValues));
                Random random = new Random();
                // get random cards from the enum CardValues
                CardValues cardValuesforPlayer = (CardValues)values.GetValue(random.Next(values.Length));
                CardValues cardValuesforCPU = (CardValues)values.GetValue(random.Next(values.Length));

                if (round == 1)
                {
                    // get the enum cardvalues                 
                    int playerCard = ((int)cardValuesforPlayer);
                    int cpuCard = ((int)cardValuesforCPU);
                    round++;
                }
                if (round > 1 && !win)
                {
                    // starts with the player buying first card
                    if (cpuScore == playerScore)
                    {
                        Console.Write($"Pedir primeira Carta. (Y) ");
                    }
                    else
                        Console.Write("Pedir outra carta? (Y/N): ");
                    string message = Console.ReadLine();
                    // verify readline and increase playerscore
                    if (message.ToLower() == "y" && !win)
                    {
                        // score for player
                        int playerCard = ((int)cardValuesforPlayer);
                        playerScore += playerCard;
                        Console.WriteLine($"{player} recebe {playerCard} total value: {playerScore}\n");
                        Result(playerScore, cpuScore);
                        if (win) return;

                    }
                    else if (round > 1 && message.ToLower() == "n" && !win)
                    {
                        while (cpuScore <= playerScore && playerScore > 0)
                        {
                            // created for specific while
                            CardValues cardValuesforCPUonLoop = (CardValues)values.GetValue(random.Next(values.Length));
                            int cpuCard = ((int)cardValuesforCPUonLoop);
                            cpuScore += cpuCard;

                            // thread added so player can see the result on terminal
                            //score for CPU
                            Thread.Sleep(3000);
                            Console.WriteLine($"{cpu} recebe {cpuCard} valor total: {cpuScore}\n");
                            Result(playerScore, cpuScore);
                            if (cpuScore > playerScore)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"{cpu} ganhou!");
                                Thread.Sleep(5000);
                                cpuWins++;

                                Exit();
                            }

                        }

                    }

                }


            }

        }

        static void Result(int score, int cpuscore)
        {
            if (score > 21)
            {
                // define the color of the terminnal red if the player lost
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine($"{player} excedeu 21");
                cpuWins++;
                // thread added so the player can read CPU result
                Thread.Sleep(5000);
                Exit();


            }
            if (cpuscore > 21)
            {
                // define green color if player wins
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{cpu} excedeu 21, {player} ganhou");
                playerWins++;
                Thread.Sleep(5000);
                Exit();

            }


        }
        private static void Exit()
        {
            // exit from console/terminal
            Console.Clear();
            Environment.Exit(0);
        }
    }

}