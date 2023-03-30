using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ShowAddModifyDeleteDataJson
{
    internal class Client
    {
        public Client(string? name, DateTime? dateOfRegistration)
        {
            Name = name;
            DateOfRegistration = dateOfRegistration;
        }

        public string? Name { get; set; }
        public DateTime? DateOfRegistration { get; set; }

        public void PrintTheContents(string path)
        {
            Console.WriteLine();
            List<Client> clients;

            using (StreamReader streamReader = new StreamReader(path))
            {
                var jsonString = streamReader.ReadToEnd();
                //Deserialize the JSON data into generic list type Client objects:
                clients = JsonConvert.DeserializeObject<List<Client>>(jsonString);
            }
            foreach (var item in clients)
            {
                Console.WriteLine("Name: {0}, date of registration: {1}.",
                    item.Name, item.DateOfRegistration.Value.ToString("d"));
            }
        }




        public void AddClient(string path, Client newClient)
        {
            Console.WriteLine();
            List<Client> clients;

            using (StreamReader streamReader = new StreamReader(path))
            {
                var jsonString = streamReader.ReadToEnd();
                //Deserialize the JSON data into generic list type Client objects:
                clients = JsonConvert.DeserializeObject<List<Client>>(jsonString);
            }

            //Add newClient to the end of the list:
            clients.Add(newClient);

            //true: append data to the file, false: overwrite the file
            //If the specified file does not exist, this parameter has no effect. The
            //constructor creates a new file.
            using (StreamWriter streamWriter = new StreamWriter(path, false))
            {
                string jsonString = JsonConvert.SerializeObject(clients);
                streamWriter.Write(jsonString);
            }
        }




        public void ModifyInformation(string path, string name)
        {
            bool ifChanged = false;
            Console.WriteLine();
            List<Client> clients;

            using (StreamReader streamReader = new StreamReader(path))
            {
                var jsonString = streamReader.ReadToEnd();
                //Deserialize the JSON data into generic list type Client objects:
                clients = JsonConvert.DeserializeObject<List<Client>>(jsonString);

                var chosen = clients.Where(cli => cli.Name.Contains(name));
                int numberOfClients = clients.Count;

                if (chosen.Any())
                {
                    //You enter here if there were wanted names...
                    for (int i = 0; i < numberOfClients; i++)
                    {
                        if (clients[i].Name.Contains(name))
                        {
                            Console.WriteLine("There is a customer {0} in the file.",
                                clients[i].Name);
                            Console.Write("Do you want to change the information? (Y/N): ");
                            string choice = Console.ReadLine().ToUpper();
                            if (choice.StartsWith("Y"))
                            {
                                //You are only here if changing data is selected
                                Console.WriteLine("Next, we go through all the information.");
                                Console.WriteLine("If you do not change any information,press ENTER at that point.");
                                Console.WriteLine("Current name is {0}.", clients[i].Name);
                                Console.Write("Enter new name: ");
                                string newName = Console.ReadLine();
                                if (!String.IsNullOrEmpty(newName))
                                {
                                    clients[i].Name = newName;
                                    ifChanged = true;
                                }



                                DateTime newRegistrationDate;
                                Console.WriteLine("Current registration date is {0}.",
                                    clients[i].DateOfRegistration);
                                Console.Write("Enter new date of registration: ");
                                string received = Console.ReadLine();
                                if (!String.IsNullOrEmpty(received))
                                {
                                    while (!DateTime.TryParse(received, out newRegistrationDate))
                                    {
                                        Console.Write("Not valid, try again: ");
                                        received = Console.ReadLine();
                                    }
                                    clients[i].DateOfRegistration = newRegistrationDate;
                                    ifChanged = true;
                                }
                            }
                        }
                    }
                    if (!ifChanged)
                        Console.WriteLine("You selected the change option, but no changes were made in the end.");
                }
                else
                {
                    Console.WriteLine("No customer with that name was found.");
                    Console.WriteLine();
                }
            }
            if (ifChanged)
            {
                //true: append data to the file, false: overwrite the file
                //If the specified file does not exist, this parameter has no effect. The
                //constructor creates a new file.
                using (StreamWriter streamWriter = new StreamWriter(path, false))
                {
                    string jsonString = JsonConvert.SerializeObject(clients);
                    streamWriter.Write(jsonString);
                }
                Console.WriteLine("Editing of data in the file was successful.");
                Console.WriteLine();
            }
        }



        public void RemoveClient(string path, string name)
        {
            bool ifChanged = false;
            Console.WriteLine();
            List<Client> clients;

            using (StreamReader streamReader = new StreamReader(path))
            {
                var jsonString = streamReader.ReadToEnd();
                //Deserialize the JSON data into generic list type Client objects:
                clients = JsonConvert.DeserializeObject<List<Client>>(jsonString);

                //Are there any clients with that name?
                var chosen = clients.Where(cli => cli.Name.Contains(name));
                int numberOfClients = clients.Count;

                if (chosen.Any())
                {
                    //You enter here if there were wanted names:
                    for (int i = 0; i < numberOfClients; i++)
                    {
                        if (clients[i].Name.Contains(name))
                        {
                            Console.WriteLine("There is a customer {0} in the file.",
                                clients[i].Name);
                            Console.Write("Do you want to delete the customer's data? (Y/N): ");
                            string choice = Console.ReadLine().ToUpper();
                            if (choice.StartsWith("Y"))
                            {
                                //You are here only if data deletion is selected:
                                string toBeRemoved = clients[i].Name;
                                bool succeeded = clients.Remove(clients[i]);
                                if (succeeded)
                                {
                                    Console.WriteLine("The information of client {0} was removed from the generic collection.", 
                                        toBeRemoved);
                                    numberOfClients = numberOfClients - 1;
                                    i = i - 1;
                                    ifChanged = true;
                                }



                                else
                                    Console.WriteLine("The customer's {0} information could not be deleted from the file.",
                                    toBeRemoved);
                            }
                        }
                    }
                    if (!ifChanged)
                        Console.WriteLine("You chose the delete option, but nothing was done in the end.");
                }
                else
                {
                    Console.WriteLine("No customer with that name was found.");
                    Console.WriteLine();
                }
            }
            if (ifChanged)
            {
                //true: append data to the file, false: overwrite the file
                //If the specified file does not exist, this parameter has no effect. The
                //constructor creates a new file.
                using (StreamWriter streamWriter = new StreamWriter(path, false))
                {
                    string jsonString = JsonConvert.SerializeObject(clients);
                    streamWriter.Write(jsonString);
                }
                Console.WriteLine("Deleting of data in the file was successful.");
                Console.WriteLine();
            }
        }
    }
}
