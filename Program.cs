using System.Linq;
using System.Collections.Generic;
internal class Program
{
    private static void Main(string[] args)
    {
        // Final Project Database Tool Ben Kropp
        Console.Clear();
        string nupath = "";
        string message = @"Please enter the mode you'd like:
1. Create a new database
2. Edit an existing database
3. View an existing database
4. Delete a database";
    string tempstore = "";
        static void ViewDB(ref string nupath, ref string message, ref string tempstore)
        {
            Console.WriteLine("Please enter which file you would like to view");
            tempstore = Console.ReadLine();
            string? path = nupath + "\\" + tempstore;
            
            if(File.Exists(@$"{path}")){
                string[] dbtemp = File.ReadAllLines(path);
                for(int i = 0; i < dbtemp.Length; i++){
                    Console.WriteLine(dbtemp[i]);
                }
                Console.WriteLine(@"1. Back
2. Edit this database
3. Delete this database");
            string? input = Console.ReadLine();
            Validate(input, 3, ref nupath, ref message, ref path, ref tempstore);
            //Console.WriteLine("This message is to say the file exists");
            } else{
                Console.WriteLine("Please enter a valid CSV filename");
                ViewDB(ref nupath, ref message, ref tempstore);
            }
        }
        static void EditDB(string editing, ref string path)
        {
            Console.Clear();
            Console.WriteLine(editing);
            string? nupath = "";
            if(File.Exists(path + "\\" + editing)){
                nupath = path + "\\" + editing;
                string[] backup = File.ReadAllLines(path + "\\" + editing);
                List<string> dbtemp = new List<string>();
                List<string> inputs = new List<string>();
                for (int i = 0; i < backup.Length; i++){
                    if(backup[i] != "") {
                        dbtemp.Add(backup[i]);
                    }
                }
                int morenumbers = dbtemp[1].ToString().Split(", ").Length;
                string[][] dbentries = new string[dbtemp.Count][];
                for(int i = 0; i < dbtemp.Count; i++){
                    dbentries[i] = dbtemp[i].ToString().Split(", ");
                }
                for(int i = 0; i < dbtemp.Count; i++){
                    if(dbtemp[i] != "")
                    Console.WriteLine(dbtemp[i]);
                }
                Console.WriteLine("Please select which line you would like to edit");
                string? input = Console.ReadLine();
                string change = "";
                int choice = 0;
                bool success = Int32.TryParse(input, out choice);
                if(success && choice > 0 && choice < dbtemp.Count) {
                    Console.WriteLine("Placeholder text while I work on this function");
                    for(int i = 0; i < dbentries.Length-1; i++){
                        Console.Clear();
                        Console.WriteLine(dbentries[0][i]);
                        inputs.Add(Console.ReadLine());
                        if (i != dbentries[0].Length - 1){
                            change = change + inputs[i] + ", ";
                        } else{
                            change = change + inputs[i];
                        }
                    }
                    //change = change.Trim();
                    Console.WriteLine($"Previous entry: \n{dbtemp[choice]}");
                    Console.WriteLine($"Current changes: \n{change}");
                    Console.WriteLine("Would you like to save your changes? y/n");
                    bool keepgoing = true;
                    while(keepgoing){
                    char save = Console.ReadKey(true).KeyChar;
                    if(save == 'y'){
                        dbtemp[choice] = change;
                        File.WriteAllLines(nupath, dbtemp);
                        keepgoing = false;
                    }else if (save == 'n'){
                        keepgoing = false;
                    }
                    }
                } else{
                    Console.WriteLine("Please enter a valid line to edit");

                }
            } else{
                Console.WriteLine("Please enter the filename you would like to access");
                nupath = Console.ReadLine();
                if(File.Exists(path + "\\" + nupath)){
                    //Console.WriteLine(path);
                EditDB(nupath, ref path);
                } else {
                    Console.WriteLine("Please enter a valid file location");
                    EditDB("", ref path);
                }
            }
        }
        static void CreateDB()
        {
            Console.WriteLine("Please enter a filename for this database without the extension");
            //Validate(Console.ReadLine(), 2);
        }
        static void DeleteDB()
        {
            Console.WriteLine("This will eventually be the Delete function (may be scrapped due to security reasons)");
        }
        static bool Validate(string test, int mode, ref string nupath, ref string message, ref string path, ref string tempstore)
        {
            bool num = false;
            int temp = 0;
            num = Int32.TryParse(test, out temp);
            //Console.WriteLine(num);
            switch(test){

            }
            if (Path.Exists(test) && mode == 1)
            {
                nupath = test;
                return true;
            }
            else if (!Path.Exists(test) && mode == 1)
            {
                Console.WriteLine("Please enter a valid file path");
                Validate(Console.ReadLine(), 1, ref nupath, ref message, ref path, ref tempstore);
                return false;
            }
            else if(mode == 2 && num){
                switch(temp){
                    case 1:
                    CreateDB();
                    break;
                    case 2:
                    EditDB("", ref path);
                    break;
                    case 3:
                    ViewDB(ref nupath, ref message, ref tempstore);
                    break;
                    case 4:
                    DeleteDB();
                    break;
                    default:
                    Console.WriteLine("You made it to the default option somehow");
                    break;
                }
            } 
                else if (mode == 3 && num){
                    switch(temp){
                        case 1:
                        Console.Clear();
                        Console.WriteLine(message);
                        Validate(Console.ReadLine(), 2, ref nupath, ref message, ref path, ref tempstore);
                        break;
                        case 2:
                        EditDB(tempstore, ref path);
                        break;
                        case 3:
                        break;
                        default:
                        break;
                    }
                }
            return false;
        }
        Console.WriteLine(@"Welcome to the UDT (Unnamed Database Tool)!
Press any key to continue");
        Console.ReadKey(true);
        Console.Clear();
        Console.WriteLine("Please enter the file location where you would like to access your database from");
        string? path = Console.ReadLine();
        Validate(path, 1, ref nupath, ref message, ref path, ref tempstore);
        Console.WriteLine(message);
Validate(Console.ReadLine(), 2, ref nupath, ref message, ref path, ref tempstore);
    }
}