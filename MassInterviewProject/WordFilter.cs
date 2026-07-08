using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassInterviewProject
{
    public class WordFilter
    {
        public static List<string> BannedWords
        {
            get
            {
                return ["BAD", "WRONG"];
            }
        }
    }
}
