using AutoMapper;
using DesafioSistemaTarefas.Application.Mappings;

namespace DesafioSistemaTarefas.Test.Mocks.Objects
{
    public static class MockMapper
    {
        public static readonly IMapper mockMapper = CreateMockIMapper();

        private static IMapper CreateMockIMapper()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TarefaProfile());
                cfg.AddProfile(new HistoricoTarefaProfile());
            });
            return mockMapper.CreateMapper();
        }
    }
}
