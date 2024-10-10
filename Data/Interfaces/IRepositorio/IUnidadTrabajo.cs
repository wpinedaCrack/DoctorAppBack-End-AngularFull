namespace Data.Interfaces.IRepositorio
{
    public interface IUnidadTrabajo: IDisposable//patron IUnitofWork
    {
        IEspecialidadRepositorio Especialidad { get; }
        Task Guardar();
    }
}
