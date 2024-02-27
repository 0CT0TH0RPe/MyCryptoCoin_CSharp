# MyCryptoCoin API Documentation

The MyCryptoCoin project provides a comprehensive API that allows external clients to interact with the blockchain network. This documentation outlines the available endpoints and their functionalities.

## Base URL

The base URL for accessing the MyCryptoCoin API is:

```
https://api.mycryptocoin.com
```

## Endpoints

### 1. Blockchain

#### GET /api/blockchain

**Description**: Retrieve the information about the blockchain.

**Response**:
```json
{
  "chain": [
    {
      "index": 0,
      "previousHash": "0000000000000000000000000000000000000000",
      "timestamp": "2023-08-03T12:00:00Z",
      "data": "Genesis Block",
      "hash": "93b9f74f372b3cc45d5d4a3d8f4f3186c0c3b8cd029bc3f15a12f46be30d2815"
    },
    {
      "index": 1,
      "previousHash": "93b9f74f372b3cc45d5d4a3d8f4f3186c0c3b8cd029bc3f15a12f46be30d2815",
      "timestamp": "2023-08-03T12:05:00Z",
      "data": "Transaction Data",
      "hash": "6b7b7890c8e11410d44a693a94d5d7ee98d7f14cc6ecb93ca3be8e3a831c52f9"
    },
    // More blocks...
  ],
  "length": 2
}
```

#### POST /api/blockchain/transaction

**Description**: Create a new transaction on the blockchain.

**Request**:
```json
{
  "sender": "sender_address",
  "recipient": "recipient_address",
  "amount": 1.5
}
```

**Response**:
```json
{
  "message": "Transaction added to the next block successfully."
}
```

### 2. Transaction

#### GET /api/transaction/{transactionId}

**Description**: Retrieve information about a specific transaction.

**Response**:
```json
{
  "transactionId": "transaction_id",
  "timestamp": "2023-08-03T12:10:00Z",
  "sender": "sender_address",
  "recipient": "recipient_address",
  "amount": 1.5,
  "status": "confirmed"
}
```

### 3. Consensus Protocol

#### GET /api/consensus/algorithm

**Description**: Retrieve the current consensus algorithm used by the blockchain network.

**Response**:
```json
{
  "algorithm": "Proof of Stake"
}
```

### 4. User Profile

#### GET /api/user/profile/{userId}

**Description**: Retrieve the profile information of a specific user.

**Response**:
```json
{
  "userId": "user_id",
  "username": "user123",
  "email": "user123@email.com",
  "registrationDate": "2023-08-03T12:15:00Z"
}
```

#### POST /api/user/profile

**Description**: Create a new user profile.

**Request**:
```json
{
  "username": "new_user",
  "email": "new_user@email.com"
}
```

**Response**:
```json
{
  "userId": "new_user_id",
  "username": "new_user",
  "email": "new_user@email.com",
  "registrationDate": "2023-08-03T12:20:00Z"
}
```

### 5. Trading Bot

#### GET /api/trading-bot/{botId}

**Description**: Retrieve information about a specific trading bot.

**Response**:
```json
{
  "botId": "bot_id",
  "name": "BTC Trader",
  "strategy": "Moving Average Crossover",
  "status": "active"
}
```

#### POST /api/trading-bot

**Description**: Create a new trading bot.

**Request**:
```json
{
  "name": "New Bot",
  "strategy": "RSI Divergence"
}
```

**Response**:
```json
{
  "botId": "new_bot_id",
  "name": "New Bot",
  "strategy": "RSI Divergence",
  "status": "active"
}
```

## Error Handling

If an error occurs during API requests, the response will include an error code and a message explaining the issue.

Example Error Response:
```json
{
  "error": "Invalid sender address.",
  "code": 400
}
```

## Conclusion

The MyCryptoCoin API provides a set of endpoints to interact with the blockchain network, create transactions, manage user profiles, and handle trading bots. By following this documentation, developers can effectively integrate their applications with the MyCryptoCoin blockchain platform and perform various operations seamlessly.