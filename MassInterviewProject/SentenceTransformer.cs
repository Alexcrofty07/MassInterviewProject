using System.Text.RegularExpressions;

namespace MassInterviewProject
{
    public class SentenceTransformer
    {
        WordFilter profanities = new();

        /// <summary>
        /// Ask user for string, creates a nullable string variable with response.
        /// </summary>
        /// <returns></returns>
        public static string? GetInput()
        {
            Console.Out.WriteLine("Please enter your string, followed by any possible transformation: \n");
            return Console.ReadLine();
        }

        /// <summary>
        /// Search the inputted string for the console commands by searching for the " -" 
        /// string which indicates the start of the command. 
        /// Then add each subsequent letter to list of strings to be processed later.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<string> GetCommandsFromInput(string input)
        {
            List<string> commands = [];

            if (input.Contains(" -"))
            {
                foreach (string word in input.Split(" "))
                {
                    if (word.StartsWith('-') && word.Length > 1)
                    {
                        commands.Add(word[1..].ToUpper());
                    }
                }
            }

            return commands;
        }

        /// <summary>
        /// Takes the input and the created list of commands to format. By default creates sentence case, otherwise 
        /// works with the commands
        /// </summary>
        /// <param name="input"></param>
        /// <param name="commands"></param>
        /// <returns></returns>
        public static string FormatOutput(string input, List<string> commands)
        {
            if (commands != null && commands.Count > 0)
            {
                input = input.Substring(0, input.IndexOf(" -")).Trim();
            }

            string sentenceCase = input[0].ToString().ToUpper() + input[1..].ToLower();

            if (commands == null || commands.Count < 1)
            {
                return sentenceCase;
            }
            else
            {
                string transformedOutput = sentenceCase;
                foreach (string command in commands)
                {
                    switch (command)
                    {
                        case "U":
                            transformedOutput = transformedOutput.ToUpper();
                            break;
                        case "L":
                            transformedOutput = transformedOutput.ToLower();
                            break;
                        case "P":
                            transformedOutput = FilterOutputForProfanity(transformedOutput);
                            break;
                    }
                }

                return transformedOutput;
            }
        }

        /// <summary>
        /// Search for each profanity in word list and check if input contains 
        /// If it does, replace with correct number of * depending on profanity length 
        /// </summary>
        /// <param name="unfilteredInput"></param>
        /// <returns></returns>
        public static string FilterOutputForProfanity(string unfilteredInput)
        {
            string filteredInput = unfilteredInput;
            foreach (string profanity in WordFilter.BannedWords)
            {
                if (unfilteredInput.Contains(profanity, StringComparison.CurrentCultureIgnoreCase))
                {
                    filteredInput = Regex.Replace(filteredInput, profanity, new String('*', profanity.Length), RegexOptions.IgnoreCase);
                }
            }

            string s = filteredInput ?? unfilteredInput;

            return s;
        }

        /// <summary>
        /// Main function, call the functionality methods and return either transformed output or error message to user. 
        /// </summary>
        public static void Main()
        {
            string? userInput = GetInput();

            if (!string.IsNullOrEmpty(userInput))
            {
                List<string> commands = GetCommandsFromInput(userInput);

                string transformedOutput = FormatOutput(userInput, commands);

                Console.WriteLine(transformedOutput ?? "An error occurred in the application, please try again");
            }
        }


    }
}