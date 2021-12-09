using System;
using System.Collections.Generic;

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

            
            
            //right = new List<string>();
            //right.Add("T");
            //right.Add("E'");
            //dictionary.Add("E", right);

            //right = new List<string>();
            //right.Add("+");
            //right.Add("T");
            //right.Add("E'");
            //right.Add("|");
            //right.Add(EPSILON);
            //dictionary.Add("E'", right);

            //right = new List<string>();
            //right.Add("F");
            //right.Add("T''");
            //dictionary.Add("T", right);

            //right = new List<string>();
            //right.Add("*");
            //right.Add("F");
            //right.Add("T'");
            //right.Add("|");
            //right.Add(EPSILON);
            //dictionary.Add("T'", right);

            //right = new List<string>();
            //right.Add("(");
            //right.Add("E'");
            //right.Add(")");
            //right.Add("|");
            //right.Add("id");
            //dictionary.Add("F", right);




            right = new List<string>();
            right.Add("T");
            right.Add("R");
            dictionary.Add("E", right);

            right = new List<string>();
            right.Add("+");
            right.Add("T");
            right.Add("R");
            right.Add("|");
            right.Add(EPSILON);
            dictionary.Add("R", right);

            right = new List<string>();
            right.Add("F");
            right.Add("Y");
            dictionary.Add("T", right);

            right = new List<string>();
            right.Add("*");
            right.Add("F");
            right.Add("Y");
            right.Add("|");
            right.Add(EPSILON);
            dictionary.Add("Y", right);

            right = new List<string>();
            right.Add("(");
            right.Add("E");
            right.Add(")");
            right.Add("|");
            right.Add("i");
            dictionary.Add("F", right);

            Sets sets = new Sets(dictionary);
            sets.FirstSets();
            sets.FollowSets();
        }
    }
}
