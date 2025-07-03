// See https://aka.ms/new-console-template for more information
using LibraryMgmt;


Start();
static async Task Start()

{

    bool play = true;
    DbOperations authorDbOperations = new DbOperations();

    do
    {
        Console.WriteLine("\n===== Library Management System =====");
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. Create a New Author");
        Console.WriteLine("2. Add Books to an Author");
        Console.WriteLine("3. List All Books with Their Authors");
        Console.WriteLine("4. Update a Book's Title");
        Console.WriteLine("5. Delete a Book");
        Console.WriteLine("6. Quit");
        Console.Write("Enter your choice (1-6): ");
     
            int option = int.Parse(Console.ReadLine());
        switch (option)
        {
            case 1:
                Console.WriteLine(authorDbOperations.AddAuthor());
                break;
            case 2:
                authorDbOperations.AddBooksToAuthors();
                break;
            case 3:
                authorDbOperations.ListAllBooks();
                break;

            case 4:
                authorDbOperations.UpdateBookTitle();
                break;

            case 5:
                authorDbOperations.deleteBook();
                break;

            case 6:
                Environment.Exit(0);
                break;

            default:

                Console.WriteLine("InValid Option ");
                break;

        }

        Console.WriteLine("-----------");
        Console.Write("Do you want to perform Db Operations action again? Enter 1 for Yes or 0 for No: ");
        if(!(int.Parse(Console.ReadLine()) == 1))
        {
            play = false;
        }

    } while (play);

}
