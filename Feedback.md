# Feedback

## My approach : 
Hi,

As it was stated that I had to time box the challenge I would like to describe how I discovered the project and how I prioritized the tasks.

For clarity, in this document, the `Provided API` will be called the `Movies API`, it's named this way on Swagger. And the `Challenge API`, the one I need to implement, will be called the `Cinema API`

I Started by the Readme and gathered the requirements. I noticed a few things : 
    a. I need to use a  `Movies API` that is already provided
    b. The database layer should be done, I checked the Database folder and I was able to find database queries to handle the creation of showtimes/tickets and to confirm the payment. I noticed that every operation was receiving a `CancellationToken`, probably that's something I will need to leverage in this exercice.
    c. I listed the requirements in a todo list at the end of this document 

Running `docker-compose up` allowed me to reach Swagger for the `Movies API`, an authentication key is requeried. It will be added to the configuration file.

Continuing my exploration, I looked at the docker-compose.yaml file to better understand what was made available for this environment. It pulls the `lodgify/movies-api:3`, checking on dockerhub I noticed that an update was available [lodgify/movies-api:4](https://hub.docker.com/layers/lodgify/movies-api/4/images/sha256-857bcefb757c46cf65b6b00bf5e0db51cc7d0f055f48bf6c8a7c08f6a461f263), it was not required to updated the movie api but that could be a nice upgrade for later. The main difference seems to be the .NET version that goes from 6 to 8.


Openning the csproj shows that the `Cinema API` is already configured as a gRPC client. It's in .NET 3.1 and includes a `Controllers\` folder that cannot be found in the project directory. This will need to be created. A gRPC package is available `Grpc.AspNetCore` but I am wondering if it's not missing a Client specific package. To be tested.


Program.cs confirms that the Console logging is configured.


Startup.cs confirms that the Database is configured and Controllers are declared. Just need to create them. There is no authentication configuration.

There is a Dockerfile for the `ApiApplication` project as well.

From there my plan is to : 
1. Add a unit tests project to be able to cover any new feature. As I don't have much time, I will only test the controllers.
2. Configure Swagger for the `Cinema API`, a documentation is always better when it's easy to share !
3. Fix the gRPC connection as I understand we need this application to be optimized for fast queries
4. Add an authentication layer to avoid any unwanted queries on our `Cinema API`
5. Implement Showtimes creation
6. Use caching, this could be seen as an early optimization but as the `Movies API` is **really slow** and fails more times than it works, this becomes a necessity to make our API usable and reduce our users frustration.
7. Implement seats reservations
8. Implement seats confirmations
9. Add execution time tracking, this is hepfull for long terme observability and optimization, so it's not a priority.
10. Fix the `Movies API` configuration if anything is wrong there.

After creating the unit test, controller stub for showtimes and configured swagger, it was time to move to using the gRPC call to get the movies list.

Fixing the gRPC call required to use the Api Key found in the `Movies API` swagger. I added the key to the configuration file and injected the configuration to the `ApiClientGrpc` service. While doing so, I moved the proto file to it's `Protos` directory and the grpc client to a `Services` directory, for clarity. Now we can move to working with shows !

Thinking about the screen to create shows, I figured we would have a form where we can select a movie, an auditorium and a date. A price would have been nice but I noticed it was not requested by the data layer. I also thought that we wouldn't like to expose the grpc layer to the controller, because later we would add caching, to do so I created DTOs and updated the grpc client to return a `List<MovieDTO>` instead of grpc objects.

A strange disparity I noticed between `MovieEntity`and the `showResponse` is that the first is expecting an ImdbId and an int for it's primary id, while the last only sends back a string id. This could be clarified by checking the API documentation or asking the dev team for mor information but for this exercice I will assume the Id returned by the `Movies API` is the imdbId and that the int id related to the `MovieEntity` is the unique id of this entity when associated with a show. ie, we could have a single movie associated with multiple show times and they will have as many unique ids.

## Requirments 

This list is priorized.


- [x] Add unit tests project to have a TDD approach
- [x] Configure Swagger 
- [x] Authentication - Protected the `Cinema API` with an API Key, for the need of this exercice it will be static.
- [x] Movies API - Configure the API Key
- [x] Movies API - gRPC needs to be fixed, for faster communication
- [x] Showtimes - Create showtimes by using the `Movies API`
- [ ] Cache - Use caching to improve reliability, ProvidedAPI is slow and fails a lot.
- [ ] Reserve seats - Reponse should contain : GUID, Nb of seat, Auditorium name, Movie Name
- [ ] Reserve seats - Reservation should expire after 10 min
- [ ] Reserve seats - A reserved seat cannot be reserved again if it didn't expire 
- [ ] Reserve seats - A sold seat cannot be reserved
- [ ] Buy seats - Request should contain : Reservation GUID
- [ ] Buy seats - It is not possible to buy the same seat two times
- [ ] Buy seats - Expired reservation GUIDs are not accepted

- [ ] Execution Tracking - track the execution time of each request
- [ ] Add requests to cUrls file
- [ ] Fix `Movies API` configuration


## To be improved 