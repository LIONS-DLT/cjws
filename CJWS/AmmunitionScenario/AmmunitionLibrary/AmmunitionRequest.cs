namespace AmmunitionLibrary
{
    public class AmmunitionRequest
    {
        public string OrderID { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public string AmmunitionType { get; set; } = string.Empty;
        public int Rounds {  get; set; }

    }
}