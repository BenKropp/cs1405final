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
4. Exit the app";
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
3. Exit the app");
            string? input = Console.ReadLine();
            Validate(input, 3, ref nupath, ref message, ref path, ref tempstore);
            //Console.WriteLine("This message is to say the file exists");
            } else{
                Console.WriteLine("Please enter a valid CSV filename");
                ViewDB(ref nupath, ref message, ref tempstore);
            }
        }
        static void EditDB(string editing, ref string path, ref string message, ref string tempstore)
        {
            Console.Clear();
            Console.WriteLine(editing);
            bool success = false;
            string? nupath = "";
            if(File.Exists(path + "\\" + editing)){
                string? input = "";
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
                for(int i = 0; i < dbtemp.Count-1; i++){
                    if(dbtemp[i] != "")
                    Console.WriteLine(dbtemp[i]);
                }
                int choice = 0;
                    Console.WriteLine("Would you like to add a new line? y/n");
                    char add = Console.ReadKey(true).KeyChar;
                    if(add == 'y'){
                        choice = dbtemp.Count;
                    } else{
                Console.WriteLine("Please select which line you would like to edit");
                input = Console.ReadLine();
                        
                    }
                string change = "";
                if(add != 'y'){
                success = Int32.TryParse(input, out choice);
                }
                if(choice > 0 && ((success && choice < dbtemp.Count) || add == 'y')) {
                    //Console.WriteLine("Placeholder text while I work on this function");
                    for(int i = 0; i < dbentries[0].Length; i++){
                        Console.Clear();
                        Console.WriteLine(dbentries[0][i]);
                        inputs.Add(Console.ReadLine());
                        if (i != dbentries[0].Length-1){
                            change = change + inputs[i] + ", ";
                        } else{
                            change = change + inputs[i];
                        }
                    }
                    //change = change.Trim();
                    if(add != 'y'){
                    Console.WriteLine($"Previous entry: \n{dbtemp[choice]}");
                    Console.WriteLine($"Current changes: \n{change}");
                    }
                    Console.WriteLine("Would you like to save your changes? y/n");
                    bool keepgoing = true;
                    while(keepgoing){
                    char save = Console.ReadKey(true).KeyChar;
                    if(save == 'y'){
                        if(add == 'y'){
                            dbtemp.Add(change);
                        }else {
                        dbtemp[choice] = change;
                        }
                        File.WriteAllLines(nupath, dbtemp);
                        keepgoing = false;
                    }else if (save == 'n'){
                        keepgoing = false;
                    }
                    }
                    Console.Clear();
                    Console.WriteLine(message);
                    Validate(Console.ReadLine(), 2, ref nupath, ref message, ref path, ref tempstore);
                } else{
                    Console.WriteLine("Please enter a valid line to edit");

                }
            } else{
                Console.WriteLine("Please enter the filename you would like to access");
                nupath = Console.ReadLine();
                if(File.Exists(path + "\\" + nupath)){
                    //Console.WriteLine(path);
                EditDB(nupath, ref path, ref message, ref tempstore);
                } else {
                    Console.WriteLine("Please enter a valid file location");
                    EditDB("", ref path, ref message, ref tempstore);
                }
            }
        }
        static void CreateDB(ref string nupath, ref string path, ref string message, ref string tempstore)
        {
            List<string> builder = new List<string>();
            string? input = "";
            char choice = ' ';
            bool prevsaved = false;
            string saveas = "";
            string temprow = "";
            Console.WriteLine("Please enter a filename for this database without the extension");
            input = Console.ReadLine();
            if(!File.Exists(path + "\\" + input + ".csv")){
                //The file can be generated
                saveas = path + "\\" + input + ".csv";
                while(choice != '2'){
                    Console.Clear();
                    if(prevsaved){
                        Console.WriteLine($"Previous entry saved as {input}");
                    }
                Console.WriteLine("1. Add new column \n2. Next step (warning: cannot add more columns after this point)");
                choice = Console.ReadKey(true).KeyChar;
                    if(choice == '1'){
                        Console.WriteLine("Please enter a title for this column");
                        input = Console.ReadLine();
                        prevsaved = true;
                        builder.Add(input);
                    } else if (choice != '1' && choice != '2'){
                        Console.WriteLine("That's not a valid choice, please enter '1' or '2'");
                    }
                }
                for(int i = 0; i < builder.Count; i++){
                    if(i == 0){
                    temprow = temprow + builder[i];
                    } else {
                        temprow = temprow + ", " + builder[i];
                    }
                }
                List<string> rows = new List<string>();
                rows.Add(temprow);
                Console.Clear();
                choice = '0';
                Console.WriteLine(temprow);
                Console.WriteLine("Finished adding columns");
                while(choice != '2' && choice != '3'){
                    Console.Clear();
                Console.WriteLine("1. Add new row \n2. Save file as .csv\n3. Quit without saving");
                choice = Console.ReadKey(true).KeyChar;
                if(choice == '1'){
                    temprow = "";
                    //Console.WriteLine("Placeholder for building a row");
                    for(int i = 0; i < builder.Count; i++){
                        Console.Clear();
                        Console.WriteLine(builder[i]);
                        if(i == 0){
                        temprow = temprow + Console.ReadLine();
                        } else{
                            temprow = temprow + ", " + Console.ReadLine();
                        }
                    }
                    rows.Add(temprow);
                    Console.WriteLine($"Last entry:\n{temprow}");
                } else if (choice == '2'){
                    File.WriteAllLines(saveas, rows);
                    if(File.Exists(saveas)){
                        Console.WriteLine("Successfully saved. Press any key to return to the main menu");
                        Console.ReadKey(true);
                        Console.Clear();
                        Console.WriteLine(message);
                        Validate(Console.ReadLine(), 2, ref nupath, ref message, ref path, ref tempstore);
                    }
                    }else if(choice != '2' && choice != '3'){
                    Console.WriteLine("Please enter a valid option");
                }
                }
            } else{
                Console.WriteLine("That file already exists, please enter a different name");
                CreateDB(ref nupath, ref path, ref message, ref tempstore);
            }
        }
        static void Exit()
        {
            Console.WriteLine("Exiting app...");
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
                    CreateDB(ref nupath, ref path, ref message, ref tempstore);
                    break;
                    case 2:
                    EditDB("", ref path, ref message, ref tempstore);
                    break;
                    case 3:
                    ViewDB(ref nupath, ref message, ref tempstore);
                    break;
                    case 4:
                    Exit();
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
                        EditDB(tempstore, ref path, ref message, ref tempstore);
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