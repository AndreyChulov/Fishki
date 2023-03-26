namespace Fishki
{
    public static class FishkaEnumExtension
    {
        public static FishkaEnum Roll(this FishkaEnum fishka)
        {
            return fishka == FishkaEnum.Enabled ? FishkaEnum.Disabled : FishkaEnum.Enabled;
        }
    }
}