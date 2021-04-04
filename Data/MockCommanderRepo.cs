using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                    new Command {Id = 0, HowTo = "Make a tea", Line="Boil Water", Platform="Ketlle & cup" },
                    new Command {Id = 1, HowTo = "Cut a bread", Line="Take a knife", Platform="knife and chopping board" },
                    new Command {Id = 2, HowTo = "Make Nothing", Line="Sit back and relex", Platform="Sofa and bed" },
            };
            return commands;

        }

        public Command GetCommandById(int id)
        {
            return new Command {Id = 0, HowTo = "Make a tea", Line="Boil Water", Platform="Ketlle & cup" };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void updateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}