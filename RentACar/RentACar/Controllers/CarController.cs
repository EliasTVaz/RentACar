using Microsoft.AspNetCore.Mvc;
using RentACar.Services.CarService;

namespace RentACar.Controllers
{
    [ApiController]
    public class CarController : ControllerBase
    {
        //-------------------------------------------------------------
        #region variáveis
        //-------------------------------------------------------------
        private readonly CarService _carService;
        //-------------------------------------------------------------
        #endregion variáveis
        //-------------------------------------------------------------

        //-------------------------------------------------------------
        #region construtor
        //-------------------------------------------------------------
        public CarController( CarService carService )
        {
            _carService = carService;
        }
        //-------------------------------------------------------------
        #endregion construtor
        //-------------------------------------------------------------

        //-------------------------------------------------------------
        [HttpGet]
        [Route( "api/CarController/GetAllCars" )]
        public async Task<ActionResult<List<Car>>> GetAllCar()
        {
            return Ok( await _carService.GetAllCars() );
        }

        //-------------------------------------------------------------
        [HttpGet]
        [Route( "api/CarController/GetSingleCar" )]
        public async Task<ActionResult<Car>> GetSingleCar( int Id )
        {
            var result = await _carService.GetSingleCar( Id );

            if ( result == null )
                return NotFound( "Nenhum carro foi encontrado com o identificar informado." );

            return Ok( result );
        }

        //-------------------------------------------------------------
        [HttpPost]
        [Route( "api/CarController/AddCar" )]
        public async Task<ActionResult<List<Car>>> AddCar( [FromBody] Car car )
        {
            var result = await _carService.AddCar( car );
            return Ok( result );
        }

        //-------------------------------------------------------------
        [HttpPut]
        [Route( "api/CarController/UpdateCar" )]
        public async Task<ActionResult<List<Car>>> UpdateCar( [FromBody] Car request )
        {
            var result = await _carService.UpdateCar( request );

            if( result is null )
                return NotFound( "Carro não encontrado." );

            return Ok( result );
        }

        //-------------------------------------------------------------
        [HttpDelete]
        [Route( "api/CarController/DeleteCar" )]
        public async Task<ActionResult<Car>> DeleteCar( int Id )
        {
            var result = await _carService.DeleteCar( Id );

            if( result is null )
                return NotFound( "Carro não encontrado." );

            return Ok( result );
        }


    }
}
