# Avro.Os.Identity

Operating system detection and identity functionality for the Avro ecosystem. Determines if the current OS is macOS, Linux, or Windows.

## Overview

This package provides concrete implementations for operating system detection using .NET's `RuntimeInformation` APIs. It implements the interfaces defined in `Avro.Os.Abstractions`.

## Features

- Detect current operating system (macOS, Linux, Windows)
- Get detailed OS information (version, architecture, 64-bit detection)
- Both static and instance-based APIs
- Dependency injection support

## Installation

```bash
dotnet add package Avro.Os.Identity
```

## Usage

### Static API (Quick Access)

```csharp
using Avro.Os.Identity;

// Get OS type
var osType = OsIdentity.GetOperatingSystemType();
var osName = OsIdentity.GetOperatingSystemString();

// Check specific OS
if (OsIdentity.IsMacOS())
{
    Console.WriteLine("Running on macOS");
}
else if (OsIdentity.IsLinux())
{
    Console.WriteLine("Running on Linux");
}
else if (OsIdentity.IsWindows())
{
    Console.WriteLine("Running on Windows");
}
```

### Instance-Based API (Dependency Injection)

```csharp
using Avro.Os.Abstractions;
using Avro.Os.Identity;

// Register in DI container
services.AddSingleton<IOperatingSystemDetector, OperatingSystemDetector>();
services.AddSingleton<IOperatingSystemInfo, OperatingSystemInfo>();

// Use in your services
public class MyService
{
    private readonly IOperatingSystemDetector _osDetector;
    
    public MyService(IOperatingSystemDetector osDetector)
    {
        _osDetector = osDetector;
    }
    
    public void DoSomething()
    {
        if (_osDetector.IsMacOS())
        {
            // macOS-specific logic
        }
    }
}
```

### Detailed OS Information

```csharp
using Avro.Os.Abstractions;
using Avro.Os.Identity;

IOperatingSystemInfo osInfo = new OperatingSystemInfo();

Console.WriteLine($"OS: {osInfo.Name}");
Console.WriteLine($"Version: {osInfo.Version}");
Console.WriteLine($"Architecture: {osInfo.Architecture}");
Console.WriteLine($"64-bit: {osInfo.Is64Bit}");
```

## Dependencies

- `Avro.Os.Abstractions` - Core abstractions and interfaces
- .NET 10.0

## License

MIT License