namespace RentACar.DTOs
{
    public record struct AgendamentoCreateDto( Car car, Cliente cliente, DateTime Data );
}
