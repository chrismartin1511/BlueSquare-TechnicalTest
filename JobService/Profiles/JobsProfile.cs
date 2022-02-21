using AutoMapper;
using JobService.Dtos;
using JobService.Models;

namespace JobService.Profiles
{
    public class JobsProfile : Profile
    {
        public JobsProfile()
        {
            // Source -> Target
            CreateMap<Job, JobTransactionDto>();
            CreateMap<Job, JobDto>();
            CreateMap<JobDto, Job>();
            CreateMap<Job, JobTransactionPublishedDto>();

            // Need map to from ExternalJob -> MongoModel
        }
    }
}