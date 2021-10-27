namespace Tic_Tac_Toe
{
    public class GameTile
    {
        public bool IsTokenX { get; set; }
        public bool IsTokenO { get; set; }
        public bool IsTokenBlank { get; set; }

        public GameTile()
        {
            IsTokenX = false;
            IsTokenO = false;
            IsTokenBlank = true;
        }

        public void SetTokenX()
        {
            IsTokenBlank = false;
            IsTokenX = true;
        }

        public void SetTokenO()
        {
            IsTokenBlank = false;
            IsTokenO = true;
        }

        public void ResetTile()
        {
            IsTokenX = false;
            IsTokenO = false;
            IsTokenBlank = true;
        }

        public string TileDisplay(int position)
        {

            if (IsTokenO) return " O ";
            if (IsTokenX) return " X ";
            if (position.ToString().Length > 1) return $" {position}";
            return $" {position} ";
        }

        public char GetToken()
        {
            if (IsTokenO) return 'O';
            if (IsTokenX) return 'X';
            else return ' ';
        }
    }
}