using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.UseCases;

public class TourProblemService : CrudService<TourProblemDto, TourProblem>, ITourProblemService
{
    public TourProblemService(ICrudRepository<TourProblem> repository, IMapper mapper) : base(repository, mapper) { }
}