# C# Developer Interview - Vehicle Message Processor

## Overview
This is a console application that mimics the processing of messages from devices attached to vehicles. The shell of the application has been provided, and you will need to implement the core message processing logic.

## What's Already Implemented
- **Message Class**: Defines a message with a `Dictionary<int, string>` for fields and an `int MessageType` property
- **MessageParser**: Parses XML messages into Message objects
- **MessageQueue**: Uses a BlockingCollection to allow messages to be passed between threads
- **MessageProcessor**: Called by the consumer thread to process messages (needs implementation)
- **WebRequestHelper**: Provides both synchronous and asynchronous HTTP GET methods
- **Program.cs**: Requests a file path, creates a consumer thread and then parses messages into the queue

## Message Format
Messages are in XML format:
```xml
<message type="X">
  <field number="0">VALUE1</field>
  <field number="1">VALUE2</field>
  <field number="X">VALUEX</field>
</message>
```

Each field will contain some data that the device is sending to the system. These can be 
doubles, integers, strings, date times etc.

The messages will be parsed for you so you don't need to worry about the XML format. The
important part is which fields contain which bits of information and in what format that
information is in.

## Your Task

Your task is to implement the Process(Message message) method in the MessageProcessor

This method should

### 1. Extract the relevant information from the message
There are several message types defined in [SampleMessages/README.md](SampleMessages/README.md)

These message types have different arrangements of fields but contain generally the same
information. These fields can be accessed using the Fields Dictionary on the message object

You'll want to come up with some kind of mechanism for determining how to translate the
passed in message based on its message type. This mechanism should be maintainable with
a large number of message types so a simple switch statement won't be enough

You'll then need to decide what you want to be returned by this translation mechanism

### 2. Take the appropriate action based on the event code in the message
Once you have translated the message you will need to take the appropriate action based
on its event code

In the real world this would involve generating events, saving information to the database
and sending out notifications but for this example we are just going to write information
to the console

There are 5 different event types

#### Location
Display the event type, timestamp and location information

#### Speeding Start
Display the event type, timestamp, location information and speed

#### Speeding Stop
Display the event type, timestamp, location information current speed and max speed if available

#### Idle Start
Display the event type, timestamp and location information

#### Idle End
Display the event type, timestamp, location information current speed and idle time if available

One of the message types also contains a VIN field. For these messages you should use
an HTTP request to get information about the vehicle and display that as well

Use the `WebRequestHelper` to call: `https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVin/{VIN}?format=json`

Again you'll want to come up with some kind of mechanism for taking the appropriate action
based on the event code and again we need this to be maintainable with a large number of event
codes so it can't be a simple switch statement

## Message Types
There are three different message types (0, 1, and 3). Each type represents messages from a different GPS device manufacturer, which is why they have different field mappings and event codes:

**Type 0** (Manufacturer A): Event Code in Field 1 (1=location, 2=start speeding, 3=end speeding, 4=idle start, 5=idle end)

**Type 1** (Manufacturer B): Event Code in Field 0 (50=location, 62=start speeding, 63=end speeding, 97=idle start, 98=idle end)

**Type 3** (Manufacturer C): Event Code in Field 12 (15=location, 26=idle start, 28=idle end, 51=start speeding, 52=end speeding), includes VIN in Field 0

See the `SampleMessages/` folder for complete examples of each message type and event.

## Running the Application
The application can be built and ran with Visual Studio, Visual Studio Code or the command line

For the command line use
1. Build the solution: `dotnet build`
2. Run the application: `dotnet run`

For Visual Studio Code you will likely want the C# Dev Kit extension

When running the application it will prompt you for a path to an XML file containing
messages. Use the files in the `SampleMessages/` folder for testing

The file should be copied to the output when building so you can simple
enter `SampleMessages/...` or you can use absolute paths

Note that the files we test the program with may not be the same ones provided

## Evaluation Criteria
- Code organization and structure
- Proper use of interfaces and abstraction
- Error handling
- Usage of web requests
- Code readability and maintainability
- Proper separation of concerns

## Bonus Points
- Add logging
- Display Only the 'Model Year', 'Make', 'Model' and 'Fuel Type - Primary'
- Add unit tests
- Handle edge cases and validation

Good luck! #Thank you!

---

## Solution Overview & Design Decisions

This section describes the design and implementation choices made to complete the assignment.

### High-Level Architecture

The solution follows a clear separation of concerns and is organized into logical layers:

- **Domain**
  - Core business models such as `VehicleEvent` and `VehicleEventType`
  - No infrastructure or framework dependencies

- **Vendors**
  - Message translators per GPS manufacturer (`MessageTypeZero`, `MessageTypeOne`, `MessageTypeThree`)
  - Each translator is responsible for mapping raw message fields into a normalized domain event

- **Processing**
  - `MessageBuilder` selects the correct translator based on `MessageType`
  - `MessageProcessor` orchestrates message translation and dispatching
  - `VehicleEventDispatcher` handles event-specific output logic

- **Infrastructure (Infra)**
  - VIN decoding client using the provided `WebRequestHelper`
  - Logging abstraction with a console-based implementation

This structure keeps the system extensible, testable, and easy to reason about.

---

### Message Translation Strategy

Instead of using large `switch` statements, each message type is handled by a dedicated translator implementing a common interface.

Benefits:
- Adding a new device type requires **no changes** to existing logic
- Field mappings are isolated per manufacturer
- Improves readability and maintainability

---

### Event Handling Strategy

Event handling is implemented using a dictionary-based dispatcher:

- Each `VehicleEventType` is mapped to a dedicated handler
- Avoids complex conditional logic
- Makes behavior easy to extend or modify

---

### VIN Enrichment

For message types containing a VIN:
- The VIN is decoded using the official NHTSA API
- Only the following fields are displayed as required:
  - Model Year
  - Make
  - Model
  - Fuel Type – Primary

This logic is isolated from core processing to avoid coupling business logic with external services.

---

### Logging

Basic structured logging was added to:
- Track message processing flow
- Record event type and timestamps
- Aid debugging without cluttering business logic

---

### Validation & Error Handling

The system validates:
- Required fields per message type
- Supported message types and event codes
- Proper parsing of timestamps and coordinates

Failures are handled using clear exceptions and fail-fast behavior.

---

### Testing Strategy

Unit tests were added for:
- Message translators (field mapping and event detection)
- Validation and error scenarios

External services (VIN decoding API) are treated as third-party dependencies and are not directly unit tested.

---

### Why This Design

The chosen design emphasizes:
- **Open/Closed Principle** – new message types can be added without modifying existing code
- **Single Responsibility** – each class has a clear, focused purpose
- **Testability** – core logic can be tested without infrastructure dependencies
- **Real-world maintainability** – mirrors patterns used in production message-processing systems