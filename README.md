# Petstore API Test Framework

BDD-style API automation for the [Swagger Petstore](https://petstore.swagger.io/) public API, covering `POST /pet`, `GET /pet/{petId}`, and `DELETE /pet/{petId}`.

## Stack

| Concern | Tool |
|---|---|
| Language | C# (.NET 10) |
| Test runner | NUnit 4 |
| BDD layer | Reqnroll 3 (SpecFlow successor) |
| HTTP client | `System.Net.Http.HttpClient` |
| Serialization | `System.Text.Json` |
| Assertions | FluentAssertions 7 |

## How to run

```bash
dotnet test
```

Or with verbose BDD output:

```bash
dotnet test --logger "console;verbosity=normal"
```

## Project structure

```
PetstoreTestTask/
├── Api/
│   └── PetApiClient.cs          # All HTTP calls; returns typed ApiResponse<T>
├── Builders/
│   └── PetBuilder.cs            # Test data factory; ID generation lives here, not in models
├── Configuration/
│   └── ApiConfiguration.cs      # Base URL; swap for env var / config file if needed
├── Contexts/
│   └── PetScenarioContext.cs    # Per-scenario state; small and typed
├── Features/
│   ├── CreatePet.feature
│   ├── GetPet.feature
│   └── DeletePet.feature
├── Hooks/
│   └── CleanupHooks.cs          # AfterScenario teardown of created pets
├── Models/
│   ├── ApiResponse.cs           # HttpStatusCode + typed body + raw content
│   ├── Category.cs
│   ├── Pet.cs
│   └── Tag.cs
└── StepDefinitions/
    ├── CreatePetSteps.cs         # Given (payload setup) + When POST
    ├── DeletePetSteps.cs         # When DELETE
    ├── GetPetSteps.cs            # When GET
    ├── PetResponseAssertionSteps.cs  # All Then steps — reused across all features
    └── PetSetupSteps.cs          # Given "pet exists in store" — shared test setup
```