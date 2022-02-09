# BlueSquare-TechnicalTest
This is the Blue Square Technical Test

## Scenario
A customer has a web-based solution for creating, managing and closing repair jobs for the business. The system is archaic and they don’t have the skills in-house to integrate it with other solutions, but we have access to their database.
They want to keep the system in place as the cost to change is too high, but would like us to build a solution to help them better manage the repair jobs, communication with the end-user and part management.
You have been assigned the task of building this solution and the integration which is via RESTful API. You have been tasked with building the API integration.

## Requirements
- Your solution should be serverless and use micro-services (e.g. AWS Lambda)
- It needs to routinely check for new jobs in the client system
- New jobs found should be created in our system which is a mixture of Mongo / DynamoDB and MySQL 
- Full job details should be written to Mongo / DynamoDB
- Transaction data should be written to MySQL
- It should update the client system when we receive payments, and when jobs are closed
- It should trigger an SMS to the end-user when we receive their job as a courtesy, and should send them another SMS when we close the job, asking for feedback
- Our SMS system should utilise Vonage's API and their documentation can be found here - https://developer.nexmo.com/use-cases/private-sms-communication

## Client MySQL Database schema – Table = Jobs

Field name       | Type
---------------- | ----------------
Job_id           | Int
Job_type         | Varchar
Job_status       | Varchar
Client_firstname | Varchar
Client_lastname  | Varchar
Client_postcode  | Varchar
Client_mobile    | Varchar
Product_id       | Varchar
Product_type     | Varchar
  
## Solution MySQL Database Schema – Table = job_trans

Field name       | Type
---------------- | ----------------
Job_id           | Int
Job_status       | Varchar
creationDate     | Datetime
Update_note      | Varchar


## Mongo Collection – This can be assumed as however you like. You just need to write JSON to the collection.

```json

{
  "jobs": [
    {
      "job_id": "00123",
      "job_date": "01012021",
      "job_type": "warranty repair",
      "job_status": "New",
      "customer": [
        {
          "client_firstname": "Joe",
          "client_lastname": "Bloggs",
          "client_postcoce": "SG13 7TZ",
          "client_mobile": "+447770 123456"
        }
      ],
      "product": [
        {
          "product_id": "123",
          "product_type": "spider catcher"
        }
      ]
    }
  ]
}

```

## Expectation
Please provide a series of scripts or snippets that will perform the following tasks in a micro-service environment;

- Routinely check for and collect new jobs from the clients database
- Write job data to the two databases
- Update jobs in the client system
- Trigger and compose SMS messages
- For every GET, PUT or Event (SMS) you should write that transaction to the job_trans table
- Job_status can be:
-- NEW - Job created in client system
-- OPENED - Job Processed and sms sent to user
-- IN PROGRESS - Client started work on repair
-- JOB COMPLETED - Client work completed
-- CLOSED - Payment received and feedback SMS sent

- If you want to Mock your own API for this solution, you can.
- You do not need to create any databases or a vonage account. You do not need to deploy this code.
- You may find that the requirements or information required is incomplete, feel free to alter or expand on them as you see fit and mention these in a committed text file. 
