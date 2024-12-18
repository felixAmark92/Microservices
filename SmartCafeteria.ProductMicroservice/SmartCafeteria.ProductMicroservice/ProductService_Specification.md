# Product Service: Manages daily/weekly menus and availability

## Entities

### Product

| Property Name | Data Type | Description                                         |
| ------------- | --------- | --------------------------------------------------- |
| Id            | int       | Product id                                          |
| Name          | string    | Name of the product                                 |
| Price         | double    | Price of the product                                |
| IsAvailable   | bool      | Status of the product, is available or out of stock |

## Endpoints

| Path             | Method | Request | Response  | ResponseCodes |
| ---------------- | ------ | ------- | --------- | ------------- |
| "/products"      | GET    | NONE    | Product[] | 200           |
| "/products/{id}" | GET    | int id  | Product   | 200, 404      |
| "/products"      | POST   | Product | NONE      | 200, 400      |
| "/products/{id}" | PUT    | int id  | NONE      | 200, 404      |
| "/products/{id}" | DELETE | int id  | NONE      | 200, 404      |
