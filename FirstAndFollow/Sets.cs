using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstAndFollow
{
    public class Sets
    {
        string EPSILON = "ε";
        List<string> forFirstSet;
        List<string> forFollowSet;
        List<string> NonTerminals;
        List<string> Terminals;
        string following = string.Empty;
        string START_SYMBOL = string.Empty;
        readonly IDictionary<string, List<string>> Grammar;
        Dictionary<string, List<string>> firstSets = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> followSet = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> rand = new Dictionary<string, List<string>>();

        public Sets(Dictionary<string, List<string>> keyValues)
        {
            Grammar = keyValues;
            START_SYMBOL = Grammar.First().Key;
            NonTerminals = Grammar.Keys.ToList();
        }
        public Dictionary<string, List<string>> FirstSets()
        {

            foreach (string item in NonTerminals)
            {
                FollowFirst(item);
                firstSets.Add(item, forFirstSet);
            }

            Console.WriteLine("-----FIRST SETS-----");
            foreach (var item in firstSets)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(item.Key);
                sb.Append(" -> [ ");
                foreach (string value in item.Value)
                {
                    sb.Append(value + ",");
                }
                sb.Length--;
                sb.Append(" ]");
                Console.WriteLine(sb.ToString());
                sb.Clear();
            }

            return firstSets;
        }
        public Dictionary<string, List<string>> FollowSets()
        {
            int index = 0;
            foreach (string item in Grammar.Keys)
            {
                index++;
                following = item;
                Follow(item);
                if (START_SYMBOL == item)
                {
                    forFollowSet.Add("$");
                }
                followSet.Add(item, forFollowSet);
            }

            Console.WriteLine("-----FOLLOW SETS-----");
            StringBuilder sb;
            foreach (var item in followSet)
            {
                sb = new StringBuilder();
                sb.Append(item.Key + " -> [ ");                
                foreach (var values in item.Value.Distinct())
                {
                    sb.Append(values + " ,");
                }
                sb.Length--;
                sb.Append(" ]");
                Console.WriteLine(sb.ToString());
            }
            return followSet;
        }
        public IDictionary<string, List<string>> GetGrammar()
        {
            return Grammar;
        }
        public List<string> GetNonTerminals()
        {
            return NonTerminals;
        }
        public List<string> GetTerminals()
        {
            Terminals = new List<string>();
            foreach (var item in Grammar)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    if (isTerminal(item.Value[i]))
                    {
                        Terminals.Add(item.Value[i]);
                    }
                }
            }
            Terminals.RemoveAll(x => x == "|");
            return Terminals;
        }
        void FollowFirst(string s)
        {
            forFirstSet = new List<string>();
            try
            {

                List<string> rightSide = null;
                if (Grammar.TryGetValue(s, out rightSide))
                {
                    CheckAgain:
                    if (isTerminal(rightSide[0]))
                    {
                        forFirstSet.Add(rightSide[0]);
                        if (rightSide.Contains("|"))
                        {
                            List<string> newRight = new List<string>();
                            newRight.AddRange(rightSide);
                            newRight = AfterSlash(newRight);

                            if (isTerminal(newRight[0]))
                            {
                                forFirstSet.Add(newRight[0]);
                            }
                            else if (isNonTerminal(newRight[0]))
                            {
                                rightSide = newRight;
                                goto CheckAgain;
                            }
                            
                        }
                    }
                    else if(isNonTerminal(rightSide[0])) 
                    {
                        FollowFirst(rightSide[0]);
                    }   
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        void Follow(string s)
        {
            int i = 0;
            bool isFound = false;
            forFollowSet = new List<string>();
            try
            {
                List<string> rightSide = null;
                Dictionary<string,List<string>> resizedGrammar = GrammarValuesWhere(s);
                foreach (var item in resizedGrammar.ToList())
                {
                    foreach (var value in item.Value)
                    {
                        if (value == s && !isFound)
                        {
                            if (item.Value.IndexOf(value) == item.Value.Count - 1)
                            {
                                Follow(item.Key);
                                break;
                            }
                            else
                            {
                                isFound = true;
                                continue;
                            }
                        }
                        else if (isFound)
                        {
                            isFound = false;
                            if (value == "|")
                            {
                                continue;
                            }
                            else if (isEpsilon(value))
                            {
                                if (!forFollowSet.Exists(x => x == "$"))
                                {
                                    forFollowSet.Add("$");
                                }
                            }
                            else if (isTerminal(value))
                            {
                                forFollowSet.Add(value);
                                if (item.Value.Exists(x => x == EPSILON) && !forFollowSet.Exists(x => x == "$"))
                                {
                                    forFollowSet.Add("$");
                                }
                            }
                            else if (isNonTerminal(value))
                            {
                                Follow(item.Key);
                            }
                            break;
                        }
                        else if (value == "|")
                        {
                            continue;
                        }
                        else if (isEpsilon(value))
                        {
                            forFollowSet.Add("$");
                        }
                        else if (isTerminal(value) && item.Key == s)
                        {
                            forFollowSet.Add(value);
                        } 
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        void FindFollow(string s)
        {
            try
            {
                List<string> rightSide = null;
                if (Grammar.TryGetValue(s, out rightSide))
                {
                    if (isNonTerminal(rightSide[0]))
                    {
                        FindFollow(rightSide[0]);
                    }
                    else
                    {
                        List<string> newList = ResizeList(rightSide);
                        if (isTerminal(newList[0]))
                        {
                            forFollowSet.Add(newList[0]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            
        }
        bool isTerminal(string s)
        {
            if (!NonTerminals.Contains(s))
            {
                return true;
            }
            return false;
        }
        bool isNonTerminal(string s)
        {
            if (NonTerminals.Contains(s))
            {
                return true;
            }
            return false;
        }
        bool isEpsilon(string s)
        {
            if(EPSILON == s)
                return true;
            return false;
        }
        List<string> AfterSlash(List<string> rightSide)
        {
            int indexOfSlash = rightSide.FindIndex(x => x == "|");
            rightSide.RemoveRange(0, indexOfSlash + 1);
            return rightSide;
        }
        List<string> ResizeList(List<string> rightSide)
        {
            int index = rightSide.FindIndex(x => x == following);
            rightSide.RemoveRange(0, index + 1);
            return rightSide;
        }
        Dictionary<string, List<string>> GrammarValuesWhere(string s)
        {
            rand.Clear();
            foreach (var item in Grammar)
            {
                if (item.Value.Exists(x=>x.Contains(s)))
                {
                    rand.Add(item.Key, item.Value);
                }
            }
            return rand;
        }
    }
}
