using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITourProblemRepository
{
    public TourProblem GetOne(int tourProblemId);
    public TourProblem Update(TourProblem tourProblemId);
    public List<TourProblemResponse> GetById(int tourtourProblemIdId);

}