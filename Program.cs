// 08/11/2021
// L. Julia Rodrigo
// 19718171

// operating system:    windows
// IDE:                 Visual Studio Code

// this code will accept only html files and convert them to other files.

//DONT FORGET TO GET THE FILE DIRECTORY OF YOUR FILES. please write them in the main method below

// the only beginning files are Program.cs and table.html

// i have made a latex and two text table conversion formats

// https://tableconvert.com/?output=text
// reference for latex and text table format

// the code will give you instructions as you go along


using System;
using System.IO;
using System.Text.RegularExpressions;

// all helpful links that helped me

// https://www.techiedelight.com/identify-string-is-numeric-csharp/
// https://stackoverflow.com/questions/787932/using-c-sharp-regular-expressions-to-remove-html-tags
// https://stackoverflow.com/que/stions/10824165/converting-a-csv-file-to-json-using-c-sharp
// https://www.codegrepper.com/code-examples/csharp/c%23+create+file+and+write
// dotnet new console
// Install-Package Aspose.Cells

// dotnet add package HtmlAgilityPack --version 1.11.37
using HtmlAgilityPack;
// https://sites.harding.edu/fmccown/java_csharp_comparison.html#arrays

using System.Linq;
using System.Collections.Generic;

namespace assignment_1_converting
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("\nBe sure to hit 'y' or 'Y' to continue\n");
            yes(Console.ReadLine());
            
            Console.WriteLine("\n\nI will change any .html file to .csv, .json or .md\n");
            Console.WriteLine("Dont forget to rewrite file directory\n");

            yes(Console.ReadLine());

//----------------------------------------please enter file directory =======================================

            // dont forget to reference the 'table.html' file at the end!
            String fileDirectory = @"D:\3rd Year\cs264 c sharp\assignment 1 converting\table.html"; 

