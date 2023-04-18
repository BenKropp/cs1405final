// Final Project Database Tool Ben Kropp
Console.Clear();
static void AccessDB(string path){

}
static bool Validate(string test, int mode){
    if(Path.Exists(test) && mode == 1){
        return true;
    } else if (!Path.Exists(test) && mode == 1){
        Console.WriteLine("Please enter a valid file path");
        Validate(Console.ReadLine(), 1);
        return false;
    } else return false;
}
Console.WriteLine(@"Welcome to the UDT (Unnamed Database Tool)!
Press any key to continue");
Console.ReadKey(true);
Console.WriteLine("Please enter the file location where you would like to access your database from");
string? path = Console.ReadLine();
Validate(path, 1);