using MassInterviewProject;

namespace Tests
{
    [TestClass()]
    public class SentenceTransformerTests
    {
        /// <summary>
        /// Take the input and separate the commands and format the output 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Formatted string</returns>
        public string Act(string input)
        {
            List<string> commands = SentenceTransformer.GetCommandsFromInput(input);
            string actual = SentenceTransformer.FormatOutput(input, commands);

            return actual;
        }

        /// <summary>
        /// Test conversion to upper case works with combinations of inputs using -u command.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="expectedOutput"></param>
        [DataRow("Test sentence case gets converted. To upper -u", "TEST SENTENCE CASE GETS CONVERTED. TO UPPER")]
        [DataRow("test lower case gets converted to upper -u", "TEST LOWER CASE GETS CONVERTED TO UPPER")]
        [DataRow("TEsT Casing goes to upper WHEN both commands are used -l -u", "TEST CASING GOES TO UPPER WHEN BOTH COMMANDS ARE USED")]
        [DataRow("Test upper casing with multiple non programmed commands  -a -b -c -u", "TEST UPPER CASING WITH MULTIPLE NON PROGRAMMED COMMANDS")]
        [DataRow("Test upper casing works with hypenated-words -u", "TEST UPPER CASING WORKS WITH HYPENATED-WORDS")]
        [TestMethod]
        public void TestConversionToUpperCase(string input, string expectedOutput)
        {
            string actual = Act(input);

            //Assert 
            Assert.IsTrue(isCorrectCasing(actual, true));
            Assert.AreEqual(actual, expectedOutput);
        }

        /// <summary>
        /// Test conversion to lower case works in combinations with -l command.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="expectedOutput"></param>
        [DataRow("TEST UPPER CASE GETS CONVERTED TO LOWER -l", "test upper case gets converted to lower")]
        [DataRow("Test sentence case gets converted. To lower -l", "test sentence case gets converted. to lower")]
        [DataRow("TEsT Casing goes to lower WHEN both commands are used -u -l", "test casing goes to lower when both commands are used")]
        [DataRow("Test lower casing with multiple non programmed commands -a -b -c -l", "test lower casing with multiple non programmed commands")]
        [DataRow("Test lower casing works with hypenated-words -l", "test lower casing works with hypenated-words")]
        [TestMethod] 
        public void TestConversionToLowerCase(string input, string expectedOutput)
        {
            string actual = Act(input);

            // Assert
            Assert.IsTrue(isCorrectCasing(actual, false));
            Assert.AreEqual(expectedOutput, actual);
        }

        /// <summary>
        /// Test the profanity filter works with in different circumstances using -p command
        /// </summary>
        /// <param name="input"></param>
        /// <param name="expectedOutput"></param>
        [TestMethod]
        [DataRow("Test bad words get filtered and rest converted to lower -l -p", "test *** words get filtered and rest converted to lower")]
        [DataRow("test bad words get filtered and rest converted to upper -u -p", "TEST *** WORDS GET FILTERED AND REST CONVERTED TO UPPER")]
        [DataRow("test sentence CASING when using wrong bad words -p", "Test sentence casing when using ***** *** words")]
        [DataRow("test purging with WRONG BAD words in caps -p", "Test purging with ***** *** words in caps")] // extra case to ensure commands work in caps
        [DataRow("test purging with wrong bad words with capital command -P", "Test purging with ***** *** words with capital command")]
        [DataRow("test upper casing when bad words purged first -p -u", "TEST UPPER CASING WHEN *** WORDS PURGED FIRST")]
        [DataRow("Test LOWER casing when bad words purged first -p -l", "test lower casing when *** words purged first")]
        public void TestPurgingWithBadWords(string input, string expectedOutput)
        { 
            string actual = Act(input);

            Assert.AreEqual(expectedOutput, actual);
        }

        /// <summary>
        /// Method to check if all chars in given string are the correct case.
        /// </summary>
        /// <param name="input">String to check</param>
        /// <param name="checkingUpper">bool to check upper or lower casing</param>
        /// <returns>Value equal to if all characters are correct case in input string</returns>
        private bool isCorrectCasing(string input, bool checkingUpper)
        {
            if (!string.IsNullOrEmpty(input))
            {
                bool caseMatches = true;
                foreach(char c in input)
                {
                    if (char.IsLetter(c))
                    {
                        if (!Char.IsUpper(c).Equals(checkingUpper))
                        {
                            caseMatches = false;
                        }
                    }
                }

                return caseMatches;
            }

            // if string is null or empty return false 
            return false;
        }
    }
}