using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentACar.Data.CMD;
using RentACar.Data.Form;

namespace RentACar.Controllers
{
    [ApiController]
    public class RentController : ControllerBase
    {
        //-------------------------------------------------------------
        #region variáveis
        //-------------------------------------------------------------
        private readonly DataContext _context;
        //-------------------------------------------------------------
        #endregion variáveis
        //-------------------------------------------------------------

        //-------------------------------------------------------------
        #region construtor
        //-------------------------------------------------------------
        public RentController( DataContext context )
        {
            _context = context;
        }
        //-------------------------------------------------------------
        #endregion construtor
        //-------------------------------------------------------------

        [HttpPost]
        [Route( "api/RentController/ListCars" )]
        public async Task<ActionResult<Car>> ListCars( ListCarsForm form )
        {
            var cars = _context.Cars.Where( x => x.Marca.Contains( form.Nome ) )
                        .Where(x => x.Marca.Contains( form.Modelo ))
                        .Where(x => x.Marca.Contains( form.Marca ))
                        .ToArrayAsync();

            if( cars != null )
                return Ok( await cars );
            else
                return NotFound( "Não foi encontrado nenhum carro." );
        }

        //-------------------------------------------------------------
        [HttpPost]
        [Route( "api/RentController/AgendamentoCliente" )]
        public async Task<ActionResult<Car>> AgendamentoCliente( [FromBody]AgendamentoForm form )
        {
            if( form.isFormValid() )
            {
                CmdAgendamento cmd = new CmdAgendamento( form );

                await cmd.execCmdAsync( _context );

                if( cmd.MsgErro.IsNullOrEmpty() )
                    return Ok( cmd.MsgSucesso );
                else
                    return NotFound( cmd.MsgErro );
            }
            else
                return NotFound( form.msgErro );
        }
    }
}
