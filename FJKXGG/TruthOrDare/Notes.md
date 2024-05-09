# Documentation and Notes for the project

- The project is a simple CRUD application with a console user interface
- The user interface is designed to be easily replaceable with a REST API

## Project structure, architecture:

- The software architecture is a weird mixture of ports and adapters and layered
	- The UI and the Infrastructure is connected with ports and adapters
	- The main logic is in the UI's controller
	- Rest of the application is doing CRUD
- Not DDD based namespace and folder structure

## Exception handling method:

- Catch as low as possible the exceptions and throw custom exceptions with user friendly message to the upper layers
- Flow control based on the built in exceptions or custom exceptions if layer crossing is needed

## Used design principles:

- Single responsibility
- Interface segregation
- Open/closed
- Liskov substitution
- Dependency inversion
- Dependency injection
- Don't repeat yourself
- CQRS
- CQS
- Keep it stupid simple -> Obviously I'm Failed in this point X)
- Write access modifiers everywhere

## Used Design Patterns:

- Repository
- Controller
- Service (not needed so finally not used)
- Entity
- Ports and Adapters

## Future improvements:

- Result pattern
- Better Exception handling
- Logging
- Usage of reflection

## Kérdéseim:

- Access modifiers:
	- Hogy választja szét az assembly-ket a fordító? Egy másik projekt ugyan abban a solution-ben másik assembly?
	- Ha egy internal osztály implementál egy internal interface-t, ami egy másik namespace-ben van, akkor az osztály metódusainak miért kell public-nek lenniük?
- Hiba kezelés
	- Hogyan érdemes megoldani a hiba kezelést egy egyszerû adatbázis lekérdezést végrehajtó metódusban?
	- Result pattern:
		- ténylegesen elterjedt a gyakorlatban a Result Pattern?
  		- CQS pattern-t sérti
   	- Miért nem lehet nyelvi szinten megadni ha egyetódus exception-t dob?									
- Mennyire kell a Single Responsibility és interface segregation elvet követni? - Pl. CRUD mûveleteket szét kell bontani (CQRS)?
	- YAGNI: You Aint Gonna Need It
	- Application layeren belüli Main controller osztály privát metódusait nem service-ba szervezem ki és mennek a Domain rétegbe hanem csak privát metódusokba
