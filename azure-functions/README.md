# Azure Functions

Course - [Azure Functions Fundamentals by Mark Heath](https://app.pluralsight.com/library/courses/azure-functions-fundamentals/)

## Azure Functions Core Tools

- Install the Azure Function Core Tools for local development ([Work with Azure Functions Core Tools](https://learn.microsoft.com/azure/azure-functions/functions-run-local)). Use the `func version` in a PowerShell window to check if the Azure Core tools are installed.
- Use the `func init` command to create an Azure function app.
- Use the `func new` to add a function to the function app.
- Use the `func start` command to run the functions host using the command line.

## Azure Storage Emulator

Use the [Azure Storage Emulator](https://learn.microsoft.com/azure/storage/common/storage-use-emulator) for development and testing.

_**Note:**_

The Azure Storage Emulator is now deprecated. Microsoft recommends that you use the [Azurite emulator](https://learn.microsoft.com/azure/storage/common/storage-use-azurite) for local development with Azure Storage.

## How to test locally

- Start the function. 
  - You can start the function using the IDE or 
  - Use the `func start` command in a PowerShell window to start the function using the Azure Functions Core Tools.
- You can call the function using a tool like Postman or Curl. You can also use the `OnPaymentReceived` HTTP request in the DemoApp project using Rider. 