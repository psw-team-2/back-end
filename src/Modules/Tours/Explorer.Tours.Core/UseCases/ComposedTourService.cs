using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases
{
    public class ComposedTourService : CrudService<ComposedTourDto, ComposedTour>, IComposedTourService
    {
        IComposedTourRepository _composedTourRepository;
        public ComposedTourService(ICrudRepository<ComposedTour> repository, IMapper mapper) : base(repository, mapper){}
        
    }
}
