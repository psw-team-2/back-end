using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using AutoMapper;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Tours.Core.UseCases
{
    public class SecretService : ISecretService
    {
        private readonly ISecretRepository _secretRepository;
        public SecretService(ISecretRepository secretRepository)
        {
            _secretRepository = secretRepository;
        }

        public Result<SecretDto> GetSecretForCheckPoint(int checkPointId)
        {
            //if (!turistaOtkljucaoTacku(checkPointId))
            //{
            //    return Result.Fail("Check point not Unlocked");
            //}
            Secret secret = _secretRepository.GetByCheckPointId(checkPointId);//SECRET NAM JE NULL
            SecretDto secretDto = new SecretDto()
            {
                CheckPointId = secret.CheckPointId,
                Text = secret.Text,
            };

            if (secret == null)
            {
                return Result.Fail("Check point with id doesnt exists");
            }
            return secretDto;
        }

        
    }
}
