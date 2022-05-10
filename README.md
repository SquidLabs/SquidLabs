SquidLabs - Tentacles
===========
Tentacles and it's supporting libraries are an application framework for .NET.
It is licensed under Apache License Version 2. The framework is opiniated, but it is flexible enough to allow you to pick and choose components relevant to your solution.

#### Presentation
- [ ] React .Net + Api
- [ ] Asp.net Core MVC
- [ ] Service
- [ ] Console
- [ ] gRpc

#### Domain
- [x] Domain Objects (aggregate,entity,value)
- [x] Domain Object Unit Tests
- [ ] Domain Events
- [ ] Specification
- [x] DataStore and ClientFactory
- [x] EventStore
- [x] FileStore
#### Application
- [x] Repository
- [x] Repository Unit Tests
- [ ] Mapper for Repository domain types to DataStore
- [ ] Mapper tests
- [ ] ISearchableRepository (translate a specification into a datastore search)
- [ ] DataStore and ClientFactory
- [ ] CQRS (DI, gRpc, Rabbit?) Throttling?
#### Infrastructure
- [x] DataStore and ClientFactory
- [x] DataStore basic unit tests memory cache
- [x] DataStore basic unit tests file
- [ ] ClientFactory basic unit tests? memcache options? fileoptions?
- [x] DataStore Integration Tests
- [ ] DataStore Dependency Injection Tests
- [ ] ISearchable DataStore
- [ ] Domain Event Messaging  Infrastructure