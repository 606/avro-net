# Avro.Os.Abstractions

Abstractions and interfaces for operating system detection functionality in the Avro ecosystem.

## Overview

This package provides the core abstractions and interfaces for operating system detection. It defines contracts that can be implemented by various OS detection providers.

## Interfaces

### IOperatingSystemDetector

Provides functionality to detect the current operating system:

```csharp
public interface IOperatingSystemDetector
{
    OperatingSystemType GetOperatingSystemType();
    string GetOperatingSystemString();
    bool IsMacOS();
    bool IsLinux();
    bool IsWindows();
}
```

### IOperatingSystemInfo

Provides detailed information about the operating system:

```csharp
public interface IOperatingSystemInfo
{
    OperatingSystemType Type { get; }
    string Name { get; }
    string Version { get; }
    string Architecture { get; }
    bool Is64Bit { get; }
}
```

## Enums

### OperatingSystemType

Defines the supported operating system types:

- `Unknown`
- `MacOS`
- `Linux`
- `Windows`

## Installation

```bash
dotnet add package Avro.Os.Abstractions
```

## Usage

This package contains only abstractions. For concrete implementations, use `Avro.Os.Identity` or implement your own providers.

## License

MIT License