using System.Data.SqlTypes;

namespace RentACar.Model
{
    public class Car
    {
        public int      Id          { get; set; }
        public string   Nome        { get; set; }
        public string   Modelo      { get; set; }
        public string   Marca       { get; set; }
        public Decimal  ValorDiaria { get; set; }

        //-------------------------------------------------------------
        #region construtor
        //-------------------------------------------------------------
        public Car() 
        {
            Nome = string.Empty;
            Modelo = string.Empty;
            Marca = string.Empty;
            ValorDiaria = Decimal.Zero;
        }
        //-------------------------------------------------------------
        #endregion construtor
        //-------------------------------------------------------------
    }
}
