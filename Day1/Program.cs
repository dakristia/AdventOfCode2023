using System;

using System;
using System.Collections.Generic;

namespace AdventOfCode2023.Day1
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    namespace AdventOfCode2023.Day1
    {
        class Solution
        {
            static void Main(string[] args)
            {
                sol3();
            }

            static void sol2()
            {
                // Read input
                List<string> lines = File.ReadAllLines(@"input.1.txt").ToList();
                // List<string> lines = File.ReadAllLines(@"testinput.txt").ToList();

                // Console.WriteLine(lines);
                // Only keep second element
                // lines = new List<string> { lines[1] }; //!REMOVE
                Console.WriteLine(lines.Count);
                // Mapping strings to int


                Dictionary<string, int> stringToInt = new Dictionary<string, int>();
                stringToInt.Add("zero", 0);
                stringToInt.Add("one", 1);
                stringToInt.Add("two", 2);
                stringToInt.Add("three", 3);
                stringToInt.Add("four", 4);
                stringToInt.Add("five", 5);
                stringToInt.Add("six", 6);
                stringToInt.Add("seven", 7);
                stringToInt.Add("eight", 8);
                stringToInt.Add("nine", 9);

                List<string> cleanedLines = new List<string>();

                foreach (string line in lines)
                {
                    // string lowercasedLine = line.ToLower();
                    // Console.WriteLine(lowercasedLine);
                    string digitLine = line.ToLower();

                    // for each key value paint in stringToInt
                    // replace key with value
                    string updatedLine = digitLine;

                    do
                    {
                        Dictionary<int, string> replacementDict = new Dictionary<int, string>();
                        foreach (KeyValuePair<string, int> entry in stringToInt)
                        {
                            digitLine = updatedLine;

                            string key = entry.Key;
                            string value = entry.Value.ToString();
                            Regex regexDigitWord = new Regex(key, RegexOptions.IgnoreCase);
                            Match match = regexDigitWord.Match(digitLine);
                            if (match.Success)
                            {
                                replacementDict.Add(match.Index, match.Value);
                            }



                        }

                        Console.WriteLine("   ");


                        if (replacementDict.Any())
                        {
                            if (replacementDict.Count > 1)
                            {
                                Console.WriteLine("More than one match found");
                                Console.WriteLine("digitLine: " + digitLine);
                                // Print each key value pair
                                foreach (KeyValuePair<int, string> entry in replacementDict)
                                {
                                    Console.WriteLine(entry.Key + " " + entry.Value);
                                }
                            }
                            int indexN = replacementDict.Keys.Min();
                            string valueOfN = replacementDict[indexN];
                            Console.WriteLine("replacing: " + valueOfN + " on index: " + indexN + " in " + digitLine);
                            // Console.WriteLine("indexN: " + indexN);
                            // Console.WriteLine("valueOfN: " + valueOfN);
                            int lengthOfWord = valueOfN.Length;

                            string before = digitLine.Substring(0, indexN);
                            string middle = digitLine.Substring(indexN, lengthOfWord);
                            string after = digitLine.Substring(indexN + lengthOfWord);

                            middle = middle.Replace(valueOfN, stringToInt[valueOfN].ToString());

                            // digitLine = before + middle + after;

                            updatedLine = before + middle + after;
                            Console.WriteLine("digitLine: " + digitLine);
                            Console.WriteLine("updatedLine: " + updatedLine);
                        }
                        else
                        {
                            Console.WriteLine("No match found: ", digitLine, updatedLine);
                        }
                        Console.WriteLine("   " + digitLine + " " + updatedLine);

                    } while (updatedLine != digitLine);


                    // digitLine = digitLine.Replace(" ", "");

                    // Remove all non-numeric characters
                    Regex regex = new Regex("[^0-9]");

                    string cleanedLine = regex.Replace(digitLine, "");
                    cleanedLines.Add(cleanedLine);
                    // Console.WriteLine(digitLine);

                }

                lines = cleanedLines;

                List<int> numbers = new List<int>();
                foreach (string line in lines)
                {
                    string n1_s = line.First().ToString();
                    string n2_s = line.Last().ToString();
                    string n_s = n1_s + n2_s;
                    int number = int.Parse(n_s);
                    numbers.Add(number);
                }

                // Summing up
                int sum = 0;
                foreach (int number in numbers)
                {
                    sum += number;
                }

                List<string> ogLines = File.ReadAllLines(@"input.1.txt").ToList();

                for (int i = 0; i < ogLines.Count; i++)
                {
                    Console.WriteLine(ogLines[i] + " " + numbers[i]);
                }

                Console.WriteLine(sum);

                Console.ReadKey();
            }

            static void sol1()
            {
                // Read input
                List<string> lines = File.ReadAllLines(@"input.1.txt").ToList();
                Console.WriteLine(lines);



                // Remove all non-numeric characters
                List<string> cleanedLines = new List<string>();
                Regex regex = new Regex("[^0-9]");
                foreach (string line in lines)
                {
                    string cleanedLine = regex.Replace(line, "");
                    cleanedLines.Add(cleanedLine);
                }

                lines = cleanedLines;

                List<int> numbers = new List<int>();
                foreach (string line in lines)
                {
                    string n1_s = line.First().ToString();
                    string n2_s = line.Last().ToString();
                    string n_s = n1_s + n2_s;
                    int number = int.Parse(n_s);
                    numbers.Add(number);
                }

                foreach (int line in numbers)
                {
                    Console.WriteLine(line);
                }
                //Summing up
                int sum = 0;
                foreach (int number in numbers)
                {
                    sum += number;
                }


                Console.WriteLine(sum);

                // Console.WriteLine(new int[] { 0, 1, 2, 3 }.ToList());
                Console.ReadKey();
            }

            static void sol3()
            {
                // Read input
                List<string> lines = File.ReadAllLines(@"input.1.txt").ToList();
                // List<string> lines = File.ReadAllLines(@"testinput.txt").ToList();
                Console.WriteLine(lines);

                List<int> numbers = new List<int>();

                Dictionary<string, int> stringToInt = new Dictionary<string, int>();
                stringToInt.Add("zero", 0);
                stringToInt.Add("one", 1);
                stringToInt.Add("two", 2);
                stringToInt.Add("three", 3);
                stringToInt.Add("four", 4);
                stringToInt.Add("five", 5);
                stringToInt.Add("six", 6);
                stringToInt.Add("seven", 7);
                stringToInt.Add("eight", 8);
                stringToInt.Add("nine", 9);

                Dictionary<int, string> replacementDict = new Dictionary<int, string>();
                foreach (string line in lines)
                {
                    Console.WriteLine();
                    Console.WriteLine(line);
                    int frontNum = -99999999;
                    int backNum = -99999999;
                    // From front, find first number
                    for (int i = 0; i < line.Length; i++)
                    {
                        string maybeNum = line[i].ToString();
                        // if character is number, save and break
                        if (int.TryParse(maybeNum, out int n)) { frontNum = int.Parse(maybeNum); break; }

                        string frontWord = line.Substring(0, i + 1);
                        // Get key matching digitWord
                        foreach (string key in stringToInt.Keys)
                        {
                            // Console.WriteLine("key: " + key + "frontWord: " + frontWord + " frontWord.Contains(key): " + frontWord.Contains(key));
                            if (frontWord.Contains(key))
                            {
                                // if word is number, save and break
                                frontNum = stringToInt[key];
                                break;
                            }
                        }
                        if (frontNum > 0) { break; }
                    }

                    // From back, find last number
                    for (int i = line.Length - 1; i >= 0; i--)
                    {
                        string maybeNum = line[i].ToString();
                        // if character is number, save and break
                        if (int.TryParse(maybeNum, out int n)) { backNum = int.Parse(maybeNum); break; }
                        // Console.WriteLine(line);
                        // Console.WriteLine((i - 1).ToString() + (line.Length - 1));
                        string backWord = line.Substring(i, line.Length - i);
                        // Get key matching digitWord
                        foreach (string key in stringToInt.Keys)
                        {
                            // Console.WriteLine("key: " + key + " backWord: " + backWord + " backWord.Contains(key): " + backWord.Contains(key));
                            if (backWord.Contains(key))
                            {
                                // if word is number, save and break
                                backNum = stringToInt[key];
                                break;
                            }
                        }
                        if (backNum > 0) { break; }
                    }

                    string number = frontNum.ToString() + backNum.ToString();
                    numbers.Add(int.Parse(number));

                }

                foreach (int line in numbers)
                {
                    Console.WriteLine(line);
                }
                //Summing up
                int sum = 0;
                foreach (int number in numbers)
                {
                    sum += number;
                }

                Console.WriteLine(sum);
            }
        }
    }
}
