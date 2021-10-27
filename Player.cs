public class Player
{
    public string FirstName { get; set; }
    public int GamesWon { get; set; }
    public char PlayerToken { get; set; }

    public Player(string firstName, char playerToken)
    {
        FirstName = firstName;
        GamesWon = 0;
        PlayerToken = playerToken;
    }

    public void PlayerWon() => GamesWon++;
}
