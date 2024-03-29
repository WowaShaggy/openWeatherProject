﻿Feature: OpenWeatherTesting

@forecast
Scenario: Get Forecast
	When I gonna get "forecast"
	And I add "id" with value "524901"
	And I add my AppId
	And I send GET request
	Then Cod should be equal 200
	And Id should be equal 524901
	And City name should be equal "Moscow"


@weather
Scenario: Get Weather
	When I gonna get "weather"
	And I add "q" with value "London"
	And I add my AppId
	And I send GET request
	Then Cod should be equal 200
	And Name should be equal "London"
	And MainTemp should not be equal empty

@stations
Scenario: Put Station
	Given I gonna get station "3.0" request
	And I add id "5d3afec76c634e000131c036"
	And I add my AppId
	And I send GET request with id
	And Name should be equal "San Francisco Test Station"
	And Longitude should be equal -122.43
	And Latitude should be equal 37.76
	And Altitude should be equal 150
	When I send PUT request
	Then Name should not be equal "San Francisco Test Station"
	And Longitude should not be equal -122.43
	And Latitude should not be equal 37.76
	And Altitude should not be equal 150


@stations
Scenario: Post and Delete Station
	Given I gonna get station "3.0" request
	And I add my AppId
	And I send POST request
	And Name should be equal "Brest"
	And Altitude should be equal 150
	And Id should not be empty
	When I send DELETE request by ID
	Then Status codeare is NoContent
	And Content is empty
