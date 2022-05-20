namespace PongOnline.Models
{
    public class Game
    {
        public Game()
        {
            BallStats = new List<BallStats>();
        }
        public int Id { get; set; }
        public Player? Player1 { get; set; }
        public Player? Player2 { get; set; }
        public List<BallStats> BallStats { get; set; }
    }
    public class Player
    {
        public int Id { get; set; }
        public int PlayerNumber { get; set; }
        public int Score { get; set; }
        public bool Win { get; set; }
    }
    public class BallStats
    {
        public int Id { get; set; }
        public int Ypos { get; set; }
        public bool Dir { get; set; }
        public Player? Player { get; set; }
    }
}
