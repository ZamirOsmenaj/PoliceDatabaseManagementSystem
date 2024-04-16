using Newtonsoft.Json;
using System.Text.Json;

namespace PoliceDatabaseManagementSystem {
    internal class Program {
        static void Main(string[] args) {

            // Create a list to store accusation objects
            WriteAccusationsInJSON();
            // Create a list to store Accusation objects
            List<Accusation> accusationsList = new List<Accusation>();
            ReadAccusationsFromJSON(accusationsList);

            // Create a list to store criminalGroup objects
            WriteCriminalGroupsInJSON();
            // Create a list to store criminalGroup objects
            List<CriminalGroup> criminalGroupsList = new List<CriminalGroup>();
            ReadCriminalGroupsFromJSON(criminalGroupsList);

            // Create the JSON file that will store the criminal records
            WriteCriminalRecordsInJSON(accusationsList, criminalGroupsList);
            // Create a list to store criminalRecord objects
            List<CriminalRecord> criminalRecordsList = new List<CriminalRecord>();
            ReadCriminalRecordsFromJSON(criminalRecordsList);

            // Create Police Database
            PoliceCriminalDatabase policeDatabase = new PoliceCriminalDatabase();

            for (int i = 0; i < criminalRecordsList.Count; i++) {
                policeDatabase.InsertCriminalRecord(criminalRecordsList[i]);
            }

            for (int i = 0; i < criminalGroupsList.Count; i++) {
                policeDatabase.InsertCriminalGroup(criminalGroupsList[i]);
            }

            // Random number generator
            Random random = new Random();

            // Iterate over each criminal group
            foreach (CriminalGroup group in criminalGroupsList) {
                // Randomly select 3 criminal records from the list
                List<CriminalRecord> selectedRecords = new List<CriminalRecord>();
                for (int i = 0; i < 3; i++) {
                    // Generate a random index within the range of criminalRecordsList.Count
                    int index = random.Next(criminalRecordsList.Count);
                    // Add the randomly selected criminal record to the selectedRecords list
                    selectedRecords.Add(criminalRecordsList[index]);
                }

                // Add the selected criminal records to the participating criminals list of the group
                foreach (CriminalRecord record in selectedRecords) {
                    group.AddCriminalToCriminalGroup(record);
                    // If the criminal record has an associated criminal group, update its reference to the current group
                    record.UpdateCriminalGroup(group);
                }
            }

            // Create third-party company for qualitative statistics
            ICrimesInsider3rdPartyCompany thirdPartyCompany = new CrimesInsider3rdPartyCompany(policeDatabase);

            // Test qualitative statistics
            Console.WriteLine("Most dangerous criminal: " + thirdPartyCompany.GetMostDangerousCriminal().Nickname + "\n");
            Console.WriteLine("Criminals sorted by severity:");
            foreach (var criminal in thirdPartyCompany.GetCriminalsSortedBySeverity()) {
                Console.WriteLine($"- {criminal.Nickname}, Severity: {criminal.AccusationDetails.Severity}");
            }
            Console.WriteLine("\nMost connected criminal: " + thirdPartyCompany.GetMostConnectedCriminal().Nickname);
            Console.WriteLine("\nAverage age of criminals: " + thirdPartyCompany.GetAverageAgeOfCriminals().ToString("F0"));
        }

