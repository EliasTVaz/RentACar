namespace RentACar.Model
{
    public class Agendamento
    {
        public int      Id      { get; set; }
        public Car      Car     { get; set; }
        public Cliente  Cliente { get; set; }
        public DateTime Data    { get; set; }
    }
}
