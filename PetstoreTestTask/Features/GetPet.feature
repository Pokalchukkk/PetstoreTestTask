Feature: Get Pet by ID
  As an API consumer
  I want to retrieve a pet by its ID
  So that I can read its current state

  Scenario: Retrieve an existing pet and verify the data matches what was created
    Given a pet with name "GetableDog" and status "available" exists in the store
    When I send a GET request for that pet
    Then the response status code should be 200
    And the response should contain a pet with name "GetableDog"
    And the response should contain pet status "available"

  Scenario: Attempt to retrieve a pet that does not exist
    When I send a GET request for pet with id 999999901
    Then the response status code should be 404

  Scenario: Retrieved pet contains all fields that were set at creation
    Given a full pet payload with name "RoundTripDog", category "Dogs", status "pending", and tags "vaccinated" and "trained"
    When I send a POST request to create the pet
    And I send a GET request for that pet
    Then the response status code should be 200
    And the response should contain a pet with name "RoundTripDog"
    And the response should contain pet status "pending"
    And the response should contain category name "Dogs"
    And the response should contain tag "vaccinated"
    And the response should contain tag "trained"

  Scenario: 404 response for a non-existing pet contains an error body with type and message fields
    When I send a GET request for pet with id 999999901
    Then the response status code should be 404
    And the response body should contain field "type"
    And the response body should contain field "message"
