using ShowAddModifyDeleteDataJson;

bool more = false;
int choice;
string path;
string clientName;
DateTime registrationDate;

Client client;

List<Client> allClients = new List<Client>()
{
    new Client(null, null)
};

do
{
    Console.WriteLine();
    Console.WriteLine("Welcome to JSON file processing: Select an action(1 - 4)");
    Console.WriteLine("1. Print the contents of the file to the screen.");
    Console.WriteLine("2. Add a new client.");
    Console.WriteLine("3. Modify client information.");
    Console.WriteLine("4. Remove client.");
    Console.WriteLine();
    Console.Write("Select an action: ");

    string received = Console.ReadLine();
    while (!Int32.TryParse(received, out choice) || choice < 1 || choice > 4)
    {
        Console.Write("Not valid, try again: ");
        received = Console.ReadLine();
    }

    switch (choice)
    {
        case 1:
            Console.WriteLine("Your choice: Print the contents of the file to the screen.");
            do
            {
                Console.Write("Enter the file path: ");
                path = Console.ReadLine();
                if (String.IsNullOrEmpty(path))
                    Console.WriteLine("The field was left empty, try again!");

            } while (String.IsNullOrEmpty(path));



            //string polku = @"C:\Users\htimy01\source\JSON\client.json";

            allClients[0].PrintTheContents(path);
            break;
        case 2:
            Console.WriteLine("Your choice: Add a new client.");
            do
            {
                Console.Write("Enter the file path: ");
                path = Console.ReadLine();
                if (String.IsNullOrEmpty(path))
                    Console.WriteLine("The field was left empty, try again!");

            } while (String.IsNullOrEmpty(path));
            do
            {
                Console.Write("Enter the client's name: ");
                clientName = Console.ReadLine();
                if (String.IsNullOrEmpty(clientName))
                    Console.WriteLine("The field was left empty, try again!");

            } while (String.IsNullOrEmpty(clientName));

            Console.Write("Enter the date of registration: ");
            received = Console.ReadLine();
            while (!DateTime.TryParse(received, out registrationDate))
            {
                Console.Write("Not valid, try again: ");
                received = Console.ReadLine();
            }
            client = new Client(clientName, registrationDate);
            allClients[0].AddClient(path, client);
            break;
        case 3:
            Console.WriteLine("Your choice: Modify client information.");
            do
            {
                Console.Write("Enter the file path: ");
                path = Console.ReadLine();
                if (String.IsNullOrEmpty(path))
                    Console.WriteLine("The field was left empty, try again!");

            } while (String.IsNullOrEmpty(path));



            do
            {
                Console.Write("Enter the client's name: ");
                clientName = Console.ReadLine();
                if (String.IsNullOrEmpty(clientName))
                    Console.WriteLine("The field was left empty, try again!");

            } while (String.IsNullOrEmpty(clientName));
            allClients[0].ModifyInformation(path, clientName);
            break;
        case 4:
            Console.WriteLine("Your choice: Remove client.");
            do
            {
                Console.Write("Enter the file path: ");
                path = Console.ReadLine();
                if (String.IsNullOrEmpty(path))
                    Console.WriteLine("The field was left empty, try again!");
            } while (String.IsNullOrEmpty(path));
            do
            {
                Console.Write("Enter the client's name: ");
                clientName = Console.ReadLine();
                if (String.IsNullOrEmpty(clientName))
                    Console.WriteLine("The field was left empty, try again!");
            } while (String.IsNullOrEmpty(clientName));
            allClients[0].RemoveClient(path, clientName);
            break;
        default:
            Console.WriteLine("Your choice: You'll never get there.");
            break;
    }
    Console.WriteLine();
    Console.Write("Do you continue with the new operation? (Y/N): ");
    received = Console.ReadLine().ToUpper();
    if (received.StartsWith("Y"))
        more = true;
    else
        more = false;
} while (more);
