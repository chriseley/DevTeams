using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class Devloper_Repo
{
    private readonly List<Developer> _developerDatabase = new List<Developer>();
    private int _count;
    public bool AddDeveloperToDatabase(Developer developer)
    {
        if (developer != null)
        {
            _count++;
            developer.ID = _count;
            _developerDatabase.Add(developer);
            return true;
        }
        else
        {
            return false;
        }
    }
    public List<Developer> GetAllDevelopers()
    {
        return _developerDatabase;
    }
    public Developer GetDeveloperByID(int id)
    {
        foreach (var developer in _developerDatabase)
        {
            if (developer.ID == id)
            {
                return developer;
            }
        }
        return null;
    }
    public bool UpdateDeveloperData(int id, Developer newDeveloperData)
    {
        var oldDevData = GetDeveloperByID(id);
        if (newDeveloperData != null)
        {
            oldDevData.FirstName = newDeveloperData.FirstName;
            oldDevData.LastName = newDeveloperData.LastName;
            oldDevData.HasPluralSightAccess = newDeveloperData.HasPluralSightAccess;
            return true;
        }
        return false;
    }
    public bool RemoveDeveloperFromSystem(int id)
    {
        var developer = GetDeveloperByID(id);
        if (developer != null)
        {
            _developerDatabase.Remove(developer);
            return true;
        }
        else
        {
            return false;
        }

    }
}
