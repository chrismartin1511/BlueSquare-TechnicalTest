using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JobService.AsyncDataServices;
using JobService.Data;
using JobService.Dtos;
using JobService.Models;
using JobService.SyncDataServices.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalJobsController : ControllerBase
    {
        private readonly IExternalJobRepo _repository;
        private readonly IMapper _mapper;
        private readonly IJobTransactionDataClient _jobTransactionDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public ExternalJobsController(
            IExternalJobRepo repository,
            IMapper mapper,
            IJobTransactionDataClient jobTransactionDataClient,
            IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _jobTransactionDataClient = jobTransactionDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<JobDto>> GetExternalJobsAsync()
        {
            var jobItems = _repository.GetAllExternalJobs();

            return Ok(_mapper.Map<IEnumerable<JobDto>>(jobItems));
        }

        [HttpGet("{id}", Name = "GetExternalJobById")]
        public async Task<ActionResult<JobDto>> GetExternalJobByIdAsync(int id)
        {
            var jobItem = _repository.GetExternalJobById(id);

            var jobTransactionDto = _mapper.Map<JobTransactionDto>(jobItem);

            // Send Sync Message
            try
            {
                await _jobTransactionDataClient.SendJobToJobTransaction(jobTransactionDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not send Synchronously: {ex.Message}");
            }

            if (jobItem != null)
            {
                return Ok(_mapper.Map<JobDto>(jobItem));
            }

            return NotFound();
        }

        [HttpGet("GetNewJobs", Name = "GetNewJobs")]
        public ActionResult<JobDto> GetNewJobs(string jobStatus)
        {
            var newJobItems = _repository.GetNewJobs();
           
            try
            {
                foreach (var newJobItem in newJobItems)
                {
                    var jobTransactionPublishedDto = _mapper.Map<JobTransactionPublishedDto>(newJobItem);

                    jobTransactionPublishedDto.UpdateNote = "GET Request New Job";
                    
                    _messageBusClient.PublishJobTransaction(jobTransactionPublishedDto);

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not send Asynchronously: {ex.Message}");
            }
           
            if (newJobItems != null)
            {
                return Ok(_mapper.Map<IEnumerable<JobDto>>(newJobItems));
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult<JobDto> UpdateExternalJob(JobDto jobDto, int jobId)
        {
            var jobModel = _mapper.Map<Job>(jobDto);
            _repository.UpdateExternalJobStatus(jobModel, jobId);
            _repository.SaveChanges();

            var updatedJob = _repository.GetExternalJobById(jobId);

            return Ok(_mapper.Map<JobDto>(updatedJob));
        }
    }
}