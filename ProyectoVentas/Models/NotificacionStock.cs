namespace ProyectoVentas.Models
{
    public class NotificacionStock
    {
        public int Id { get; set; }
        public int PrendaId { get; set; }
        public Prenda? Prenda { get; set; }
        public String Nombre { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        
    }
}
