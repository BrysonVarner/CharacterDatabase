using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CharacterDatabase.Data;
using CharacterDatabase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CharacterDatabase.Controllers
{

    public class CharacterController : Controller
    {
        private readonly ICharacterService _characterService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        public CharacterController(ICharacterService characterService, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _characterService = characterService;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var charactersList = _characterService.GetAllCharacters();
            return View(charactersList);
        }

        public IActionResult UserHome()
        {
            var charactersListFiltered = _characterService.GetAllCharacters();
            return View(charactersListFiltered);
        }

        public IActionResult ViewCharacter(long id)
        {
            var character = _characterService.GetCharacter(id);
            return View(character);
        }

        public IActionResult UpdateCharacter(int id)
        {
            Character getChar = _characterService.GetCharacter(id);

            if (getChar == null)
            {
                return View("CharacterNotFound");
            }

            return View(getChar);
        }
        public IActionResult UpdateCharacterToDatabase(Character character)
        {
            if (ModelState.IsValid)
            {
                _characterService.UpdateCharacter(character);
                
            }

            return RedirectToAction("ViewCharacter", new { id = character.CharacterID });

        }
        public IActionResult InsertCharacter()
        {
            var newUnnamedCharacter = new Character();
            return View(newUnnamedCharacter);
        }
        public IActionResult InsertCharacterToDatabase(Character characterToInsert)
        {
            _characterService.InsertCharacter(characterToInsert);            
            return RedirectToAction("Index");
        }
        public IActionResult DeleteCharacter(Character character)
        {
            _characterService.DeleteCharacter(character);
            return RedirectToAction("Index");
        }
        public IActionResult SearchForCharacter(string searchParameters)
        {
            var returnedSearchCharacters = _characterService.SearchForCharacter(searchParameters);
            if (returnedSearchCharacters != null)
            {
                return View(returnedSearchCharacters);
            }
            else
            {
                return View("CharacterNotFound");
            }
        }



    }
}