        static void WriteAccusationsInJSON() {
            string jsonData = @"
            [
              {
                ""Type"": ""Theft"",
                ""Severity"": ""Medium"",
                ""Description"": ""Shoplifting"",
                ""DateOccurred"": ""2022-01-15""
              },
              {
                ""Type"": ""Assault"",
                ""Severity"": ""Low"",
                ""Description"": ""Assault with a deadly weapon"",
                ""DateOccurred"": ""2023-05-20""
              },
              {
                ""Type"": ""Homicide"",
                ""Severity"": ""High"",
                ""Description"": ""Murder"",
                ""DateOccurred"": ""2023-10-05""
              },
              {
                ""Type"": ""Theft"",
                ""Severity"": ""High"",
                ""Description"": ""Bank robbery"",
                ""DateOccurred"": ""2022-03-10""
              },
              {
                ""Type"": ""Assault"",
                ""Severity"": ""Medium"",
                ""Description"": ""Domestic violence"",
                ""DateOccurred"": ""2023-02-08""
              },
              {
                ""Type"": ""Fraud"",
                ""Severity"": ""Low"",
                ""Description"": ""Tax evasion"",
                ""DateOccurred"": ""2022-09-12""
              },
              {
                ""Type"": ""Theft"",
                ""Severity"": ""Low"",
                ""Description"": ""Petty theft"",
                ""DateOccurred"": ""2023-07-23""
              },
              {
                ""Type"": ""Assault"",
                ""Severity"": ""High"",
                ""Description"": ""Aggravated assault"",
                ""DateOccurred"": ""2022-11-30""
              },
              {
                ""Type"": ""Homicide"",
                ""Severity"": ""High"",
                ""Description"": ""Manslaughter"",
                ""DateOccurred"": ""2023-08-17""
              },
              {
                ""Type"": ""Fraud"",
                ""Severity"": ""Medium"",
                ""Description"": ""Embezzlement"",
                ""DateOccurred"": ""2022-05-06""
              },
              {
                ""Type"": ""Theft"",
                ""Severity"": ""Low"",
                ""Description"": ""Burglary"",
                ""DateOccurred"": ""2023-03-28""
              },
              {
                ""Type"": ""Assault"",
                ""Severity"": ""Medium"",
                ""Description"": ""Bar fight"",
                ""DateOccurred"": ""2022-07-14""
              },
              {
                ""Type"": ""Fraud"",
                ""Severity"": ""High"",
                ""Description"": ""Identity theft"",
                ""DateOccurred"": ""2023-06-09""
              },
              {
                ""Type"": ""Homicide"",
                ""Severity"": ""High"",
                ""Description"": ""Serial killing"",
                ""DateOccurred"": ""2022-02-03""
              },
              {
                ""Type"": ""Fraud"",
                ""Severity"": ""Low"",
                ""Description"": ""Insurance fraud"",
                ""DateOccurred"": ""2023-04-19""
              },
              {
                ""Type"": ""Assault"",
                ""Severity"": ""LifeThreatening"",
                ""Description"": ""Terrorist attack"",
                ""DateOccurred"": ""2022-08-20""
              }
            ]";

            // Write JSON data to a text file
            File.WriteAllText("accusations.json", jsonData);

        }

        static void ReadAccusationsFromJSON(List<Accusation> accusationsList) {
            // Read JSON data from the file
            string jsonFromFile = File.ReadAllText("accusations.json");

            // Parse JSON data into an array of dynamic objects
            JsonDocument doc = JsonDocument.Parse(jsonFromFile);

            // Get the root element of the JSON document
            JsonElement root = doc.RootElement;

            // Assuming the root element is an array, iterate over each item
            foreach (JsonElement item in root.EnumerateArray()) {
                // Access properties of each item
                CrimeType type = Enum.Parse<CrimeType>(item.GetProperty("Type").GetString());
                CrimeSeverity severity = Enum.Parse<CrimeSeverity>(item.GetProperty("Severity").GetString());
                string description = item.GetProperty("Description").GetString();
                DateTime dateOccurred = DateTime.Parse(item.GetProperty("DateOccurred").GetString());

                // Create Accusation object and add it to the list
                Accusation accusation = new Accusation(type, severity, description, dateOccurred);
                accusationsList.Add(accusation);
            }
        }

        static void WriteCriminalGroupsInJSON() {
            string jsonData = @"
            [
              {
                ""Name"": ""Gang Bank"",
                ""Records"": []
              },
              {
                ""Name"": ""Drug Cartel"",
                ""Records"": []
              },
              {
                ""Name"": ""Smuggling Ring"",
                ""Records"": []
              },
              {
                ""Name"": ""Cybercrime Syndicate"",
                ""Records"": []
              },
              {
                ""Name"": ""Human Trafficking Network"",
                ""Records"": []
              },
              {
                ""Name"": ""Arms Trafficking Organization"",
                ""Records"": []
              },
              {
                ""Name"": ""Extortion Ring"",
                ""Records"": []
              },
              {
                ""Name"": ""Money Laundering Operation"",
                ""Records"": []
              },
              {
                ""Name"": ""Forgery Ring"",
                ""Records"": []
              },
              {
                ""Name"": ""Counterfeiting Syndicate"",
                ""Records"": []
              },
              {
                ""Name"": ""Identity Theft Network"",
                ""Records"": []
              },
              {
                ""Name"": ""Prostitution Ring"",
                ""Records"": []
              },
              {
                ""Name"": ""Black Market Network"",
                ""Records"": []
              },
              {
                ""Name"": ""Organized Theft Crew"",
                ""Records"": []
              },
              {
                ""Name"": ""Terrorist Organization"",
                ""Records"": []
              }
            ]";

            // Write JSON data to a text file
            File.WriteAllText("criminalGroups.json", jsonData);
        }

        static void ReadCriminalGroupsFromJSON(List<CriminalGroup> criminalGroupsList) {
            // Read JSON data from the file
            string jsonFromFile = File.ReadAllText("criminalGroups.json");

            // Parse JSON data into an array of dynamic objects
            JsonDocument doc = JsonDocument.Parse(jsonFromFile);

            // Get the root element of the JSON document
            JsonElement root = doc.RootElement;

            // Assuming the root element is an array, iterate over each item
            foreach (JsonElement item in root.EnumerateArray()) {
                // Access properties of each item
                string name = item.GetProperty("Name").GetString();
                List<CriminalRecord> records = new List<CriminalRecord>(); // Empty list

                // Create Accusation object and add it to the list
                CriminalGroup criminalGroup = new CriminalGroup(name, records);
                criminalGroupsList.Add(criminalGroup);
            }
        }

