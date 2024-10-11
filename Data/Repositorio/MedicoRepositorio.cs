using Data.Interfaces.IRepositorio;
using Models.Entidades;

namespace Data.Repositorio
{
    public class MedicoRepositorio : Repositorio<Medico>, IMedicoRepositorio
    {
        private readonly AplicationDbContext _db;
        public MedicoRepositorio(AplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Medico medico)
        {
            var medicoDB = _db.Medicos.FirstOrDefault(e => e.Id == medico.Id);
            if (medicoDB != null)
            {
                medicoDB.Nombres = medico.Nombres;
                medicoDB.Apellidos = medico.Apellidos;
                medicoDB.Estado = medico.Estado;
                medicoDB.Direccion = medico.Direccion;
                medicoDB.Telefono = medico.Telefono;
                medicoDB.Genero = medico.Genero;
                medicoDB.EspecialidadId = medico.EspecialidadId;
                medicoDB.FechaActualizacion = medico.FechaActualizacion;
                _db.SaveChanges();
            }
        }

        //public async Task Agregar(Medico entidad)
        //{
        //    await  _db.AddAsync(entidad);
        //    await _db.SaveChangesAsync();
        //}

        //public Task<Medico> ObtenerPrimero(Expression<Func<Medico, bool>> filtro = null, string incluirPropiedades = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Medico>> ObtenerTodos(Expression<Func<Medico, bool>> filtro = null, Func<IQueryable<Medico>, IOrderedQueryable<Medico>> orderBy = null, string incluirPropiedades = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Remover(Medico entidad)
        //{
        //    throw new NotImplementedException();
        //}
    }
}