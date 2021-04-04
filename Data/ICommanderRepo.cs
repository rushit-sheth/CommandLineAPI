using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        bool SaveChanges();
        //Give me list of our all command object
        IEnumerable<Command> GetAllCommands();
        //Find Command by Id
        Command GetCommandById(int id);
        void CreateCommand(Command cmd);

        void updateCommand(Command cmd);
        void DeleteCommand(Command cmd);
    }
}