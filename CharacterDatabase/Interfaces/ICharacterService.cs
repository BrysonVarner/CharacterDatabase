using CharacterDatabase.Models;
using Microsoft.AspNetCore.Mvc;

namespace CharacterDatabase
{
    public interface ICharacterService
    {
        public IEnumerable<Character> GetAllCharacters();        
        public Character GetCharacter(long id);
        public void UpdateCharacter(Character character);
        public void InsertCharacter(Character characterToInsert);
        public void DeleteCharacter(Character character);
        public IEnumerable<Character> SearchForCharacter(string searchParameters);
    }
}