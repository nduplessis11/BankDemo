# Current Account API

## Overview

This API allows for the initiation of a current account through a single operation. The endpoint is designed to be straightforward and operation-based, without adhering to RESTful resource conventions.

## API Endpoints

### POST `/CurrentAccount/Initiate`

#### Description
Initiates the process of creating a new current account. The request includes the necessary details such as product reference, customer information, and account details.

#### Request Body

The request body must include the following fields:

- **ProductInstanceReference**:
  - `CurrentAccountAgreement` (string): Reference to the current account agreement.
  
- **CurrentAccountNumber**:
  - `AccountIdentificationType` (string): Type of account identification (e.g., BBAN, IBAN).
  - `AccountIdentification`:
    - `IdentifierValue` (string): The actual identification number.
  
- **CustomerReference**:
  - `PartyReference`:
    - `PartyName` (string): Name of the account holder.
    - `PartyIdentification`:
      - `PartyIdentificationType` (string): Type of identification (e.g., Taxidentificationnumber).
      - `PartyIdentification` (string): The identification number.

- **AccountType** (string): Type of account being created (e.g., DebitAccount, CreditAccount).

- **AccountCurrency**:
  - `AccountCurrencyType` (string): Type of currency (e.g., BaseCurrency).
  - `Currencycode` (string): Currency code (e.g., USD).

#### Example Request

```json
{
  "ProductInstanceReference": {
    "CurrentAccountAgreement": "AG123456789"
  },
  "CurrentAccountNumber": {
    "AccountIdentificationType": "BBAN",
    "AccountIdentification": {
      "IdentifierValue": {
        "Value": "123456789"
      }
    }
  },
  "CustomerReference": {
    "PartyReference": {
      "PartyName": {
        "Name": "John Doe"
      },
      "PartyIdentification": {
        "PartyIdentificationType": "Taxidentificationnumber",
        "PartyIdentification": "TAX123456789"
      }
    }
  },
  "AccountType": "DebitAccount",
  "AccountCurrency": {
    "AccountCurrencyType": "BaseCurrency",
    "AccountCurrency": {
      "Currencycode": "USD"
    }
  }
}
```
