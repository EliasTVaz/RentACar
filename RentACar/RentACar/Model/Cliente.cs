using System.Text.RegularExpressions;

namespace RentACar.Model
{
    public class Cliente
    {
        public int                  Id              { get; set; }
        public string               Nome            { get; set; }
        public string               Password        { get; set; }

        //-------------------------------------------------------------
        #region construtor
        //-------------------------------------------------------------
        public Cliente()
        {
            Nome = string.Empty;
            Password = string.Empty;
        }
        //-------------------------------------------------------------
        #endregion construtor
        //-------------------------------------------------------------
    }
}
