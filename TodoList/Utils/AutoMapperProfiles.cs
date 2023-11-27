using AutoMapper;
using TodoList.DTO;
using TodoList.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Utils
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserLoginDTO, UserLogin>();
            CreateMap<UserLogin, UserDTO>();
            CreateMap<TareaCracionDTO, Tarea>();
            CreateMap<Tarea, TareaDTO>();
            CreateMap<Tarea, TareaUserDTO>()
                .ForMember(x => x.Nombre, options => options.MapFrom(c => c.Nombre))
                .ForMember(x => x.Descripcion, options => options.MapFrom(c => c.Descripcion))
                .ForMember(x => x.Completado, options => options.MapFrom(c => c.Completado))
                .ForMember(x => x.UserDTO, options => options.MapFrom(MapFromAutoresLibrosToAutoresDTO))
                ;
        }

        private UserDTO MapFromAutoresLibrosToAutoresDTO(Tarea tarea, TareaUserDTO tareaDTO)
        {
            UserDTO response = new UserDTO();
            if (tarea.UserLogin == null)
                return response;

            response = (new UserDTO { Id = tarea.UserLogin.Id, Correo = tarea.UserLogin.Correo });
            
            return response;
        }
    }
}
