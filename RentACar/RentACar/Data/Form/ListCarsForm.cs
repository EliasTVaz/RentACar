using Microsoft.IdentityModel.Tokens;
namespace RentACar.Data.Form
{
    public class ListCarsForm : BaseForm
    {
        //-------------------------------------------------------------
        #region viaráveis
        //-------------------------------------------------------------
        public string Nome { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        //-------------------------------------------------------------
        #endregion viaráveis
        //-------------------------------------------------------------

        //-------------------------------------------------------------
        #region classes override
        //-------------------------------------------------------------
        public override bool isFormValid()
        {
            return ( true );
        }
        //-------------------------------------------------------------
        #endregion classes override
        //-------------------------------------------------------------
    }
}