        static void WriteCriminalRecordsInJSON(List<Accusation> accusationsList, List<CriminalGroup> criminalGroupsList) {

            List<object> jsonObjects = new List<object>();

            // Generate 15 instances of names, nicknames, and birthdates
            List<(string Name, string Nickname, DateTime Birthdate)> criminalNames = new List<(string, string, DateTime)>
            {
                ("John Doe", "Big Bad John", new DateTime(1980, 1, 1)),
                ("Jane Smith", "Sneaky Slinky Jane", new DateTime(1985, 2, 2)),
                ("Michael Johnson", "Mighty Mike", new DateTime(1975, 3, 3)),
                ("Alice Brown", "Ace Bandit", new DateTime(1990, 4, 4)),
                ("Robert Wilson", "Robo-Bobby", new DateTime(1982, 5, 5)),
                ("Emily Davis", "Epic Em", new DateTime(1988, 6, 6)),
                ("David Jones", "Dangerous DJ", new DateTime(1979, 7, 7)),
                ("Sarah Taylor", "Sassy Sharpshooter", new DateTime(1987, 8, 8)),
                ("Christopher Clark", "Cunning Chris", new DateTime(1983, 9, 9)),
                ("Jessica Martinez", "Jazzy Jess", new DateTime(1981, 10, 10)),
                ("William White", "Wild Will", new DateTime(1976, 11, 11)),
                ("Amanda Garcia", "Amazing Mandy", new DateTime(1992, 12, 12)),
                ("Matthew Rodriguez", "Mad Matt", new DateTime(1984, 1, 13)),
                ("Olivia Lopez", "Outlaw Liv", new DateTime(1989, 2, 14)),
                ("Daniel Lee", "Daring Danny", new DateTime(1986, 3, 15))
            };

            for (int i = 0; i < criminalNames.Count; i++)
{
                // Randomly select accusation and group
                Accusation accusation = accusationsList[new Random().Next(accusationsList.Count)];
                CriminalGroup group = criminalGroupsList[new Random().Next(criminalGroupsList.Count)];

                // Create JSON object for the criminal
                var jsonObject = new {
                    Name = criminalNames[i].Name,
                    Nickname = criminalNames[i].Nickname,
                    Birthdate = criminalNames[i].Birthdate,
                    Accusation = accusation,
                    Group = group
                };

                // Add JSON object to the list
                jsonObjects.Add(jsonObject);
            }

            // Serialize the list of JSON objects to JSON format
            string json = JsonConvert.SerializeObject(jsonObjects, Newtonsoft.Json.Formatting.Indented);

            // Write the JSON data to a file
            File.WriteAllText("criminalRecord.json", json);
        }

        static void ReadCriminalRecordsFromJSON(List<CriminalRecord> criminalRecordsList) {
            // Read JSON data from the file
            string jsonFromFile = File.ReadAllText("criminalRecord.json");

            // Parse JSON data into an array of dynamic objects
            JsonDocument doc = JsonDocument.Parse(jsonFromFile);

            // Get the root element of the JSON document
            JsonElement root = doc.RootElement;

            // Assuming the root element is an array, iterate over each item
            foreach (JsonElement item in root.EnumerateArray()) {
                // Access properties of each item
                string name = item.GetProperty("Name").GetString();
                string nickname = item.GetProperty("Nickname").GetString();
                DateTime birthdate = DateTime.Parse(item.GetProperty("Birthdate").GetString());

                // Access Accusation sub-object
                JsonElement accusationElement = item.GetProperty("Accusation");
                int accusationType = accusationElement.GetProperty("Type").GetInt32();
                int accusationSeverity = accusationElement.GetProperty("Severity").GetInt32();
                string accusationDescription = accusationElement.GetProperty("Description").GetString();
                DateTime accusationDateOccurred = DateTime.Parse(accusationElement.GetProperty("DateOccurred").GetString());

                Accusation accusation = new Accusation((CrimeType)accusationType, (CrimeSeverity)accusationSeverity, accusationDescription, accusationDateOccurred);

                // Access Group sub-object
                JsonElement groupElement = item.GetProperty("Group");
                string groupName = groupElement.GetProperty("Name").GetString();

                CriminalGroup group = new CriminalGroup(groupName, new List<CriminalRecord>());


                CriminalRecord criminalRecord = new CriminalRecord(name, nickname, birthdate, accusation, group);
                criminalRecordsList.Add(criminalRecord);

            }
        }
    }
}
