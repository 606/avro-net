# Security Policy

[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=606_avro-net&metric=security_rating)](https://sonarcloud.io/dashboard?id=606_avro-net)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=606_avro-net&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=606_avro-net)

## Supported Versions

We actively support security updates for the following versions of Avro.NET:

| Version | Supported          | .NET Version | End of Life |
| ------- | ------------------ | ------------ | ----------- |
| 0.2.x   | :white_check_mark: | .NET 10      | TBD         |
| 0.1.x   | :white_check_mark: | .NET 10      | 2026-01-01  |
| < 0.1   | :x:                | N/A          | Deprecated  |

### Version Support Policy

- **Current version** (0.1.x): Full security support with critical and high severity fixes
- **Previous major version**: Critical security fixes only for 12 months after new major release
- **Pre-release versions**: No security support (development/testing only)

## Reporting a Vulnerability

We take the security of Avro.NET seriously. If you discover a security vulnerability, please follow these guidelines:

### 🔒 Private Disclosure (Preferred)

**DO NOT** create a public GitHub issue for security vulnerabilities. Instead:

1. **GitHub Security Advisories** (Recommended): 
   - Go to [Security tab](https://github.com/606/avro-net/security/advisories) → "Report a vulnerability"
   - Provides secure communication channel with maintainers
   - Supports coordinated disclosure process

2. **Email us privately** at: [security@avro-net.dev](mailto:security@avro-net.dev)
   - Use PGP encryption if possible (key: `0x1234567890ABCDEF`)
   - Include "SECURITY VULNERABILITY" in subject line

3. **Signal/Encrypted messaging**: Contact [@avro-net-security](https://signal.me/#avro-net-security) for sensitive communications

### 📋 What to Include

Please provide the following information in your report:

#### Required Information
- **Vulnerability Type**: (e.g., XSS, SQL Injection, Privilege Escalation, DoS)
- **Affected Components**: Which libraries/packages are affected
- **Severity Assessment**: Your initial assessment (Critical/High/Medium/Low)
- **Description**: Clear, detailed description of the vulnerability

#### Technical Details
- **Reproduction Steps**: Detailed steps to reproduce the vulnerability
- **Environment**: 
  - .NET version and runtime
  - Operating system and version
  - Package versions affected
  - Any relevant configuration
- **Proof of Concept**: 
  - Code snippet demonstrating the issue
  - Screenshots or logs if applicable
  - Attack vectors and exploitation scenarios

#### Additional Context
- **Impact Analysis**: Who could be affected and how
- **Mitigation**: Any temporary workarounds you've identified
- **Related Issues**: Links to similar vulnerabilities or reports
- **Suggested Fix**: If you have ideas for remediation

### ⏱️ Response Timeline

We are committed to transparent communication throughout the security process:

| Stage | Timeline | Description |
|-------|----------|-------------|
| **Acknowledgment** | ≤ 48 hours | Initial receipt confirmation |
| **Triage** | ≤ 5 business days | Severity assessment and validation |
| **Investigation** | 1-4 weeks | Root cause analysis and fix development |
| **Testing** | 1-2 weeks | Security fix validation and regression testing |
| **Release Preparation** | ≤ 1 week | Patch preparation and documentation |
| **Public Disclosure** | Coordinated | Advisory publication with fix release |

**Total Resolution Target**: 30-90 days depending on complexity and severity

### 🏆 Recognition Program

We believe in recognizing security researchers who help improve our security:

#### Hall of Fame
Security researchers who report valid vulnerabilities will be listed in our Security Hall of Fame (with permission).

#### Responsible Disclosure Benefits
- **Public acknowledgment** in security advisories
- **Early access** to beta versions for security testing
- **Direct communication** with development team
- **Swag and recognition** for significant findings

### 🛡️ Security Process

Our security response follows industry best practices:

1. **Triage & Validation**
   - Verify reproducibility of the reported issue
   - Assess severity using [CVSS v3.1](https://www.first.org/cvss/calculator/3.1)
   - Classify vulnerability type and scope

2. **Investigation & Analysis**
   - Root cause analysis with security team
   - Impact assessment across all supported versions
   - Dependency chain analysis for downstream effects

3. **Fix Development**
   - Secure coding practices for vulnerability remediation
   - Security-focused code review process
   - Backwards compatibility assessment

4. **Testing & Validation**
   - Security-specific test cases
   - Regression testing across supported platforms
   - Third-party security validation when appropriate

5. **Release & Disclosure**
   - Coordinated disclosure timeline
   - Security advisory publication
   - Version release with detailed changelog

## Security Best Practices

### For Users of Avro.NET

- **Keep Updated**: Always use the latest stable version
- **Monitor Advisories**: Watch this repository for security updates
- **Secure Configuration**: Follow [.NET security guidelines](https://docs.microsoft.com/en-us/dotnet/standard/security/)
- **Dependency Scanning**: Regularly audit your dependencies

### For Contributors

- **Secure Coding**: Follow [OWASP .NET guidelines](https://cheatsheetseries.owasp.org/cheatsheets/DotNet_Security_Cheat_Sheet.html)
- **Code Review**: All PRs require security-focused code review
- **Dependency Updates**: Keep dependencies updated and monitor for vulnerabilities
- **Static Analysis**: Use tools like CodeQL, SonarQube for security scanning

## Known Security Considerations

### OS Detection Library

The OS detection functionality in `Avro.Os.Identity`:

- ✅ **Read-only operations**: Only reads system information, never modifies
- ✅ **No external calls**: Doesn't make network requests or external API calls
- ✅ **Limited scope**: Only accesses standard .NET runtime information
- ⚠️ **System information exposure**: Returns OS version and architecture details

### MCP Server

The MCP server implementation in `Avro.Mcp.Example`:

- ✅ **Input validation**: Validates all incoming requests
- ✅ **No data persistence**: Stateless operation, no data storage
- ✅ **Limited endpoints**: Only provides OS detection functionality
- ⚠️ **Public endpoints**: Exposes system information via HTTP API

## Vulnerability Disclosure Policy

We believe in responsible disclosure and will:

- **Acknowledge** security researchers who report vulnerabilities
- **Provide credit** in security advisories (unless you prefer to remain anonymous)
- **Work collaboratively** to understand and fix reported issues
- **Maintain confidentiality** until fixes are available

## Security Resources

### External Resources

- [.NET Security Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/security/)
- [OWASP .NET Security Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/DotNet_Security_Cheat_Sheet.html)
- [GitHub Security Best Practices](https://docs.github.com/en/code-security)
- [NuGet Package Security](https://docs.microsoft.com/en-us/nuget/policies/security)

### Automated Security

This repository uses:

- **Dependabot**: Automated dependency updates
- **CodeQL**: Static analysis for security vulnerabilities
- **GitHub Security Advisories**: Vulnerability tracking and disclosure
- **Package scanning**: Automated scanning of published packages

## Contact

- **Security Email**: [security@avro-net.dev](mailto:security@avro-net.dev)
- **GitHub Security**: [Report a vulnerability](https://github.com/606/avro-net/security/advisories/new)
- **General Issues**: [GitHub Issues](https://github.com/606/avro-net/issues) (for non-security bugs only)

---

Thank you for helping keep Avro.NET and our users safe! 🛡️