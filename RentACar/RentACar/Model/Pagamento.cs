namespace RentACar.Model
{
    public class Pagamento
    {
        public int          Id          { get; set; }
        public Decimal      ValorTotal  { get; set; }
        public Agendamento  Agendamento { get; set; }
    }
}
