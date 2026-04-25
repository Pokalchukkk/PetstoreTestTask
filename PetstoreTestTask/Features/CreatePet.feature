Feature: Create Pet
  As an API consumer
  I want to POST a new pet to the store
  So that it is registered and retrievable by its assigned ID

  Scenario: Create a pet with minimal required fields and verify the response contract
    Given a minimal pet payload with name "MinimalDog" and status "available"
    When I send a POST request to create the pet
    Then the response status code should be 200
    And the response should contain a pet with name "MinimalDog"
    And the response should contain pet status "available"
    And the response should contain a non-zero pet id

  Scenario: Create a pet with a full payload and verify all fields are echoed back
    Given a full pet payload with name "FullDog", category "Dogs", status "pending", and tags "vaccinated" and "trained"
    When I send a POST request to create the pet
    Then the response status code should be 200
    And the response should contain a pet with name "FullDog"
    And the response should contain pet status "pending"
    And the response should contain category name "Dogs"
    And the response should contain tag "vaccinated"
    And the response should contain tag "trained"

  Scenario: Create a pet with "sold" status and verify status is persisted
    Given a minimal pet payload with name "SoldPet" and status "sold"
    When I send a POST request to create the pet
    Then the response status code should be 200
    And the response should contain pet status "sold"

  Scenario: Create a pet with multiple photo URLs and verify they are echoed back in the response
    Given a pet payload with name "PhotoPet", status "available", and photo urls "http://img1.example.com" and "http://img2.example.com"
    When I send a POST request to create the pet
    Then the response status code should be 200
    And the response should contain a non-zero pet id
    And the response should contain photo url "http://img1.example.com"
    And the response should contain photo url "http://img2.example.com"

  Scenario: Two consecutive create requests receive different IDs
    Given a minimal pet payload with name "UniquePet1" and status "available"
    When I send a POST request to create the pet
    And I save the returned pet id as "firstId"
    Given a minimal pet payload with name "UniquePet2" and status "available"
    When I send a POST request to create the pet
    And I save the returned pet id as "secondId"
    Then the ids saved as "firstId" and "secondId" should be different

  Scenario: Server assigns a non-zero ID when zero is sent in the request body
    Given a pet payload with a zero id, name "ZeroIdPet", and status "available"
    When I send a POST request to create the pet
    Then the response status code should be 200
    And the response should contain a non-zero pet id
