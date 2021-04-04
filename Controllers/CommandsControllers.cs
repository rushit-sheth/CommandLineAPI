using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controller

{  
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {

        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/commands
        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            //This is fine when we map item with empty DTO.
            return Ok(_mapper.Map<CommandReadDto>(commandItem));

        }

        //POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            //Command is empty for now, so it's fine to map like this, we are anyway inserting whole new entity.
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var CommandToRetrun = _mapper.Map<CommandReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById), new {Id = CommandToRetrun.Id}, CommandToRetrun);

            //return Ok(CommandToRetrun);

        }

        //not that efficient as we have to pass whole set to update one/few/all attribute.
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(CommandUpdateDto commandUpdateDto, int id)
        {
            var CommandModel = _repository.GetCommandById(id);

            if (CommandModel == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDto, CommandModel);
            //This is going to do nothing at all
            _repository.updateCommand(CommandModel);
            _repository.SaveChanges();
            return NoContent();
            
        }

        //This patch opeation will have some JSON array, passed in body and "op" attribute will tell us what we need to perform and path attribute will tell what to(which column) replace. E.g - Add, Replace, move etc.
        /*[
            {
                "op" : "replace",
                "path" : "/howto",
                "value" : "I have updated it with Patch"
            }
        ]*/
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var CommandModelFromRepo = _repository.GetCommandById(id);
            if(CommandModelFromRepo == null)
            {
                return NotFound();
            }

            var CommandToPatch = _mapper.Map<CommandUpdateDto>(CommandModelFromRepo);
            //this basically apply the update and ModelState checks for any validation issue.
            patchDoc.ApplyTo(CommandToPatch, ModelState);

            //Check for any validation issues we have and return that if there are any validation.
            if (!TryValidateModel(CommandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            //now we don't have any validation issue, so we will try to map CommandToPatch with CommandModelFromRepo. So updateDTO to Command.
            _mapper.Map(CommandToPatch, CommandModelFromRepo);
            //this line will do nothing
            _repository.updateCommand(CommandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand (int id)
        {
            var CommandModelFromRepo = _repository.GetCommandById(id);

            if (CommandModelFromRepo == null)
            {
                return NotFound();
            }
        
        //It will call the delete command from SqlCommanderRepo and entity will be deleted.
        _repository.DeleteCommand(CommandModelFromRepo);
        _repository.SaveChanges();

        return NoContent();

        }

    }
}