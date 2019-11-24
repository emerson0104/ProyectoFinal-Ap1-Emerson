using Hotell.DAL;
using Hotell.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotell.BLL
{
    public class ReservasBLL
    {
        public static bool Guardar(Reservas r)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                RepositorioBase<Clientes> cl = new RepositorioBase<Clientes>();

                if (db.Reservas.Add(r) != null)
                {
                    string estado = "Ocupado";
                    foreach (var item in r.Detalle)
                    {
                        db.Habitaciones.Find(item.HabitacionId).Estado = estado;
                    }
                    
                    db.Usuarios.Find(r.UsuarioId).TotalVentas += r.MontroReserva;

                    paso = db.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static bool Modificar(Reservas reservas)
        {
            bool paso = false;
            Contexto db = new Contexto();
            RepositorioBase<Reservas> Reser = new RepositorioBase<Reservas>();


            try
            {
                var Anterior = ReservasBLL.Buscar(reservas.ReservaId);
                foreach (var item in Anterior.Detalle)
                {
                    // var habitacion = repositorio.Buscar(item.HabitacionId);
                    if (!reservas.Detalle.Exists(d => d.Id == item.Id))
                    {

                        db.Entry(item).State = EntityState.Deleted;


                    }
                }
                db.Entry(reservas).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();
            RepositorioBase<Usuarios> cl = new RepositorioBase<Usuarios>();
            Reservas ventas = new Reservas();

            try
            {
                string estado = "Disponible";
                foreach (var item in ventas.Detalle)
                {
                    db.Habitaciones.Find(item.HabitacionId).Estado = estado;
                }

                var Ventas = db.Reservas.Find(id);
                var clientes = cl.Buscar(Ventas.ReservaId);
                db.Usuarios.Find(Ventas.UsuarioId).TotalVentas -= Ventas.MontroReserva;
                db.Entry(Ventas).State = EntityState.Deleted;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Reservas Buscar(int id)
        {

            Reservas reservas = new Reservas();
            Contexto db = new Contexto();
            try
            {
                reservas = db.Reservas.Find(id);


                reservas.Detalle.Count();


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return reservas;
        }

        public static List<Reservas> GetList(Expression<Func<Reservas, bool>> reser)
        {
            List<Reservas> lista = new List<Reservas>();
            Contexto db = new Contexto();
            try
            {
                lista = db.Reservas.Where(reser).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return lista;
        }
        public static bool checkins(int id)
        {
            Reservas v = new Reservas();
            Contexto db = new Contexto();
            bool paso = false;

            try
            {
                string estado = "Disponible";
                foreach (var item in v.Detalle)
                {
                    db.Habitaciones.Find(item.HabitacionId).Estado = estado;
                }

                var Ventas = db.Reservas.Find(id);

                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static bool checkout(int id)
        {
            Reservas v = new Reservas();
            Contexto db = new Contexto();
            bool paso = false;

            try
            {
                string estado = "Disponible";
                foreach (var item in v.Detalle)
                {
                    db.Habitaciones.Find(item.HabitacionId).Estado = estado;
                }

                var Ventas = db.Reservas.Find(id);
                db.Entry(v).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }
    }
}