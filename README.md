# PokemonCulture API
Please clone or download the project.

## Getting started - Local windows machine edition
### Build
run `.\build.ps1` from the root of the project, either in your terminal of choice or run the powershell script directly (right click -> Run in powershell).  
The script, builds the solution, runs the tests and publishes the release version of the service.  
The release version is published to `Visualbean.Pokemon\bin\Release\netcoreapp3.1\win-x64`  

### Run
run ```.\run.ps1``` from the root of the project, either in your terminal of choice or run the powershell script directly (right click -> Run in powershell).  
This will run the service in the console as a selfhosted api.  

## Getting started - Docker (Linux container) aka. serious business edition.

### Build & Run
1. Run `docker build -t pokemonapi .` from the root of the project.    
2. Run `docker run -d -p 5000:80 --name shakespeareanpokemon pokemonapi`  

Now you can go to http://localhost:5000/docs to browse the swagger docs and test it out.


## Using it
The API starts a webserver on http://localhost:5000  
The documentation can be found at /docs, so please browse to http://localhost:5000/docs for the swagger docs  

## TODO
### Tests

- Test all of the things. - higher coverage
- Integration tests.

### Caching

- Cache in blob / redis / permanent storage as the data is 100% static due to the nature of pokemon descriptions. and the nature of a container is well not.. Would also mean better scalability in terms of having multiple instances.

### API

- Wrap responses in a proper response object
- Add throttling or  circuit breaker logic. (Throttling request based on downstream dependency throttling) - no reason to call other Apis if we know we will fail.
- add the possibility of telling which version you would prefer (red, green etc) possibly with odata or simply a query parameter.
- Taking description based on client language? or at least not taking First english, but random or based on version (red, green) ?? Localization?

### Project

- Logging
- Metrics
- Move downstream dependency clients to utility project.
- separate response classes from internal "domain" classes. (CQRS)
- Proper exception handler (middleware) (forgot to handle the throwing exception in the shakespeare service. )
