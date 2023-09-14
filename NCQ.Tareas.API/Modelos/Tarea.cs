using System.ComponentModel.DataAnnotations;

namespace NCQ.Tareas.API.Modelos
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int? ColaboradorId { get; set; }
        public byte Estado{ get; set; }
        public byte Prioridad { get; set;}

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        public DateTime FechaFin { get; set; }
        public string? Notas { get; set; }

    }
}
