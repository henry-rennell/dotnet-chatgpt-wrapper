# Running FunctionApp Locally

## Prerequisites

- [.NET SDK 9.0.x](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) 
- [Azure Functions Core Tools (v4+)](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- [Node.js & npm](https://nodejs.org/) 
- [Git](https://git-scm.com/)

## Getting Started

### 1. Clone the Repository
##### Run
```
git clone https://github.com/henry-rennell/dotnet-chatgpt-wrapper cd dotnet-chatgpt-wrapper/FunctionApp
```

### 2. Install Dependencies
Ensure you have the correct .NET SDK:
##### Run
```
dotnet --version
```
Should output: '9.0.x'

If necessary, install [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local):
##### Run
```
npm install -g azure-functions-core-tools@4 --unsafe-perm true
```

### 3. Build the Project
##### Run
```
dotnet clean
dotnet build
```

### 4. Run The Functions Locally
##### Run
```
func host start
```