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

Now you can go to http://localhost:5000 to browse the swagger docs and test it out.


## Using it
The API starts a webserver on http://localhost:5000  
The documentation can be found at /docs, so please browse to http://localhost:5000/docs for the swagger docs  
