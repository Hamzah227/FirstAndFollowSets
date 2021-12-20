using System;
using System.Collections.Generic;
using Parser;

namespace FirstAndFollow
{
    class Program
    {
        static void Main(string[] args)
        {
            string EPSILON = "ε";
            //RHS as a Key.
            //LHS as a List of string
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            List<string> right;

            #region Grammar 0
            right = new List<string>();
            right.Add("T");
            right.Add("E'");
            dictionary.Add("E", right);

            right = new List<string>();
            right.Add("+");
            right.Add("T");
            right.Add("E'");
            right.Add("|");
            right.Add(EPSILON);
            dictionary.Add("E'", right);

            right = new List<string>();
            right.Add("F");
            right.Add("T''");
            dictionary.Add("T", right);

            right = new List<string>();
            right.Add("*");
            right.Add("F");
            right.Add("T'");
            right.Add("|");
            right.Add(EPSILON);
            dictionary.Add("T'", right);

            right = new List<string>();
            right.Add("(");
            right.Add("E'");
            right.Add(")");
            right.Add("|");
            right.Add("id");
            dictionary.Add("F", right);
            #endregion

            #region Grammar 01
            //right = new List<string>();
            //right.Add("a");
            //right.Add("A'");
            //right.Add("|");
            //right.Add("b");
            //right.Add("B");
            //right.Add("|");
            //right.Add("a");
            //dictionary.Add("S", right);


            //right = new List<string>();
            //right.Add("a");
            //right.Add("B'");
            //right.Add("|");
            //right.Add("B");
            //right.Add("|");
            //right.Add(EPSILON);
            //dictionary.Add("A", right);

            //right = new List<string>();
            //right.Add("b");
            //right.Add("|");
            //right.Add(EPSILON);
            //dictionary.Add("B", right);
            #endregion

            Sets sets = new Sets(dictionary);
            Dictionary<string, List<string>> firstSets = sets.FirstSets();
            Dictionary<string, List<string>> followSets = sets.FollowSets();
            IDictionary<string, List<string>> Grammar = sets.GetGrammar();

            LL1 Parser = new LL1(sets.GetTerminals(), sets.GetNonTerminals());
            Parser.ParseTable(firstSets, followSets, Grammar);
        }
    }
}
