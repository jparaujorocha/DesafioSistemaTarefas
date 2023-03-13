using AutoMapper;
using DesafioSistemaTarefas.Application.DTOs;
using DesafioSistemaTarefas.Domain.Entities;

namespace DesafioSistemaTarefas.Application.Mappings
{
    public class HistoricoTarefaProfile : Profile
    {
        public HistoricoTarefaProfile()
        {
            CreateMap<Tarefa, HistoricoTarefaDto>()
           .ForMember(dest => dest.Id, opt =>
           {
               opt.UseDestinationValue()
               ;
           })
           .ForSourceMember(src => src.Id, opt =>
           {
               opt.DoNotValidate();
           })
           .ForMember(dest => dest.IdTarefa, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.IdStatusTarefa, opt => { opt.SetMappingOrder(1); })
           .ForMember(dest => dest.Status, opt =>
           {
               opt.SetMappingOrder(2);
               opt.MapFrom(src => src.GetNomeStatusTarefa(src.IdStatusTarefa));
           }).ReverseMap();


            CreateMap<HistoricoTarefa, HistoricoTarefaDto>()
           .ForMember(dest => dest.IdStatusTarefa, opt => { opt.SetMappingOrder(1); })
           .ForMember(dest => dest.Status, opt =>
           {
               opt.SetMappingOrder(2);
               opt.MapFrom(src => src.GetNomeStatusTarefa(src.IdStatusTarefa));
           }).ReverseMap();
        }
    }
}
