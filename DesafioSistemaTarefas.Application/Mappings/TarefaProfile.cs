using AutoMapper;
using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Domain.Entities;

namespace DesafioSistemaTarefas.Application.Mappings
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<Tarefa, TarefaDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.IdStatusTarefa, opt => { opt.SetMappingOrder(1); })
           .ForMember(dest => dest.Status, opt => {
               opt.SetMappingOrder(2);
               opt.MapFrom(src => src.GetNomeStatusTarefa(src.IdStatusTarefa));
           })
          .ReverseMap();
        }
    }
}
