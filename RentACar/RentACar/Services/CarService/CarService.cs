using Azure.Core;

namespace RentACar.Services.CarService
{
    public class CarService
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
        public CarService( DataContext context )
        {
            _context = context;
        }
        //-------------------------------------------------------------
        #endregion construtor
        //-------------------------------------------------------------

        //-------------------------------------------------------------
        #region public class
        //-------------------------------------------------------------
        public async Task<List<Car>> GetAllCars()
        {
            return ( await _context.Cars.ToListAsync() );
        }

        //-------------------------------------------------------------
        public async Task<Car?> GetSingleCar( int Id )
        {
            var car = await _context.Cars.FindAsync( Id );

            if( car is null )
                return null;

            return ( car );
        }

        //-------------------------------------------------------------
        public async Task<List<Car>> AddCar( Car car )
        {
            _context.Cars.Add( car );
            
            await _context.SaveChangesAsync();

            return ( await _context.Cars.ToListAsync() );
        }

        //-------------------------------------------------------------
        public async Task<List<Car>?> UpdateCar( Car request )
        {
            var car = await _context.Cars.FindAsync( request.Id );

            if( car is null )
                return null;

            car.Nome = request.Nome;
            car.Modelo = request.Modelo;
            car.Marca = request.Marca;

            await _context.SaveChangesAsync();

            return ( await _context.Cars.ToListAsync() );
        }

        //-------------------------------------------------------------
        public async Task<List<Car>?> DeleteCar( int Id )
        {
            var car = await _context.Cars.FindAsync( Id );

            if( car is null )
                return null;

            _context.Cars.Remove( car );
            await _context.SaveChangesAsync();

            return ( await _context.Cars.ToListAsync() );
        }

        //-------------------------------------------------------------
        #endregion public class
        //-------------------------------------------------------------
    }
}
