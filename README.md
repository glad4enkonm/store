# store
[![Build Status](https://travis-ci.org/glad4enkonm/store.svg?branch=master)](https://travis-ci.org/glad4enkonm/store)

Web API using ASP.NET Core 2.0 exercise

It's time to summarize my 3 days efforts:

- swagger-ui integrated in API and available on root url
- had some fun trying to build swagger-codegen with mvn (wanted to check the last .net core generation) 
- Was impressed by [munch-store](https://github.com/dmunch/munch-store) implementation. Tried to find alternative solutions, but it was really hard.
- Implemented model in yml with code generation (used [swagger-gen-aspnetcore](https://github.com/dmunch/swagger-gen-aspnetcore)). Compared with the last version of swagger-codegen for .net core 2, swagger-gen-aspnetcore looks better (async, abstract classes).
- Added build on Travis.
- Included [automapper](http://automapper.org/), had difficulties with Mapper in unit tests. Started to work stable only after singleton mapper implementation.
- In memory repositories for items and orders.
- Unit tests.
- Model form API and Doman project merged into one.

All end points finished

TO DO:

- Reiew and clean up client and example, add comments
- Add unit tests for all end points
- More tests.. :)
- Refactor to use HAL (api & client)
- Spend time on github learning by great examples
