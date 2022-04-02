using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    public class DevTeamRepo
    {
        private int _count=0;
        private readonly List<DevTeam> _devTeamDatabase = new List<DevTeam>();

        public bool AddDevTeamToDatabase(DevTeam devTeam)
        {
            if(devTeam != null)
            {
                _count++;
                
                devTeam.ID=_count;

                _devTeamDatabase.Add(devTeam);

                return true;
            }
            else
            {
                return false;
            }
        }
    
        public List<DevTeam> GetAllDevTeams()
        {
            return _devTeamDatabase;
        }
        public DevTeam GetDevTeamByID(int id)
        {
            foreach(DevTeam devTeam in _devTeamDatabase)
            {
                if(devTeam.ID==id)
                {
                    return devTeam;
                }
            }
            return null;
        }

        public bool UpdateDevTeamData(int devTeamID, DevTeam newDevTeamData)
        {
            var oldDevTeamData = GetDevTeamByID(devTeamID);

            if(oldDevTeamData != null)
            {
                oldDevTeamData.Name= newDevTeamData.Name;
                oldDevTeamData.Developers= newDevTeamData.Developers;
                return true;
            }
            else
            {
                return false;
            }
        }
    
        public bool RemoveDevTeamFromDatabase(int id)
        {
            var devTeam = GetDevTeamByID(id);
            if (devTeam != null)
            {
                _devTeamDatabase.Remove(devTeam);
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }

