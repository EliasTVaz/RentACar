namespace RentACar.Data
{
    public class DataContext : DbContext
    {
        //-------------------------------------------------------------
        #region construtor
        //-------------------------------------------------------------
        public DataContext( DbContextOptions<DataContext> options ) :base( options ) { }
        //-------------------------------------------------------------
        #endregion construtor
        //-------------------------------------------------------------

        //-------------------------------------------------------------
        #region funções public
        //-------------------------------------------------------------
        public DbSet<Car>           Cars            { get; set; }
        public DbSet<Agendamento>   Agendamentos    { get; set; }
        public DbSet<Cliente>       Clientes        { get; set; }
        public DbSet<Pagamento>     Pagamentos      { get; set; }
        //-------------------------------------------------------------
        #endregion funções public
        //-------------------------------------------------------------
    }
}
