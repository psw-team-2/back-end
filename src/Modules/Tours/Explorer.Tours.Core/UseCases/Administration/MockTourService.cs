using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.UseCases;

public class MockTourService : CrudService<MockTourDto, MockTour>, IMockTourService
{
    public MockTourService(ICrudRepository<MockTour> repository, IMapper mapper) : base(repository, mapper) { }
}