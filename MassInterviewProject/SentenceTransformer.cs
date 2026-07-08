using System.Text.RegularExpressions;

namespace MassInterviewProject
{
    public class SentenceTransformer
    {
        WordFilter profanities = new();
        public static string? GetInput()
        {
            Console.Out.WriteLine("Pleaase enter your string: \n");
            return Console.ReadLine();
        }

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

        public static string FilterOutputForProfanity(string unformattedInput)
        {
            string filteredInput = unformattedInput;
            foreach (string profanity in WordFilter.BannedWords)
            {
                if (unformattedInput.Contains(profanity, StringComparison.CurrentCultureIgnoreCase))
                {
                    filteredInput = Regex.Replace(filteredInput, profanity, new String('*', profanity.Length), RegexOptions.IgnoreCase);
                }
            }

            string s = filteredInput ?? unformattedInput;

            return s;
        }

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