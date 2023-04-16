# Purple Bank CDS Products API


## About

This is a simple implementation of the CDS API. It is a .NET 7 Web API that uses Cosmosdb. The API implements the GetProducts endpoint found here: https://consumerdatastandardsaustralia.github.io/standards/?examples#get-products

## Tech

- .NET 7
- Cosmosdb
- Swagger
- AppInsights
- Serilog
- xUnit

## Running the application

Make sure you have the following installed:

- .NET 7
- Cosmosdb Emulator

To run the application:

- Make sure the Cosmosdb Emulator is running.
- Make sure you are in the `src` directory.

Run the following command:
`
dotnet run
`

## Running the tests

Make sure you have the following installed:
- .NET 7
- Cosmosdb Emulator

To run the tests:

- Make sure the Cosmosdb Emulator is running.
- Make sure you are in the `src` directory.

Run the following command:
`
dotnet test
`

## Project Structure

The project is split into 5 main projects:

- BigPurpleBank.Api.Product.Web
  - This is the main web project. It contains the controllers and the swagger configuration.
- BigPurpleBank.Api.Product.Data
  - This project contains the Cosmosdb configuration and the Cosmosdb repository.
- BigPurpleBank.Api.Product.Model
  - This project contains the models used in the application.
- BigPurpleBank.Api.Product.Service
  - This project contains the services used in the application.
- BigPurpleBank.Api.Product.Common
  - This project contains the common code used in the application. This should be separated out into a nuget package. Which would be used by all the other projects.

## Response Model

### Generic Response Model
The response model is a custom model that is used to return the response. This is to allow for the links and meta to be added to the response.
Data is a generic type that can be used to return any type of data.

```
{
  "data" : []
  "links" : {
    "self" : "",
    "first" : "",
    "prev" : "",
    "next" : "",
    "last" : ""
  },
    "meta" : {
        "totalRecords" : 0,
        "totalPages" : 0
    }
}
```

### Error Response Model

The error response model is a custom model that is used to return the error response. This is to allow for the error to be returned in the correct format.

```
{
  "errors" : [
    {
      "code" : "",
      "title" : "",
      "detail" : "",
      "meta" : {}
    }
  ]
}
```

## Endpoints
### GET `/v3/Banking/Products`

#### Headers

- `x-v" : "3"`

#### Query Parameters
This endpoint returns a list of products. The endpoint supports the following query parameters:

- effective
- updated-since
- brand
- updated-since
- page

#### Response

#### 200
````
{
  "data": {
    "productId": "string",
    "effectiveFrom": "2023-04-16T05:04:18.391Z",
    "effectiveTo": "2023-04-16T05:04:18.391Z",
    "lastUpdated": "2023-04-16T05:04:18.391Z",
    "productCategory": 0,
    "name": "string",
    "description": "string",
    "brand": "string",
    "brandName": "string",
    "applicationUri": "string",
    "isTailored": true,
    "additionalInformation": {
      "overviewUri": "string",
      "termsUri": "string",
      "eligibilityUri": "string",
      "feesAndPricingUri": "string",
      "bundleUri": "string",
      "additionalOverviewUris": [
        {
          "description": "string",
          "additionalInfoUri": "string"
        }
      ],
      "additionalTermsUris": [
        {
          "description": "string",
          "additionalInfoUri": "string"
        }
      ],
      "additionalEligibilityUris": [
        {
          "description": "string",
          "additionalInfoUri": "string"
        }
      ],
      "additionalFeesAndPricingUris": [
        {
          "description": "string",
          "additionalInfoUri": "string"
        }
      ],
      "additionalBundleUris": [
        {
          "description": "string",
          "additionalInfoUri": "string"
        }
      ]
    },
    "cardArt": [
      {
        "title": "string",
        "imageUri": "string"
      }
    ],
    "title": "string",
    "imageUri": "string"
  },
  "links": {
    "totalRecords": 0,
    "totalPages": 0
  },
  "meta": {
    "self": "string",
    "first": "string",
    "prev": "string",
    "next": "string",
    "last": "string"
  }
}
````
#### 400

```
{
  "errors": [
    {
      "code": "urn:au-cds:error:cds-all:Field/InvalidDateTime",
      "title": "Invalid DateTime",
      "detail": "The field is not a valid DateTime",
      "meta": {}
    }
  ]
}
```

#### 406

```
{
  "errors": [
    {
      "code": "urn:au-cds:error:cds-all:Header/InvalidVersion",
      "title": "Invalid Version",
      "detail": "The version is not valid",
      "meta": {}
    }
  ]
}
```

#### 422

```
{
  "errors": [
    {
      "code": "urn:au-cds:error:cds-all:Field/InvalidPage",
      "title": "Invalid Page",
      "detail": "The page is not valid",
      "meta": {}
    }
  ]
}
```

#### 500

```
{
  "errors": [
    {
      "code": "urn:au-cds:error:cds-all:InternalError",
      "title": "Internal Error",
      "detail": "An internal error has occurred",
      "meta": {}
    }
  ]
}
```

## Improvements

Below are a list of improvements that could be made to the application. I ran out of time to implement these.

- Update Version to use Asp.Versioning.Mvc
- Custom DateTime/ DateTimeOffSett validation Service.
  - At the moment all validation shows a response of `InValid Field` even if the field is a Date. This does not match the requirements to have a specific code for `urn:au-cds:error:cds-all:    Field/InvalidDateTime`.
- Populate Links in response with the correct links.
- Populate Meta in response with the correct record numbers
- Validation for max page is not implemented `urn:au-cds:error:cds-all:  Field/InvalidPage`
- Cleanup data generated in integrations test
- Compile a Docker-compose to bring up a docker container for running the test and Cosmosdb Emulator.