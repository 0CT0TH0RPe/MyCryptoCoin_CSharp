// Transaction.cs
using System;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Ipfs;
using Ipfs.Http;
using MyCryptoCoin.Blockchain.Services;
using MyCryptoCoin.Blockchain.Core.Models;
using MyCryptoCoin.Blockchain.Core.Blocks;
using MyCryptoCoin.Blockchain.Core.Contracts;
using MyCryptoCoin.Consensus;

namespace MyCryptoCoinCore.Blockchain.Core.Models
{
    // Implementation of the ITransaction interface representing a blockchain transaction
    public class Transaction : ITransaction
    {
        public string TransactionId { get; set; }
        public string SenderAddress { get; set; }
        public string RecipientAddress { get; set; }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string Data { get; set; }
        public string TransactionHash { get; set; }
        public string Signature { get; set; }
        public string ErrorMessage { get; set; }
        public TransactionStatus Status { get; set; }


        public Transaction(string sender, string receiver, decimal amount, DateTime Timestamp)
        {
            Sender = sender;
            Receiver = receiver;
            Amount = amount;
            Timestamp = DateTime.UtcNow;
            TransactionId = Guid.NewGuid().ToString();
        }

        public string CalculateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string data = $"{TransactionId}-{Sender}-{Recipient}-{Amount}-{Timestamp}";
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public override string ToString()
        {
            return $"Transaction ID: {TransactionId}\nSender: {Sender}\nReceiver: {Receiver}\nAmount: {Amount}\nTimestamp: {Timestamp}";
        }

        public enum TransactionStatus
        {
            Successful,
            Failed
        }
    }
}

/*
The `Transaction.cs` file contains the `Transaction` class, which is an implementation of the `ITransaction` interface in the MyCryptoCoin project. This class represents a blockchain transaction and provides concrete implementations for the properties defined in the `ITransaction` interface. The `Transaction` class encapsulates all the necessary information and functionalities related to a transaction in the blockchain.

**Transaction Class:**

The `Transaction` class defines a single blockchain transaction, representing the transfer of cryptocurrency from one wallet to another. It includes properties to store information such as the sender's address, recipient's address, amount transferred, timestamp, data, transaction hash, and digital signature.

**Properties of the Transaction Class:**

1. **TransactionId (string):**
   - Description: A unique identifier for the transaction. It helps uniquely identify each transaction and avoid duplicate entries.

2. **SenderAddress (string):**
   - Description: The address of the sender (originator) of the transaction. It represents the wallet or account from which the cryptocurrency is being transferred.

3. **RecipientAddress (string):**
   - Description: The address of the recipient (receiver) of the transaction. It represents the wallet or account that receives the transferred cryptocurrency.

4. **Amount (double):**
   - Description: The amount of cryptocurrency being transferred in the transaction. It indicates the value of the transaction in the native cryptocurrency of the blockchain.

5. **Timestamp (DateTime):**
   - Description: The timestamp of when the transaction was created or added to the blockchain. It helps order the transactions chronologically.

6. **Data (string):**
   - Description: Additional data or message included in the transaction, if any. It can be used to attach a memo or note to the transaction for specific purposes.

7. **TransactionHash (string):**
   - Description: The hash of the transaction data. This hash is used to uniquely identify the transaction and is crucial for verifying the integrity of the data during validation.

8. **Signature (string):**
   - Description: The digital signature of the transaction data. The sender signs the transaction using their private key to prove ownership and prevent tampering. The signature is used during verification to ensure the authenticity of the transaction.

**Additional Notes:**

- The `Transaction` class provides a concrete implementation of the `ITransaction` interface, which defines the structure and properties of a blockchain transaction.

- Transactions are crucial components of any blockchain, as they record the movement of cryptocurrency between wallet addresses and enable various actions on the blockchain, such as the execution of smart contracts and other operations.

- In a real-world implementation, the `Transaction` class would be used in conjunction with other blockchain components, such as wallets, mining processes, and smart contracts, to facilitate secure and efficient transactions on the blockchain network.

- The `Transaction` class's implementation of the `ITransaction` interface ensures that all instances of the class conform to a standard format, allowing for seamless interaction and data sharing within the blockchain system.

- For each new transaction recorded on the blockchain, a new instance of the `Transaction` class would be created with unique properties corresponding to that specific transaction's details.
*/