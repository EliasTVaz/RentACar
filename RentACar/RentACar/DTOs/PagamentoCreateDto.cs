namespace RentACar.DTOs
{
    public record struct PagamentoCreateDto( Decimal ValorTotal, Agendamento agendamento );
}
