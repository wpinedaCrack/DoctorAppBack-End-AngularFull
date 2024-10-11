using Data.Interfaces.IRepositorio;

namespace Data.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo////patron IUnitofWork
    {
        private readonly AplicationDbContext _db;
        public IEspecialidadRepositorio Especialidad { get; private set; }
        public IMedicoRepositorio Medico { get; private set; }

        public UnidadTrabajo(AplicationDbContext db, IEspecialidadRepositorio especialidad)
        {
            _db = db;
            Especialidad = new EspecialidadRepositorio(_db);
            Medico = new MedicoRepositorio(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
