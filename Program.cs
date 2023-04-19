using System.Linq;
using System.Collections.Generic;
internal class Program
{
    private static void Main(string[] args)
    {
        // Final Project Database Tool Ben Kropp
        Console.Clear();
        string nupath = "";
        static void ViewDB(ref string nupath)
        {
            Console.WriteLine("Please enter which file you would like to view");
            string? path = nupath + "\\" + Console.ReadLine();
            
            if(File.Exists(@$"{path}")){
                string[] dbtemp = File.ReadAllLines(path);
                for(int i = 0; i < dbtemp.Length; i++){
                    Console.WriteLine(dbtemp[i]);
                }
            //Console.WriteLine("This message is to say the file exists");
            } else{
                Console.WriteLine("Please enter a valid CSV filename");
                ViewDB(ref nupath);
            }
        }
        static void EditDB()
        {
            Console.WriteLine("This will eventually be the Edit function");
        }
        static void CreateDB()
        {
            Console.WriteLine("Please enter a filename for this database without the extension");
            //Validate(Console.ReadLine(), 2);
        }
        static void DeleteDB()
        {
            Console.WriteLine("This will eventually be the Delete function");
        }
        static bool Validate(string test, int mode, ref string nupath)
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
                Validate(Console.ReadLine(), 1, ref nupath);
                return false;
            }
            else if(mode == 2 && num){
                switch(temp){
                    case 1:
                    CreateDB();
                    break;
                    case 2:
                    EditDB();
                    break;
                    case 3:
                    ViewDB(ref nupath);
                    break;
                    case 4:
                    DeleteDB();
                    break;
                    default:
                    Console.WriteLine("You made it to the default option somehow");
                    break;
                }
            } return false;
        }
        Console.WriteLine(@"Welcome to the UDT (Unnamed Database Tool)!
Press any key to continue");
        Console.ReadKey(true);
        Console.WriteLine("Please enter the file location where you would like to access your database from");
        string? path = Console.ReadLine();
        Validate(path, 1, ref nupath);
        Console.WriteLine(@"Please enter the mode you'd like:
1. Create a new database
2. Edit an existing database
3. View an existing database
4. Delete a database");
Validate(Console.ReadLine(), 2, ref nupath);
    }
}