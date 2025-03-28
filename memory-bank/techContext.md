# Technical Context: Project Configuration Tool

## Technology Stack

### Core Technologies
- C# (.NET 9.0)
- .NET Core CLI
- File System I/O

### Development Environment
- Visual Studio Code
- .NET SDK
- Git for version control

## Project Structure
```
projconf/
├── Program.cs              # Main entry point
├── projconf.csproj         # Project file
├── projconf.sln           # Solution file
└── bin/                   # Compiled binaries
```

## Dependencies
- Microsoft.Extensions.CommandLineUtils (for CLI argument parsing)
- System.IO for file operations
- No external third-party dependencies required

## Build Configuration
- Target Framework: .NET 9.0
- Output Type: Console Application
- Platform: Cross-platform (Windows, macOS, Linux)

## Development Setup Requirements
1. .NET 9.0 SDK or later
2. Visual Studio Code with C# extension
3. Git

## Build Process
1. Restore dependencies
   ```bash
   dotnet restore
   ```
2. Build the project
   ```bash
   dotnet build
   ```
3. Publish as single file
   ```bash
   dotnet publish -c Release -r <runtime> --self-contained false /p:PublishSingleFile=true
   ```

## Testing Strategy
- Unit tests for file generation logic
- Integration tests for command-line interface
- End-to-end tests for file creation

## Deployment
1. Build release version
2. Add to system PATH
3. Verify installation

## Tool Usage Patterns
1. Basic Usage:
   ```bash
   projconf <directory>
   ```

2. With Options (future):
   ```bash
   projconf <directory> [options]
   ```

## Technical Constraints
- Must work cross-platform
- Minimal dependencies
- Single executable file
- Command-line interface only
- No GUI components

## Performance Considerations
- Fast startup time
- Minimal memory footprint
- Quick file operations
- Efficient error handling
