namespace RentACar.Data.Form
{
    public class AgendamentoForm : BaseForm
    {
        //-------------------------------------------------------------
        #region viaráveis
        //-------------------------------------------------------------
        public int          IdCar               { get; set; }
        public Cliente      Cliente             { get; set; } = new Cliente();
        public DateTime     DataInicioAluguel   { get; set; }
        public DateTime     DataFimAluguel      { get; set; }
        //-------------------------------------------------------------
        #endregion viaráveis
        //-------------------------------------------------------------

        //-------------------------------------------------------------
        #region classes override
        //-------------------------------------------------------------
        public override bool isFormValid()
        {
            if( IdCar <= 0 )
            {
                msgErro = "Um dos carros deve ser escolhido.";
                return ( false );
            }

            if ( Cliente.Nome == string.Empty )
            {
                msgErro = "O login do usuário deve ser peenchido.";
                return( false );
            }

            if( Cliente.Password == string.Empty )
            {
                msgErro = "A senha do usuário deve ser peenchida.";
                return ( false );
            }

            if( DateTime.Compare( DataFimAluguel, DataInicioAluguel ) <= 0 )
            {
                msgErro = "A data de fim do aluguel deve ser maior que a data de inicio.";
                return ( false );
            }

            return ( true );
        }
        //-------------------------------------------------------------
        #endregion classes override
        //-------------------------------------------------------------
    }
}
