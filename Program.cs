using System;
using System.IO;

namespace ProjConf
{
    public interface IFileGenerator
    {
        void Generate(string path);
    }

    public class ClineruleGenerator : IFileGenerator
    {
        public void Generate(string path)
        {
            var content = @"# Cline's Memory Bank

I am Cline, an expert software engineer with a unique characteristic: my memory resets completely between sessions. This isn't a limitation - it's what drives me to maintain perfect documentation. After each reset, I rely ENTIRELY on my Memory Bank to understand the project and continue work effectively. I MUST read ALL memory bank files at the start of EVERY task - this is not optional.

## Memory Bank Structure

The Memory Bank consists of core files and optional context files, all in Markdown format.

### Core Files (Required)
1. `projectbrief.md`
   - Foundation document that shapes all other files
   - Created at project start if it doesn't exist
   - Defines core requirements and goals
   - Source of truth for project scope

2. `productContext.md`
   - Why this project exists
   - Problems it solves
   - How it should work
   - User experience goals

3. `activeContext.md`
   - Current work focus
   - Recent changes
   - Next steps
   - Active decisions and considerations
   - Important patterns and preferences
   - Learnings and project insights

4. `systemPatterns.md`
   - System architecture
   - Key technical decisions
   - Design patterns in use
   - Component relationships
   - Critical implementation paths

5. `techContext.md`
   - Technologies used
   - Development setup
   - Technical constraints
   - Dependencies
   - Tool usage patterns

6. `progress.md`
   - What works
   - What's left to build
   - Current status
   - Known issues
   - Evolution of project decisions";

            File.WriteAllText(Path.Combine(path, ".clinerules"), content);
        }
    }

    public class ReadmeGenerator : IFileGenerator
    {
        public void Generate(string path)
        {
            var content = @"# Project Name

## Description
Brief description of the project.

## Setup
Instructions for setting up the project.

## Usage
How to use the project.

## Contributing
Guidelines for contributing to the project.

## License
Project license information.";

            File.WriteAllText(Path.Combine(path, "readme.md"), content);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                if (args.Length != 1)
                {
                    Console.WriteLine("Usage: projconf <directory>");
                    Console.WriteLine("Example: projconf myproject");
                    return;
                }

                string projectPath = args[0];

                // Convert relative path to absolute path
                if (!Path.IsPathRooted(projectPath))
                {
                    projectPath = Path.GetFullPath(projectPath);
                }

                // Create directory if it doesn't exist
                if (!Directory.Exists(projectPath))
                {
                    Directory.CreateDirectory(projectPath);
                }

                // Initialize generators
                var generators = new IFileGenerator[]
                {
                    new ClineruleGenerator(),
                    new ReadmeGenerator()
                };

                // Generate files
                foreach (var generator in generators)
                {
                    generator.Generate(projectPath);
                }

                Console.WriteLine($"Successfully initialized project in: {projectPath}");
                Console.WriteLine("Created files:");
                Console.WriteLine("  - .clinerules");
                Console.WriteLine("  - readme.md");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"Error: {ex.Message}");
                Console.Error.WriteLine("Try using an absolute path or check directory permissions.");
                Console.ResetColor();
                Environment.Exit(1);
            }
        }
    }
}
