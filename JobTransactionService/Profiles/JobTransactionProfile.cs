using AutoMapper;
using JobTransactionService.Dtos;
using JobTransactionService.Models;

namespace JobTransactionService.Profiles
{
    public class JobTransactionProfile : Profile
    {
        public JobTransactionProfile()
        {
            // Source -> Target
            CreateMap<JobTransaction, JobTransactionReadDto>();
            CreateMap<JobTransactionCreateDto, JobTransaction>();
            CreateMap<JobTransactionPublishedDto, JobTransaction>();
            CreateMap<JobTransaction, JobTransactionPublishedDto>();
        }
    }
}