# Avro MCP Example

This is a simple Model Context Protocol (MCP) server implementation for .NET 10, that demonstrates integrating with the Avro OS Detection libraries.

## Features

- MCP server implementation using the Microsoft.AspNetCore.ModelContext.Protocol package
- Integration with Avro.Os.Identity for operating system detection
- Sample model that responds to OS detection queries
- Simple moderation implementation to filter sensitive requests

## Getting Started

### Prerequisites

- .NET 10 SDK (version 10.0.100-rc.1 or later)
- A compatible editor (like Visual Studio 2022 or VS Code)

### Running the Server

```bash
cd src/mcp/Avro.Mcp.Example
dotnet run
```

The server will start at:
- http://localhost:5258
- https://localhost:7258

## API Usage

The MCP server exposes standard MCP endpoints:

### Chat Completion

```bash
curl -X POST "http://localhost:5258/mcp/chat/completions" \
  -H "Content-Type: application/json" \
  -d '{
    "model": "avro-os-identifier",
    "messages": [
      {
        "role": "user",
        "content": [{"type": "text", "text": "Detect my operating system"}]
      }
    ]
  }'
```

### Moderation

```bash
curl -X POST "http://localhost:5258/mcp/moderations" \
  -H "Content-Type: application/json" \
  -d '{
    "model": "avro-os-identifier",
    "input": "Tell me about my OS"
  }'
```

## Model Details

The OS detector model responds to the following types of queries:
- OS detection
- Architecture information
- OS version information

## Configuration

Configuration options are available in `appsettings.json` and include:
- Logging levels
- Endpoint configuration
- MCP route prefix