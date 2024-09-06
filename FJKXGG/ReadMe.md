# Truth or Dare Console Game

Well-structured, SOLID and documented Truth or Dare console game for Gábor Ruzsinszki's C# course.

- The project is a simple CRUD application with a console user interface
- The user interface is designed to be easily replaceable with a REST API

## Goal

To create a simple CRUD application to demonstrate modern software design concepts and good practices.

## Usage

Build the project and run. The entry point is in the `TruthOrDare/Program.cs` class.

## Technical Documentation

### Project structure, architecture:

- The software architecture is a mixture of ports and adapters and layered
        - The UI and the Infrastructure is connected with ports and adapters
        - The main logic is in the UI's controller
        - The rest of the application is doing CRUD
        - Not DDD based namespace and folder structure

### Exception handling method:

- Catch as low as possible the exceptions and throw custom exceptions with user-friendly messages to the upper layers
- Flow control based on the built-in exceptions or custom exceptions if layer crossing is needed

### Used design principles:

- Single Responsibility
- Interface Segregation
- Open/Closed
- Liskov Substitution
- Dependency Inversion
- Dependency Injection
- Don't Repeat Yourself
- CQRS
- CQS
- Keep it simple, stupid -> Obviously, I'm Failed on this point X)
- Write access modifiers everywhere

### Used design patterns:

- Repository
- Controller
- Entity
- Ports and Adapters****
