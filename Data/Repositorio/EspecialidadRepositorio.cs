using Data.Interfaces.IRepositorio;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    public class EspecialidadRepositorio : Repositorio<Especialidad>, IEspecialidadRepositorio
    {
        private readonly AplicationDbContext _db;
        public EspecialidadRepositorio(AplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Especialidad especialidad)
        {
            var especialidadDB = _db.Especialidades.FirstOrDefault(e=>e.Id== especialidad.Id);
            if (especialidadDB!= null)
            {
                especialidadDB.NombreEspecialidad = especialidad.NombreEspecialidad;
                especialidadDB.Descripcion= especialidad.Descripcion;
                especialidadDB.Estado=especialidad.Estado;
                especialidadDB.FechaCreacion = especialidad.FechaCreacion;
                especialidadDB.FechaActualizacion = especialidad.FechaActualizacion;
                _db.SaveChanges();
            }
        }

        //public async Task Agregar(Especialidad entidad)
        //{
        //    await  _db.AddAsync(entidad);
        //    await _db.SaveChangesAsync();
        //}

        //public Task<Especialidad> ObtenerPrimero(Expression<Func<Especialidad, bool>> filtro = null, string incluirPropiedades = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Especialidad>> ObtenerTodos(Expression<Func<Especialidad, bool>> filtro = null, Func<IQueryable<Especialidad>, IOrderedQueryable<Especialidad>> orderBy = null, string incluirPropiedades = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Remover(Especialidad entidad)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
