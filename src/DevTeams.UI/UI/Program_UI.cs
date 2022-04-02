using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public class Program_UI
    {
        private readonly DevTeamRepo _dtRepo = new DevTeamRepo();
        private readonly Devloper_Repo _dRepo = new Devloper_Repo();

        public void Run()
        {
            SeedData();
            RunApplication();
        }




    private void SeedData()
    {
        var chris = new Developer("Chris","Eley");
        var terry = new Developer("Terry","Brown");
        var robert = new Developer("Robert","Rupp");
        var donald = new Developer("Donald","Morefield");
        var ricky = new Developer("Ricky","Bobby");
        var michael = new Developer("Michael","Scarn");
        var dwight = new Developer("Dwight","Schrute");
        var jim = new Developer("Jim","Halpert");
        var kevin = new Developer("Kevin","Malone");
        _dRepo.AddDeveloperToDatabase(chris);
        _dRepo.AddDeveloperToDatabase(terry);
        _dRepo.AddDeveloperToDatabase(robert);
        _dRepo.AddDeveloperToDatabase(donald);
        _dRepo.AddDeveloperToDatabase(ricky);
        _dRepo.AddDeveloperToDatabase(michael);
        _dRepo.AddDeveloperToDatabase(dwight);
        _dRepo.AddDeveloperToDatabase(jim);
        _dRepo.AddDeveloperToDatabase(kevin);

        var indy = new DevTeam("Team Indy",new List<Developer>
        {
            chris,
            terry,
            robert
        });
        var denver = new DevTeam("Team Denver", new List<Developer>
        {
            donald,
            ricky,
            michael
        });
        var phoenix = new DevTeam("Team Phoenix", new List<Developer>
        {
            dwight,
            jim,
            kevin
        });
        _dtRepo.AddDevTeamToDatabase(indy);
        _dtRepo.AddDevTeamToDatabase(denver);
        _dtRepo.AddDevTeamToDatabase(phoenix);
    }

    private void RunApplication()
    {
        bool isRunning= true;
        while(isRunning)
        {
            Console.Clear();
            System.Console.WriteLine("==Komodo Insurance Devoloper Teams==");
            System.Console.WriteLine("Please choose one of the following: \n"+
            "1. Add new team\n"+
            "2. View all teams\n"+
            "3. View team by ID\n"+
            "4. Update team\n"+
            "==Developers==\n"+
            "5. Add new developer\n"+
            "6. View all developers\n"+
            "7. View developer by ID\n"+
            "8. Remove developer from system\n"+
            "-------------------------------------\n"+
            "10. Close application"
            );

            var userInput = Console.ReadLine();

            switch(userInput)
            {
                case "1":
                AddNewTeam();
                break;
                 case "2":
                ViewAllTeams();
                break;
                 case "3":
                ViewTeamByID();
                break;
                 case "4":
                UpdateTeam();
                break;
                 case "5":
                AddNewDeveloper();
                break;
                 case "6":
                ViewAllDevelopers();
                break;
                 case "7":
                ViewDeveloperByID();
                break;
                 case "8":
                RemoveDeveloperFromSystem();
                break;
                 case "10":
                isRunning = CloseApplication();
                break;
                default:
                System.Console.WriteLine("Invalid Selection");
                PressAnyKeyToContinue();
                break;
            }
        }
    }

    private bool CloseApplication()
    {
        Console.Clear();
        System.Console.WriteLine("See you next time!");
        PressAnyKeyToContinue();
        return false;
    }

    private void RemoveDeveloperFromSystem()
    {
        Console.Clear();
        System.Console.WriteLine("==Developer Removal Page==");

        var developers = _dRepo.GetAllDevelopers();
        foreach (Developer developer in developers)
        {
            DisplayDeveloperData(developer);
        }
        try
        {
            System.Console.WriteLine("Please select a developer by their ID:");
            var userInputSelectedDev = int.Parse(Console.ReadLine());
            bool isSuccessful= _dRepo.RemoveDeveloperFromSystem(userInputSelectedDev);
            if(isSuccessful)
            {
                System.Console.WriteLine("Develeoper was successfully deleted.");
            }
            else
            {
                System.Console.WriteLine("Failed to delete developer.");
            }
        }
        catch
        {
            System.Console.WriteLine("Invalid selection.");
        }
        PressAnyKeyToContinue();
    }

    private void ViewDeveloperByID()
    {
        Console.Clear();
        System.Console.WriteLine("==Developer Menu==\n");
        System.Console.WriteLine("Please enter a Developer ID: \n");
        var userInputDeveloperID = int.Parse(Console.ReadLine());

        var developer = _dRepo.GetDeveloperByID(userInputDeveloperID);
        
        if (developer != null)
        {
            DisplayDeveloperData(developer);
        }
        else
        {
            System.Console.WriteLine($"A developer with the ID {userInputDeveloperID} doesn't exist");
        }
        PressAnyKeyToContinue();
    }

    private void ViewAllDevelopers()
    {
        List<Developer> developersInDb = _dRepo.GetAllDevelopers();
        foreach(Developer developer in developersInDb)
        {
            DisplayDeveloperData(developer);
        }

        PressAnyKeyToContinue();
    }

    private void DisplayDeveloperData(Developer developer)
    {
        System.Console.WriteLine($"DeveloperID: {developer.ID}\n"+
        $"DeveloperName: {developer.FirstName} {developer.LastName}\n"+
        "----------------------------------\n");
    }

    private void AddNewDeveloper()
    {
        Console.Clear();
        var newDeveloper = new Developer();
        System.Console.WriteLine("[==Developer Enlisting Form==\n");

        System.Console.WriteLine("Please enter developer first name:");
        newDeveloper.FirstName =Console.ReadLine();

        System.Console.WriteLine("Please enter developer last name:");
        newDeveloper.LastName =Console.ReadLine();

        bool isSuccessful = _dRepo.AddDeveloperToDatabase(newDeveloper);
        if(isSuccessful)
        {
            System.Console.WriteLine("${newDeveloper.FirstName} - {newDeveloper.LastName} was added to the database");
        }
        else
        {
            System.Console.WriteLine("Developer failed to be added.");
        }

    }

    private void UpdateTeam()
    {
        Console.Clear();
        var availDevTeams = _dtRepo.GetAllDevTeams();
        foreach (var devTeam in availDevTeams)
        {
            DisplayTeamInfo(devTeam);
        }

        System.Console.WriteLine("Please enter a valid team ID:");
        var userInputTeamID = int.Parse(Console.ReadLine());
        var userSelectedTeam = _dtRepo.GetDevTeamByID(userInputTeamID);

        if (userSelectedTeam != null)
        {
            Console.Clear();
            var newDevTeam = new DevTeam();

            var currentDevelopers = _dRepo.GetAllDevelopers();

            System.Console.WriteLine("Please enter a team name:");
            newDevTeam.Name = Console.ReadLine();

            bool hasAssignedDevs = false;
            while (!hasAssignedDevs)
            {
                System.Console.WriteLine("Are the developers on this team? y/n");
                var userInputHasDevs = Console.ReadLine();

                if (userInputHasDevs == "Y".ToLower())
                {
                    foreach (var developer in currentDevelopers)
                    {
                        System.Console.WriteLine($"{developer.ID} {developer.FirstName} {developer.LastName}");
                    }

                    var userInputDeveloperSelection = int.Parse(Console.ReadLine());
                    var selectedDeveloper = _dRepo.GetDeveloperByID(userInputDeveloperSelection);

                    if (selectedDeveloper != null)
                    {
                        newDevTeam.Developers.Add(selectedDeveloper);
                        currentDevelopers.Remove(selectedDeveloper);
                    }
                    else
                    {
                        System.Console.WriteLine($"Sorry, a developer with the ID {userInputDeveloperSelection} doesn't exist.");
                    }
                }
                else
                {
                    hasAssignedDevs = true;
                }

                var isSuccessful = _dtRepo.UpdateDevTeamData(userSelectedTeam.ID, newDevTeam);
                if(isSuccessful)
                {
                    System.Console.WriteLine("Successfully updated.");
                }
                else
                {
                    System.Console.WriteLine("Failed to update");
                }

            }
           
        }
        else
        {
            System.Console.WriteLine($"Sorry a team with the ID {userInputTeamID} doesn't exist");
        }

        PressAnyKeyToContinue();
    }

    private void ViewTeamByID()
    {
             Console.Clear();
        System.Console.WriteLine("==Teams Menu==\n");
        System.Console.WriteLine("Please enter a Team ID: \n");
        var userInputTeamID = int.Parse(Console.ReadLine());

        var team = _dtRepo.GetDevTeamByID(userInputTeamID);
        
        if (team != null)
        {
            DisplayTeamInfo(team);
        }
        else
        {
            System.Console.WriteLine($"A Team with the ID {userInputTeamID} doesn't exist");
        }
        PressAnyKeyToContinue();
    }

    private void DisplayTeamInfo(DevTeam team)
    {
         System.Console.WriteLine($"Team Id: {team.ID}\n"+
        $"Team Name: {team.Name}\n"+
        "----------------------------------\n");
        foreach(var developer in team.Developers)
        {
            DisplayDeveloperData(developer);
        }
        
    }

    private void ViewAllTeams()
    {
          List<DevTeam> teamsInDb = _dtRepo.GetAllDevTeams();
        foreach(DevTeam devTeam in teamsInDb)
        {
            DisplayTeamInfo(devTeam);
        }

        PressAnyKeyToContinue();
    }

    private void AddNewTeam()
    {
     Console.Clear();
     var newDevTeam = new DevTeam();
     var currentDevelopers = _dRepo.GetAllDevelopers();
     
     System.Console.WriteLine("Please enter team name:");
     newDevTeam.Name = Console.ReadLine();

     bool hasAssignedDevs = false;
     while (!hasAssignedDevs)
     {
         System.Console.WriteLine("Are there any developers assigned to this team? y/n");
         var userInputHasDevs = Console.ReadLine();

         if(userInputHasDevs == "Y".ToLower())
         {
             foreach (var developer in currentDevelopers)
             {
                 System.Console.WriteLine($"{developer.ID} {developer.FirstName} {developer.LastName}");
             }
             
             var userInputDeveloperSelection = int.Parse(Console.ReadLine());
             var selectedDeveloper = _dRepo.GetDeveloperByID(userInputDeveloperSelection);

             if (selectedDeveloper != null)
             {
                 newDevTeam.Developers.Add(selectedDeveloper);
                 currentDevelopers.Remove(selectedDeveloper);
             }
             else
             {
                 System.Console.WriteLine($"Sorry, a developer with the ID {userInputDeveloperSelection} doesn't exist.");
             }
         }
        else
        {
            hasAssignedDevs = true;
        }
    }

    bool isSuccessful = _dtRepo.AddDevTeamToDatabase(newDevTeam);
    if(isSuccessful)
    {
        System.Console.WriteLine($"Team {newDevTeam.Name} was added to the database.");
    }
    else
    {
        System.Console.WriteLine("Failed to add new team.");
    }

    PressAnyKeyToContinue(); 
     
    }

    private void PressAnyKeyToContinue()
    {
        System.Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
}
