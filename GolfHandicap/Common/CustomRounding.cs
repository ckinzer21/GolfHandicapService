namespace GolfHandicap.Common
{
    public interface ICustomRounding
    {
        int RoundHalfUpElseFloor(double value);
    }

    public class CustomRounding : ICustomRounding
    {
        public int RoundHalfUpElseFloor(double value)
        {
            if (value % 1 == 0.5)
                return (int)Math.Ceiling(value);
            return (int)Math.Floor(value);
        }
    }
}
