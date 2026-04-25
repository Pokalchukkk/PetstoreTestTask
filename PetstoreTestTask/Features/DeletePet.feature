Feature: Delete Pet
  As an API consumer
  I want to DELETE a pet from the store
  So that it is no longer available

  Scenario: Delete an existing pet and receive a successful response
    Given a pet with name "DeletableDog" and status "available" exists in the store
    When I send a DELETE request for that pet
    Then the response status code should be 200

  Scenario: Delete a pet and verify it can no longer be retrieved
    Given a pet with name "GoneDog" and status "available" exists in the store
    When I send a DELETE request for that pet
    And I send a GET request for that pet
    Then the response status code should be 404

  Scenario: Attempt to delete a pet that does not exist
    When I send a DELETE request for pet with id 999999902
    Then the response status code should be 404

  Scenario: Deleting the same pet twice returns 404 on the second attempt
    Given a pet with name "IdempotentDog" and status "available" exists in the store
    When I send a DELETE request for that pet
    Then the response status code should be 200
    When I send a DELETE request for that pet
    Then the response status code should be 404

  Scenario: Deleting one pet does not affect another independently created pet
    Given a pet labeled "KeepMe" with name "KeeperDog" and status "available" exists in the store
    And a pet labeled "DeleteMe" with name "GoneDog2" and status "available" exists in the store
    When I send a DELETE request for the pet labeled "DeleteMe"
    Then the response status code should be 200
    When I send a GET request for the pet labeled "KeepMe"
    Then the response status code should be 200
    And the response should contain a pet with name "KeeperDog"

  Scenario: A new pet can be created after another pet with the same name is deleted
    Given a pet with name "PhoenixDog" and status "available" exists in the store
    When I send a DELETE request for that pet
    Then the response status code should be 200
    Given a minimal pet payload with name "PhoenixDog" and status "available"
    When I send a POST request to create the pet
    Then the response status code should be 200
    And the response should contain a pet with name "PhoenixDog"
    And the response should contain a non-zero pet id
