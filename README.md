# BlueSquare-TechnicalTest
This is the Blue Square Technical Test

## Scenario
A customer has a web-based solution for creating, managing and closing repair jobs for the business. The system is archaic and they don’t have the skills in-house to integrate it with other solutions, but we have access to their database.
They want to keep the system in place as the cost to change is too high, but would like us to build a solution to help them better manage the repair jobs, communication with the end-user and part management.
You have been assigned the task of building this solution and the integration which is via RESTful API. You have been tasked with building the API integration.

## Services
- JobService:
  - Used to Mock an API, which connects the client's external database to get new jobs and update existing ones.
  - By default it uses an In Memory database for development, but can also use a MySql database.
  - Is designed to use RabbitMQ to publish messages using the Fanout method to any subscriber.

- JobTransactionService:
  - This services main functionality is to write any transactions to a database.
  - Currently it only writes GET requests by the JobService for new jobs to an In Memory database. This can be expanded to write UPDATE requests etc.
  - Subscribes to the RabbitMQ message publisher to receive transaction data.

- InternalJobService:
  - This service is intended to write any new or updated job data from the client's database to an internal MongoDB.
  - It has not yet been configured to subscribe to the RabbitMQ message bus.

- K8S
  - This contains YAML files used to configure and deploy the services, as well as a RabbitMQ instance and MySql database.

## To Do
- SmsService:
    - Create a service to send SMS messages when certain events occur.
- Configure InternalJobService to subscribe to RabbitMQ message bus.
- Finish Kubernetes setup:
  - Add deployment file for InternalJobService.
  - Add deployment file for MongoDB.