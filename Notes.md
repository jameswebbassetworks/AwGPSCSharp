These notes are to summarize the project and some of the gotchas when looking through.


### Maintainability

The primary design here is for maintainability and scaling the service to handle more messages.  

There are two locations to look at when adding new message types to parse:
- `MessageTypes` folder
  > Message Types are the different manufacturer messages and each of the sub folders here can be used as a template.  
  > Duplicating one of these folders and then adjusting each file for a new message will add a new parsed message type to the project.
- `EventCodeHandlers` folder
  > EventCodeHandlers will be run for the Event Code name based on the enumeration found in the Message Types folders.


### Assumptions

One of the biggest things missing from the requirements are the data types for the incoming messages. Best judgement was used for these data types and can be seen in the DTO's and Parsed message classes.

No changes were made to the base files of this project (other than package adds).  It was assumed that `MessageProcessor.cs` is the entry point and all registrations/parsing happened from that point.


### Dependencies

`FluentValidation` - This was added to create a nice fluent way to parse the messages from fields list into a DTO that mirrors the data.  Once in a DTO, I could then map that DTO to the respective message class.  FluentValidation creates a nice pattern for error checking.

`Microsoft.Extensions.Logging` - This was added for the logger factory to create a static implementation for logging.


### Incomplete

Unit Tests:

This one was funny as trying to add a xUnit test project to the solution, a ton of conflicts came up due to the `SLN` and the only project existing in the same directory.  Adding the unit test project seemed to make a nested folder in the project and would then conflict with the build.
