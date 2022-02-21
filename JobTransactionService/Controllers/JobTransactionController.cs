using System;
using System.Collections.Generic;
using AutoMapper;
using JobTransactionService.Data;
using JobTransactionService.Dtos;
using JobTransactionService.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobTransactionService.Controllers
{
    [Route("api/t/[controller]")]
    [ApiController]
    public class JobTransactionController : ControllerBase
    {
        private readonly IJobTransactionRepo _repository;
        private readonly IMapper _mapper;

        public JobTransactionController(IJobTransactionRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<JobTransactionReadDto>> GetTransactions()
        {
            Console.WriteLine("--> Getting JobTransactions from JobTansactionService");

            var jobTransactionItems= _repository.GetAllTransactions();

            return Ok(_mapper.Map<IEnumerable<JobTransactionReadDto>>(jobTransactionItems));
        }

        [HttpGet("{jobId}", Name = "GetJobsLatestTransaction")]
        public ActionResult<JobTransactionReadDto> GetJobsLatestTransaction(int jobId)
        {
            Console.WriteLine($"--> Hit GetJobsLatestTransaction: {jobId}");

            if (!_repository.JobTransactionExists(jobId))
            {
                return NotFound();
            }

            var jobTransaction = _repository.GetJobsLatestTransaction(jobId);

            if (jobTransaction == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<JobTransactionReadDto>(jobTransaction));
        }

        [HttpPost]
        public ActionResult<JobTransactionReadDto> CreateJobTransaction(JobTransactionCreateDto jobTransactionCreateDto)
        {
            Console.WriteLine("--> Hit Create Job Transaction");

            var jobTransaction = _mapper.Map<JobTransaction>(jobTransactionCreateDto);

            _repository.CreateJobTransaction(jobTransaction);
            _repository.SaveChanges();

            var jobTransactionReadDto = _mapper.Map<JobTransactionReadDto>(jobTransaction);

            return CreatedAtRoute(nameof(GetJobsLatestTransaction), new { jobId = jobTransactionReadDto.JobId }, jobTransactionReadDto );
        }

        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Job Transaction Service");

            return Ok("Inbound test ok from Jobs Controller");
        }
    }
}