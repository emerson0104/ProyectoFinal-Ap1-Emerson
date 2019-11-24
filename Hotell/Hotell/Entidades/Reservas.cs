using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotell.Entidades
{
    public class Reservas
    {
        [Key]
        public int ReservaId { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaLlegada { get; set; }
        public DateTime FechaSalida { get; set; }
        public decimal MontroReserva { get; set; }
        [Browsable(false)]
        public int UsuarioId { get; set; }

        public virtual List<ReservasDetalle> Detalle { get; set; }
        public Reservas()
        {
            ReservaId = 0;
            ClienteId = 0;
            FechaReserva = DateTime.Now;
            FechaLlegada = DateTime.Now;
            FechaSalida = DateTime.Now;
            MontroReserva = 0;
            UsuarioId = 0;
            Detalle = new List<ReservasDetalle>();
        }

    }
}