using Hotell.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotell.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Clientes> Cliente { get; set; }
        public DbSet<Reservas> Reservas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Habitaciones> Habitaciones { get; set; }
        public Contexto() : base("ConStr")
        {

        }
    }
        }