namespace ProyectoVentas.Models
{
    public class Reposicion
    {
        public int Id { get; set; }
        public int PrendaId { get; set; }
        public Prenda? Prenda { get; set; }
        public int Cantidad { get; set; }

        public DateTime FechaReposicion { get; set; }
    }
}
