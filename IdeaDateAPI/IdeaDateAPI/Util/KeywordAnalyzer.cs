using System;
using System.Collections.Generic;

namespace IdeaDateAPI.Util
{
    public class KeywordAnalyzer
    {
        public KeywordAnalyzer()
        {
        }

        public static int ScoreKeywords(List<string> projectKeywords,
            List<string> userTechStack)
        {
            int result = 0;
            HashSet<string> seen =
                new HashSet<string>();
            foreach (string s in projectKeywords)
            {
                seen.Add(s);
            }

            foreach (string s in userTechStack)
            {
                if (seen.Contains(s.ToLower()))
                {
                    result++;
                }
            }

            return result;
        }
    }
}