// -------------------------------------------------------------------=======================================


            String a = File.ReadAllText(fileDirectory);

            help();
            bool finish = false;
            String input = "";
            while(finish == false) //options to choose from while the all is running.it will end when finished() is called
            {
                input = Console.ReadLine().ToLower();
                switch(input)
                {
                    case "v": verbose(); break;
                    case "-v": verbose(); break;
                    case "version": verbose(); break;
                    case "-version": verbose(); break;

                    case "o": output(a, fileDirectory); help(); break;
                    case "-o": output(a, fileDirectory); help(); break;
                    case "output": output(a, fileDirectory); help(); break;
                    case "-output": output(a, fileDirectory); help(); break;
                    
                    case "l": list(); break;
                    case "-l": list(); break;
                    case "list-formats": list(); break;
                    case "-list-formats": list(); break;

                    case "h": help(); break;
                    case "-h": help(); break;
                    case "help": help(); break;
                    case "-help": help(); break;

                    case "i": info(); break;
                    case "-i": info(); break;
                    case "info": info(); break;
                    case "-info": info(); break;

                    case "f": finish = finished(); break;
                    case "-f": finish = finished(); break;
                    case "finish": finish = finished(); break;
                    case "-finish": finish = finished(); break;

                    default: Console.WriteLine("\nhit 'h' for help :D"); break;
                }
            }
        }


        static void test()
        { 
            Console.WriteLine("MAKE SURE YOU WRITE VERBOSE METHOD\n");
        }

        static string add(int n, string s) //add a string, n times
        { 
            if(n == 0) return "";
            return s + add(n - 1, s);
        }

        static string check(string s) // check if string in a number, else it returns a "string", used for json and csv
        {
            if (Regex.IsMatch(s, @"^\d+$")) return s;
            else return "\"" + s + "\"";
        }

        static string heading(int [] greatest) // the lines for the heading of the md file
        { 
            String great = "|";
            for(int i = 0; i < greatest.GetLength(0); i++)
                if(i == (greatest.GetLength(0) - 1))
                    great = great + add(greatest[i], "-") + "|";
                else 
                    great = great + add(greatest[i], "-") + "|";
            return great;
        }

        static string headingForText(int [] greatest, String lineLol) //text lines separators
        { 
            String great = "+" + lineLol;
            for(int i = 0; i < greatest.GetLength(0); i++)
                if(i == (greatest.GetLength(0) - 1))
                    great = great + add(greatest[i], lineLol) + lineLol + "+";
                else 
                    great = great + add(greatest[i], lineLol) + lineLol + "+" + lineLol;
            return great;
        }

        static string csvFile(String [,] change) // conert to csv file
        {
            Console.WriteLine("Gonna make csv file\n");
            
            String csvFile = "";
            Regex rg = new Regex(@"[0123456789]*"); //regex for digits
            for(int i = 0; i < change.GetLength(0); i++)
            {
                for(int a = 0; a < change.GetLength(1); a++)
                {
                    if(Regex.IsMatch(change[i, a], @"^\d+$")) // if number
                        if(a == (change.GetLength(1) - 1)) // and by the end of a row
                            csvFile = csvFile + change[i, a] + "\n"; // dont put comma
                        else
                            csvFile = csvFile + change[i, a] + ",";
                    else if(a == (change.GetLength(1) - 1)) // just before the end of rows
                        csvFile = csvFile + "\"" + change[i, a] + "\"\n";
                    else
                        csvFile = csvFile + "\"" + change[i, a] + "\","; // add commas
                }
            }
            return csvFile;
        }

        static string mdFile(String [,] change)
        {
            Console.WriteLine("Gonna make md file\n");
            int [] greatest = new int [change.GetLength(1)]; // for finding the word with the greatest amount of letters
            int greatIndex = 0;

            //// go through the array and bubble sort of for the biggest amount of char in one word
            //
            for(int j = 0; j < change.GetLength(1); j++) 
            {
                for(int i = 0; i < change.GetLength(0); i++)
                    if(greatest[greatIndex] < change[i,j].Length)
                        greatest[greatIndex] = change[i,j].Length;
                greatIndex++;
            }

            greatIndex = 0; // index of the array

            String mdFile = "|";
            for(int j = 0; j < change.GetLength(1); j++)
            {
                for(int i = 0; i < change.GetLength(0); i++)
                    if(greatest[greatIndex] > change[i,j].Length)
                        change[i,j] = change[i,j] + add(greatest[greatIndex] - change[i,j].Length, " "); 
                        // put spacesto filll remaining cell
                greatIndex++;
            }

            for(int i = 0; i < change.GetLength(0); i++)
            {
                if (i != 0) mdFile = mdFile + "\n|";
                for(int j = 0; j < change.GetLength(1); j++)
                    mdFile = mdFile + change[i,j] + "|";
                if(i == 0) mdFile = mdFile + "\n" + heading(greatest); // add the row of lines
            }
            return mdFile;
        }

        static string jsonFile(String [,] change)
        {
            Console.WriteLine("Gonna make json file\n");
            string jsonFile = "[\n";
            
            for(int i = 1; i < change.GetLength(0); i++)
            {
                jsonFile = jsonFile + "\t{\n\t\t";
                for(int j = 0; j < change.GetLength(1); j++)
                {
                    if (i == (change.GetLength(0) - 1) & j == (change.GetLength(1) - 1)) // dont put comma if at the last row
                        jsonFile = jsonFile + check(change[0, j]) + " : " + check(change[i, j]) + "\n\t}\n";

                    else if(j == (change.GetLength(1) - 1)) // but comma at the end of full each row
                        jsonFile = jsonFile + check(change[0, j]) + " : " + check(change[i, j]) + "\n\t},\n";
                    
                    else // comma at every individual units
                        jsonFile = jsonFile + check(change[0, j]) + " : " + check(change[i, j]) + ",\n\t\t";
                }
            }
            return jsonFile + "\n]";
        }

        static string textFile(String [,] change, bool version2)
        {
            Console.WriteLine("Gonna make text file\n");
            int [] greatest = new int [change.GetLength(1)]; // same like md
            int greatIndex = 0;

            for(int j = 0; j < change.GetLength(1); j++)
            {
                for(int i = 0; i < change.GetLength(0); i++)
                    if(greatest[greatIndex] < change[i,j].Length)
                        greatest[greatIndex] = change[i,j].Length;
                greatIndex++;
            }
            greatIndex = 0;

            for(int j = 0; j < change.GetLength(1); j++)
            {
                for(int i = 0; i < change.GetLength(0); i++)
                    if(greatest[greatIndex] > change[i,j].Length)
                        change[i,j] = change[i,j] + add(greatest[greatIndex] - change[i,j].Length, " ");
                        // add spaces to fill the cell
                greatIndex++;
            }

            String txtFile = "";

            if(version2)
            {
                txtFile = headingForText(greatest, "─") +"\n";
                txtFile = txtFile + "| ";

                for(int i = 0; i < change.GetLength(0); i++)
                {
                    if (i != 0) txtFile = txtFile + "\n| ";
                    for(int j = 0; j < change.GetLength(1); j++)
                        txtFile = txtFile + change[i,j] + " | ";
                    if (i == 0) txtFile = txtFile + "\n" + headingForText(greatest, "─");
                }
                if(change.GetLength(0) != 1)
                txtFile = txtFile + "\n" + headingForText(greatest, "─");
            }

            else
            {
                txtFile = headingForText(greatest, "-") +"\n";
                txtFile = txtFile + "| ";

                for(int i = 0; i < change.GetLength(0); i++)
                {
                    if (i != 0) txtFile = txtFile + "\n| ";
                    for(int j = 0; j < change.GetLength(1); j++)
                        txtFile = txtFile + change[i,j] + " | ";
                    txtFile = txtFile + "\n" + headingForText(greatest, "-");
                }
            }
            return txtFile;
        }


        static string latexFile(String [,] change)
        {
            Console.WriteLine("Gonna make latex file\n");
            string latexFile = "\\begin{table}\n\t\\centering\n\t\\begin{tabular}{|";
            latexFile = latexFile + columnsLatex(change.GetLength(1)) + "}\n\t\\hline\n";

            for(int i = 0; i < change.GetLength(0); i++)
            {
                latexFile = latexFile + "\t\t";
                for(int j = 0; j < change.GetLength(1); j++)
                    if(j == (change.GetLength(1) - 1)) 
                        latexFile = latexFile + change[i, j];
                    else latexFile = latexFile + change[i, j] + " & ";
                latexFile = latexFile + " \\\\ \\hline\n";
            }
            return latexFile + "\t\\end{tabular}\n\\end{table}";
        }
        
        static String columnsLatex(int col) // show the number of columns properly
        {
            if(col == 0) return "";
            return "l|" + columnsLatex(col-1);
        }

        static String options(String extent)
        {
            Regex check = new Regex(@"[abcdezABCDEZ]"); // user input has to match with any of these letters
            while(!check.IsMatch(extent))
            {
                Console.WriteLine("\nPlease enter again! xD\n\n(a): .csv\n(b): .md\n(c): .json\n(d): .tex\n(e): .txt\n(z): .txt (version 2)\n");
                extent = Console.ReadLine();
            }
            return extent;
        }

        static void verbose()
        {
            Console.WriteLine("\n\nThis program will convert a .html table file to\nanother format of the following:");
            Console.WriteLine("[ .md | .json | .csv | .tex | .txt ]\n\nA new file may be created or overwritten\n");

            Console.WriteLine("Would you like a long explaination of the code?\n");

            if(YesOrNo(Console.ReadLine()))
            {
                Console.WriteLine("\nJust keep hitting 'y' OR 'ctrl c' to end the program in vsCode");
                yes(Console.ReadLine());

                Console.WriteLine("\nThe option 'o' or 'output' will prompt you to choose");
                Console.WriteLine("what file you want to convert your .html table to");
                yes(Console.ReadLine());
                Console.WriteLine("The html file contents will be read from the file directory\nand stored in a string");
                Console.WriteLine("Regex expression will delete all html related tags and then remove all white spaces");
                yes(Console.ReadLine());
                Console.WriteLine("All thats left in the string will be commas, information and quotation marks");
                Console.WriteLine("The file will be cleaned up further (i.e. removing some commas)");
                yes(Console.ReadLine());

                Console.WriteLine("Then the file information is placed in a 2D array which will make conversion easy");
                Console.WriteLine("the file have their own conversion functions");
                yes(Console.ReadLine());
                Console.WriteLine("A new file may be created or an existing file may be overwrote\nprovided you are happy with the format");
                yes(Console.ReadLine());
                Console.WriteLine("\nEnd of walkthrough\n\n");
            }

            help();

        }

        static void output(String a, String fileDirectory)
        {
            Console.WriteLine("\n\nYour file is a HTML file. \nWhat would you like to change it to? \nEnter 'a', 'b', 'c', 'd', 'e' or 'z'\n\n");
            Console.WriteLine("(a): csv\n(b): md\n(c): json\n(d): latex\n(e): txt\n(z): txt (version2)");

            String extent = Console.ReadLine().ToLower();
            extent = options(extent); // chosen by user input
            String confirm = ""; // user input

            while(!confirm.Equals("y"))
            {
                Console.WriteLine("Will convert to: " + extent + "\n\n(y): YES\n(n): NO");
                confirm = Console.ReadLine().ToLower();
                if(confirm.Equals("n")) extent = options("");
            }
        

            String [,] change = htmlFile(a); // the 2D array to be filled
            String converted = ""; // string converted
            String fileExtent = ""; // the file .extention

            switch(extent.ToLower())
            {
                case "a": fileExtent = "csv"; converted = csvFile(change); break;
                case "b": fileExtent = "md"; converted = mdFile(change); break;
                case "c": fileExtent = "json"; converted = jsonFile(change); break;
                case "d": fileExtent = "tex"; converted = latexFile(change); break;
                case "e": fileExtent = "txt"; converted = textFile(change, false); break;
                case "z": fileExtent = "txt"; converted = textFile(change, true); break;

                default: Console.WriteLine("\nNOT WORKING"); break;
            }

            // ENDING
            Console.WriteLine(converted);

            Console.WriteLine("\nIs this the file you want? \n'y' or 'n'");
            if(YesOrNo(Console.ReadLine())) MakeAFile(converted, fileExtent, fileDirectory);

        }

        static void MakeAFile(String converted, String fileExtent, String fileDirectory)
        {
            Console.WriteLine("\n\nYour .html file will be formatted to :....\n." + fileExtent + " file!");
            Console.WriteLine("\nContent in file about to be made: \n" + converted);
            Console.WriteLine("\nYour fileDirectory: " + fileDirectory);
            
            bool collect = false;
            String newFileName = "";

            for(int i = (fileDirectory.Length - 1); i >= 0; i--)
                if(collect) newFileName = fileDirectory[i] + newFileName;
                else if(fileDirectory[i] == '.') collect = true;

            if(fileExtent.Equals("tex")) // rename and add extention to file
                newFileName = newFileName + "-that-was-html-to-la" + fileExtent + "." + fileExtent;
            else if(fileExtent.Equals("txt"))
                newFileName = newFileName + "-that-was-html-to-text" + "." + fileExtent;
            else
                newFileName = newFileName + "-that-was-html-to-" + fileExtent + "." + fileExtent;
            Console.WriteLine("\nnewFileName: " + newFileName);

            existancefileCheck(newFileName, converted);
        }

        static void existancefileCheck(String newFileName, String fileContent)
        {
            //Check if the file exists
            if (!File.Exists(newFileName)) 
            {
                // Create the file and use streamWriter to write text to it.
                //If the file existence is not check, this will overwrite said file.
                //Use the using block so the file can close and vairable disposed correctly
                using (StreamWriter writer = File.CreateText(newFileName)) 
                {
                    writer.WriteLine(fileContent);
                }
                Console.WriteLine("\nFile created!");

            }
            else
            {
                Console.WriteLine("\nThis file seems to have existed...rewrite it?");
                if(YesOrNo(Console.ReadLine()))
                {
                    using (StreamWriter writer = File.CreateText(newFileName)) 
                    {
                        writer.WriteLine(fileContent);
                    }
                    Console.WriteLine("\nFile Overwrote!");
                }
                else Console.WriteLine("\nFile not changed.");

            }
        }


        static void list()
        {
            Console.WriteLine("\n\nYour .html table file will be formatted to any of the followings:....\n[ .csv | .md | .json | .tex | .txt ]");
        }

        static bool finished()
        {
            Console.WriteLine("\n\nBye bye? :<");
            if(YesOrNo(Console.ReadLine().ToLower()))
            {
                Console.WriteLine("aw bye next time xD\nQED.");
                return true;
            }
            else 
            {
                Console.WriteLine("Stay! :>");
                help();
                return false;
            }
        }

        

        static void info()
        {
            Console.WriteLine("\n\nThe current version is... 707-1T-d035nT-3X15T5!\n");
        }

        static void yes(String input)
        {
            while(!input.Equals("y") && !input.Equals("Y") )
                input = Console.ReadLine();
        }
        static bool YesOrNo(String input)
        {
            while(!input.Equals("n") && !input.Equals("y") )
                input = Console.ReadLine().ToLower();
            if(input.Equals("n")) return false;
            else return true;
        }

        

        static void help()
        {
            Console.WriteLine("\n\nEnter 'v', 'o', 'l', 'h', 'i' or 'f' "); // walk through; press key tp enter

            Console.WriteLine("\n-v, —verbose                 Verbose mode (debugging output to STDOUT)"); // walk through; press key tp enter
            Console.WriteLine("-o, —output                  Output file specified by <file>"); // do the code
            Console.WriteLine("-l, —list-formats            List formats"); //  Console.WriteLine("<.ext> will be one of [.html | .md | .csv | .json]");
            Console.WriteLine("-h, —help                    Show usage message"); // show instiuctiosn again
            Console.WriteLine("-i, —info                    Show version information"); // the current ver is 1
            Console.WriteLine("-f, —finish                  End the visit?");
        }

        public static String [,] htmlFile(String a) // converting html file to 2D array, parsing
        {
            String b = "";
            bool keep = false;
            String noHtml = Regex.Replace(a, @"<[^>]*>", "\""); // take out all html related tags
            String willb = Regex.Replace(noHtml, @" |\t|\n|\r", string.Empty); // clear the string of white spaces
            int ItemPerRows = 0, allItemCount = 0, maxCount = 0; // used to determin array dimensions
            bool counting = false;
            
            for(int i = 1; i < (willb.Length - 1); i++) // determin dimentions
            {
                if(willb[i] != '\"' & willb[i - 1] == '\"') 
                {
                    b += "\"" + willb[i];
                    keep = true;
                }

                else if (keep & willb[i] == '\"')
                {
                    b += willb[i] + ",";
                    keep = false;
                    allItemCount++;
                }
                
                else if (keep) b += willb[i];

                if(willb[i - 1] == '\"' & willb[i] == '\"' & willb[i + 1] != '\"')
                {
                    ItemPerRows++;
                    counting = true;
                }
                if(counting & willb[i - 1] == '\"' & willb[i] == '\"' & willb[i + 1] == '\"')
                {
                    if(maxCount < ItemPerRows) maxCount = ItemPerRows;
                    ItemPerRows = 0;
                    counting = false;
                }
            }

            string [,] change = new string[allItemCount/maxCount, maxCount];
            int start = 1, row = 0, col = 0;
            bool hey = false;
            
            for(int i = 0; i < b.Length; i++) // fill the array
            {
                if(hey && b[i] == '\"')
                {
                    change[row, col] = b.Substring(start, i - start);
                    hey = false;
                    start = i + 3;
                    col++;
                    if(col == (maxCount)) 
                    {
                        row++; col = 0;
                    }
                }
                else if(b[i] == '\"') hey = true;
            }

            Console.WriteLine("\n");
            return change;
        }
    }
}