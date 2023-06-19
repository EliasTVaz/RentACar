using Microsoft.AspNetCore.Mvc;
using RentACar.DTOs;
using RentACar.Services.CarService;

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

        [HttpGet]
        [Route( "api/RentController/ListCars" )]
        public async Task<ActionResult<Car>> ListCars( CarCreateDto request )
        {
            var car = await _context.Cars.FindAsync( request.Nome, request.Modelo, request.Valor );
        }

        //-------------------------------------------------------------
        [HttpGet]
        [Route( "api/RentController/CreateCliente" )]
        public async Task<ActionResult<Car>> CreateCliente( ClienteCreateDto request )
        {

        }

        //-------------------------------------------------------------
        [HttpGet]
        [Route( "api/RentController/CreateAgendamento" )]
        public async Task<ActionResult<List<Agendamento>>> CreateAgendamento( AgendamentoCreateDto request )
        {
            var newAgendamento = new Agendamento
            {
                Car = request.car,
                Cliente = request.cliente,
                Data = request.Data
            };

            _context.Agendamentos.Add( newAgendamento );
            await _context.SaveChangesAsync();

            return Ok( await _context.Agendamentos.Include( a => a.Car ).ToListAsync());
        }

        //-------------------------------------------------------------
    }
}
