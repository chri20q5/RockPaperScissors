namespace RockPaperScissors
{
    public class RockPaperScissors
    {
        private static readonly string LeaderboardFilepath = "leaderboard.csv";

        public class Player
            {
                public string Name { get; set; }
                public int Score { get; set; }
            }

        public static void Main(string[] args)
        {
            var leaderboard = LoadLeaderboard();


            Console.WriteLine("Welcome to Rock Paper Scissors!");
            Console.WriteLine("What is your name?");
            string playerName = Console.ReadLine();
            int playerScore = 0;

            bool playAgain = true;


            while (playAgain)
            {
                Thread.Sleep(1000);
                Console.WriteLine();
                Console.WriteLine("What do you choose? Rock, Paper, or Scissors?");

                string input = Console.ReadLine().ToLower();
                Random rnd = new Random();
                int computerChoice = rnd.Next(1, 4);


                if (input == "rock" || input == "paper" || input == "scissors")
                {
                    Console.WriteLine($"You chose {input}. Get ready!");
                    Thread.Sleep(500);
                    Console.WriteLine("Rock!");
                    Thread.Sleep(1500);
                    Console.WriteLine("Paper!");
                    Thread.Sleep(1500);
                    Console.WriteLine("Scissors!");
                    Thread.Sleep(1500);
                    switch (computerChoice)
                    {
                        case 1:
                            Console.WriteLine("Computer chose Rock!");
                            if (input == "rock")
                            {
                                Console.WriteLine("It's a tie!");
                                break;
                            }

                            if (input == "paper")
                            {
                                Console.WriteLine("You win!");
                                playerScore++;
                                break;
                            }

                            Console.WriteLine("You lose!");

                            break;

                        case 2:
                            Console.WriteLine("Computer chose Paper!");
                            if (input == "paper")
                            {
                                Console.WriteLine("It's a tie!");
                                break;
                            }

                            if (input == "scissors")
                            {
                                Console.WriteLine("You win!");
                                playerScore++;
                                break;
                            }

                            Console.WriteLine("You lose!");
                            break;

                        case 3:
                            Console.WriteLine("Computer chose Scissors!");
                            if (input == "scissors")
                            {
                                Console.WriteLine("It's a tie!");
                                break;
                            }

                            if (input == "rock")
                            {
                                Console.WriteLine("You win!");
                                playerScore++;
                                break;
                            }

                            Console.WriteLine("You lose!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
                Console.WriteLine($"Current Score: {playerName} {playerScore}");
                Console.WriteLine("Ready for another round? (yes/no)");
                string playagain = Console.ReadLine().ToLower();

                if (playagain != "yes")
                {
                   playAgain = false;

                   UpdateLeaderboard(leaderboard, playerName, playerScore);
                   SaveLeaderboard(leaderboard);
                   Console.WriteLine("Leaderboard updated!");
                }
            }

            Console.WriteLine("Final Leaderboard:");
            foreach (var player in leaderboard)
            {
                Console.WriteLine($"{player.Name}: {player.Score}");
            }
        }

        private static List<Player> LoadLeaderboard()
        {
            var leaderboard = new List<Player>();

            if (File.Exists(LeaderboardFilepath))
            {
                string[] lines = File.ReadAllLines(LeaderboardFilepath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                    {
                        leaderboard.Add(new Player { Name = parts[0], Score = score });
                    }
                }
            }
            return leaderboard;
        }

        private static void SaveLeaderboard(List<Player> leaderboard)
        {
            var lines = new List<string>();
            foreach (var player in leaderboard)
            {
                lines.Add($"{player.Name},{player.Score}");
            }
            File.WriteAllLines(LeaderboardFilepath, lines);
        }

        static void UpdateLeaderboard(List<Player> leaderboard, string Name, int playerScore)
        {
            var existingPlayer = leaderboard.Find(p => p.Name == Name);
            if (existingPlayer != null)
            {
                existingPlayer.Score += playerScore;
            }
            else
            {
                leaderboard.Add(new Player { Name = Name, Score = playerScore });
            }
        }
    }
}