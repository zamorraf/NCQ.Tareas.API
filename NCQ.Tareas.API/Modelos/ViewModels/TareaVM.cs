using System.ComponentModel.DataAnnotations;

namespace NCQ.Tareas.API.Modelos.ViewModels
{
    public class TareaVM
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int ColaboradorId { get; set; }
        public byte Estado { get; set; }
        public byte Prioridad { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        public DateTime FechaFin { get; set; }
        public string Notas { get; set; }

        public string ColaboradorNombre { get; set; }
        public string EstadoDesc { get; set; }
        public string PrioridadDesc { get; set; }
    }
}
