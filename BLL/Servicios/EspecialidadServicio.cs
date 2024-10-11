using AutoMapper;
using BLL.Servicios.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entidades;

namespace BLL.Servicios
{
    public class EspecialidadServicio : IEspecialidadServicio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;

        public EspecialidadServicio(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
        }

        public async Task<EspecialidadDto> Agregar(EspecialidadDto modeloDto)
        {
            try
            {
                Especialidad nuevaEspecialidad = new Especialidad
                {
                    NombreEspecialidad = modeloDto.NombreEspecialidad,
                    Descripcion = modeloDto.Descripcion,
                    Estado = modeloDto.Estado == 1 ? true : false,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                };
                await _unidadTrabajo.Especialidad.Agregar(nuevaEspecialidad);
                await _unidadTrabajo.Guardar();
                if (nuevaEspecialidad.Id == 0)
                    throw new TaskCanceledException("La Especialidad no se puede crear");

                return _mapper.Map<EspecialidadDto>(nuevaEspecialidad);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Actualizar(EspecialidadDto modeloDto)
        {
            try
            {
                var especialidadDB = await _unidadTrabajo.Especialidad.ObtenerPrimero(x => x.Id == modeloDto.Id);
                if (especialidadDB == null)
                    throw new TaskCanceledException("La Especialidad no existe");

                especialidadDB.NombreEspecialidad = modeloDto.NombreEspecialidad;
                especialidadDB.Descripcion = modeloDto.Descripcion;
                especialidadDB.Estado = modeloDto.Estado == 1 ? true : false;

                _unidadTrabajo.Especialidad.Actualizar(especialidadDB);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<EspecialidadDto>> ObtenerTodos()
        {
            try
            {
                var lista = await _unidadTrabajo.Especialidad.ObtenerTodos(orderBy: e => e.OrderBy(e => e.NombreEspecialidad));

                return _mapper.Map<IEnumerable<EspecialidadDto>>(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<EspecialidadDto>> ObtenerActivos()
        {
            try
            {
                var lista = await _unidadTrabajo.Especialidad.ObtenerTodos(x => x.Estado == true,
                    orderBy: e => e.OrderBy(e => e.NombreEspecialidad));

                return _mapper.Map<IEnumerable<EspecialidadDto>>(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task Remover(int id)
        {
            try
            {
                var especialidadDB = await _unidadTrabajo.Especialidad.ObtenerPrimero(x => x.Id == id);
                if (especialidadDB == null)
                    throw new TaskCanceledException("La Especialidad no existe");

                _unidadTrabajo.Especialidad.Remover(especialidadDB);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}