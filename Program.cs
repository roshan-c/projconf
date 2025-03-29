﻿using System;
using System.IO;
using System.Collections.Generic;

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

    public class GitignoreGenerator : IFileGenerator
    {
        public void Generate(string path)
        {
            var content = @"# Visual Studio files
.vs/
*.user
*.userosscache
*.sln.docstates

# Build results
[Dd]ebug/
[Rr]elease/
x64/
x86/
[Bb]in/
[Oo]bj/
[Ll]og/
[Ll]ogs/

# Python
__pycache__/
*.py[cod]
*$py.class
*.so
.Python
env/
build/
develop-eggs/
dist/
downloads/
eggs/
.eggs/
lib/
lib64/
parts/
sdist/
var/
wheels/
*.egg-info/
.installed.cfg
*.egg
venv/
ENV/

# Node.js
node_modules/
npm-debug.log
yarn-debug.log
yarn-error.log
.env
.env.test

# IDEs and editors
.idea/
.vscode/
*.swp
*.swo
*~
*.iml
.project
.classpath
.settings/
.vs/

# OS generated files
.DS_Store
.DS_Store?
._*
.Spotlight-V100
.Trashes
ehthumbs.db
Thumbs.db";

            File.WriteAllText(Path.Combine(path, ".gitignore"), content);
        }
    }

    public class StackSpecificGenerator : IFileGenerator
    {
        private readonly string _stack;
        
        public StackSpecificGenerator(string stack)
        {
            _stack = stack;
        }

        public void Generate(string path)
        {
            switch (_stack)
            {
                case "cs":
                    GenerateCSharpFiles(path);
                    break;
                case "html":
                    GenerateHtmlFiles(path);
                    break;
                case "python":
                    GeneratePythonFiles(path);
                    break;
                case "node":
                    GenerateNodeFiles(path);
                    break;
            }
        }

        private void GenerateCSharpFiles(string path)
        {
            try
            {
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = "new console",
                    WorkingDirectory = path,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = System.Diagnostics.Process.Start(startInfo);
                process?.WaitForExit();

                if (process?.ExitCode != 0)
                {
                    throw new Exception("Failed to create C# console application");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating C# project: {ex.Message}");
            }
        }

        private void GenerateNodeFiles(string path)
        {
            try
            {
                var packageJson = @"{
  ""name"": ""project-name"",
  ""version"": ""1.0.0"",
  ""description"": """",
  ""main"": ""index.js"",
  ""scripts"": {
    ""start"": ""node index.js"",
    ""test"": ""echo \""Error: no test specified\"" && exit 1""
  },
  ""keywords"": [],
  ""author"": """",
  ""license"": ""ISC""
}";

                var indexJs = @"console.log('Hello, Node.js!');";

                File.WriteAllText(Path.Combine(path, "package.json"), packageJson);
                File.WriteAllText(Path.Combine(path, "index.js"), indexJs);

                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "npm",
                    Arguments = "install",
                    WorkingDirectory = path,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = System.Diagnostics.Process.Start(startInfo);
                process?.WaitForExit();

                if (process?.ExitCode != 0)
                {
                    throw new Exception("Failed to initialize npm project");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating Node.js project: {ex.Message}");
            }
        }

        private void GenerateHtmlFiles(string path)
        {
            var htmlContent = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Project Title</title>
    <link rel=""stylesheet"" href=""style.css"">
</head>
<body>
    <h1>Welcome to Project Title</h1>
    
    <script src=""script.js""></script>
</body>
</html>";

            var cssContent = @"/* Reset default styles */
* {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
}

body {
    font-family: Arial, sans-serif;
    line-height: 1.6;
    padding: 20px;
}

h1 {
    color: #333;
    margin-bottom: 20px;
}";

            var jsContent = @"// Add your JavaScript code here
document.addEventListener('DOMContentLoaded', () => {
    console.log('Website loaded successfully!');
});";

            File.WriteAllText(Path.Combine(path, "index.html"), htmlContent);
            File.WriteAllText(Path.Combine(path, "style.css"), cssContent);
            File.WriteAllText(Path.Combine(path, "script.js"), jsContent);
        }

        private void GeneratePythonFiles(string path)
        {
            var requirementsContent = @"# Add your Python dependencies here
# Example:
# flask==2.0.1
# requests==2.26.0
# python-dotenv==0.19.0";

            File.WriteAllText(Path.Combine(path, "requirements.txt"), requirementsContent);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0 || (args.Length > 1 && args[1] == "--stack" && args.Length == 2))
                {
                    Console.WriteLine("Usage: projconf <directory> [--stack <stack>]");
                    Console.WriteLine("Example: projconf myproject --stack cs");
                    Console.WriteLine("\nAvailable stacks:");
                    Console.WriteLine("  cs        - C# project");
                    Console.WriteLine("  html      - HTML/CSS/JS project");
                    Console.WriteLine("  python    - Python project");
                    Console.WriteLine("  node      - Node.js project");
                    return;
                }

                string projectPath = args[0];
                string? stack = null;

                // Parse --stack parameter
                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i] == "--stack" && i + 1 < args.Length)
                    {
                        stack = args[i + 1].ToLower();
                        if (stack != "cs" && stack != "html" && stack != "python" && stack != "node")
                        {
                            Console.WriteLine($"Invalid stack: {stack}");
                            Console.WriteLine("Available stacks: cs, html, python, node");
                            return;
                        }
                        break;
                    }
                }

                // Handle special ".." case to use current directory
                if (projectPath == "..")
                {
                    projectPath = Directory.GetCurrentDirectory();
                }
                else
                {
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
                }

                // Initialize generators
                var generators = new List<IFileGenerator>
                {
                    new ClineruleGenerator(),
                    new ReadmeGenerator(),
                    new GitignoreGenerator()
                };

                if (stack != null)
                {
                    generators.Add(new StackSpecificGenerator(stack));
                }

                // Generate files
                foreach (var generator in generators)
                {
                    generator.Generate(projectPath);
                }

                Console.WriteLine($"Successfully initialized project in: {projectPath}");
                Console.WriteLine("Created files:");
                Console.WriteLine("  - .clinerules");
                Console.WriteLine("  - readme.md");
                Console.WriteLine("  - .gitignore");
                if (stack != null)
                {
                    Console.WriteLine($"\nStack-specific files for {stack}:");
                    switch (stack)
                    {
                        case "cs":
                            Console.WriteLine("  - Program.cs");
                            Console.WriteLine("  - projectname.csproj");
                            break;
                        case "html":
                            Console.WriteLine("  - index.html");
                            Console.WriteLine("  - style.css");
                            Console.WriteLine("  - script.js");
                            break;
                        case "python":
                            Console.WriteLine("  - requirements.txt");
                            break;
                        case "node":
                            Console.WriteLine("  - package.json");
                            Console.WriteLine("  - index.js");
                            Console.WriteLine("  - node_modules/");
                            break;
                    }
                }
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
