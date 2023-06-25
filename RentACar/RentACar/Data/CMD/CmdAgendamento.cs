using Microsoft.EntityFrameworkCore;
using RentACar.Data.Form;
using RentACar.Model;

namespace RentACar.Data.CMD
{
    public class CmdAgendamento
    {
        //-------------------------------------------------------------
        #region viaráveis
        //-------------------------------------------------------------
        public string MsgErro       { get; protected set; } = string.Empty;
        public string MsgSucesso    { get; protected set; } = string.Empty;

        private AgendamentoForm m_form;
        private Cliente m_cliente;
        private Car m_car;
        //-------------------------------------------------------------
        #endregion viaráveis
        //-------------------------------------------------------------

        //-------------------------------------------------------------
        #region construtor
        //-------------------------------------------------------------
        public CmdAgendamento( AgendamentoForm form ) 
        {
            m_form = form;
        }
        //-------------------------------------------------------------
        #endregion construtor
        //-------------------------------------------------------------

        //-------------------------------------------------------------
        #region viaráveis
        //-------------------------------------------------------------
        public async Task execCmdAsync( DataContext context )
        {
            try
            {
                if ( await validarCarroAsync( context ) && await validarAgendamentoAsync( context ) && await validarClienteAsync( context ) )
                {
                    await agendarAluguelAsync( context );
                }
            }
            catch( Exception e )
            {
                MsgErro = "Ocorreu um erro inesperado ao tentar agendar o aluguel do carro. Tente novamente mais tarde.";
            }
        }
        //-------------------------------------------------------------
        #endregion viaráveis
        //-------------------------------------------------------------

        //-------------------------------------------------------------
        #region classes private
        //-------------------------------------------------------------
        private async Task<bool> validarCarroAsync( DataContext context )
        {
            var car = await context.Cars.FindAsync( m_form.IdCar );

            if( car == null )
            {
                MsgErro = "O carro informado não foi encontrado.";
                return ( true );
            }

            m_car = car;

            return ( true );
        }

        //-------------------------------------------------------------
        private async Task<bool> validarAgendamentoAsync( DataContext context )
        {
            var agendamento = await context.Agendamentos.Where( x => x.Car.Id == m_car.Id )
                                                        .Where( x => ( DateTime.Compare( x.DataInicio, m_form.DataInicioAluguel ) >= 0 && 
                                                                       DateTime.Compare( x.DataFim, m_form.DataInicioAluguel ) <= 0 ) ||
                                                                     ( DateTime.Compare( x.DataInicio, m_form.DataFimAluguel ) >= 0 &&
                                                                       DateTime.Compare( x.DataFim, m_form.DataFimAluguel ) <= 0 ) )
                                                        .FirstOrDefaultAsync();

            if( agendamento != null )
            {
                MsgErro = string.Format( "O carro '{0}' informado já está agendado para a data '{1}'. Escolha outro carro ou data.",
                                        agendamento.Car.Id,
                                        m_form.DataInicioAluguel.ToString( "dd/MM/yyyy" ) );
                return ( true );
            }

            return ( true );
        }

        //-------------------------------------------------------------
        private async Task<bool> validarClienteAsync( DataContext context )
        {
            var cliente = await context.Clientes.Where( x => x.Nome == m_form.Cliente.Nome ).FirstOrDefaultAsync();

            if( cliente != null )
            {
                if( cliente.Password != m_form.Cliente.Password )
                {
                    MsgErro = "Login inválido.";
                    return ( false );
                }
            }

            context.Clientes.Add( new Cliente { Nome = m_form.Cliente.Nome, Password = m_form.Cliente.Password } );
            await context.SaveChangesAsync();

            cliente = context.Clientes.Where( x => x.Nome == m_form.Cliente.Nome ).Where( x => x.Password == m_form.Cliente.Password ).First();

            if( cliente == null )
            {
                MsgErro = "Ocorreu um erro ao tentar criar o novo usuário.";
                return ( false );
            }

            m_cliente = cliente;

            return ( true );
        }

        //-------------------------------------------------------------
        private async Task<bool> agendarAluguelAsync( DataContext context )
        {
            await context.Agendamentos.AddAsync( new Agendamento { Car = m_car, Cliente = m_cliente, DataInicio = m_form.DataInicioAluguel, DataFim = m_form.DataFimAluguel } );
            await context.SaveChangesAsync();

            var agendamento = context.Agendamentos.Where( x => x.Car.Equals( m_car ) )
                                            .Where( x => x.Cliente.Equals( m_cliente ) )
                                            .Where( x => x.DataFim.Equals( m_form.DataFimAluguel ) )
                                            .Where( x => x.DataInicio.Equals( m_form.DataInicioAluguel ) ).FirstOrDefault();
            if( agendamento != null )
            {
                int periodo = ( m_form.DataFimAluguel - m_form.DataInicioAluguel ).Days;
                await context.Pagamentos.AddAsync( new Pagamento { Agendamento = agendamento, ValorTotal = agendamento.Car.ValorDiaria * periodo } );
                await context.SaveChangesAsync();

                var pagamento = await context.Pagamentos.Where( x => x.Agendamento.Equals( agendamento ) ).FirstOrDefaultAsync();

                if( pagamento != null )
                {
                    MsgSucesso = string.Format( "O agendamento do carro '{0}' foi feito para o cliente '{1}' entre os dias {2} e {3} no valor total de R${4}.", 
                                                pagamento.Agendamento.Car.Id,
                                                pagamento.Agendamento.Cliente.Nome,
                                                pagamento.Agendamento.DataInicio.ToString( "dd/MM/yyyy" ),
                                                pagamento.Agendamento.DataFim.ToString( "dd/MM/yyyy" ),
                                                pagamento.ValorTotal );
                    return ( true );
                }
                else
                {
                    MsgErro = "Ocorreu um erro ao tentar agendar o aluguel do carro.";
                    return ( false );
                }
            }
            else
            {
                MsgErro = "Ocorreu um erro ao tentar agendar o aluguel do carro.";
                return ( false );
            }
        }

        //-------------------------------------------------------------
        #endregion classes private
        //-------------------------------------------------------------
    }
}
