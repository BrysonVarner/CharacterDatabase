using CharacterDatabase.Models;
using Microsoft.AspNetCore.Mvc;

namespace CharacterDatabase
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository repo;        

        public CharacterService(ICharacterRepository repo)
        {
            this.repo = repo;            
        }
        public IEnumerable<Character> GetAllCharacters()
        {
            var characters = repo.GetAllCharacters();                                
            return characters;
        }
        public IEnumerable<Character> GetAllUserCreatedCharacters()
        {
            var characters = repo.GetAllCharacters();
            return characters;
        }
        public Character GetCharacter(long id)
        {
            return repo.GetCharacter(id);
        }
        public void UpdateCharacter(Character character)
        {
            repo.UpdateCharacter(character);
        }
        public void InsertCharacter(Character characterToInsert)
        {
            repo.InsertCharacter(characterToInsert);
        }
        public void DeleteCharacter(Character character)
        {
            repo.DeleteCharacter(character);
        }

        public IEnumerable<Character> SearchForCharacter(string searchParameters)
        {
            var filteredParameters = searchParameters.Split(';', ',').ToList();
            var returnedSearchCharacters = repo.SearchForCharacter(filteredParameters);
            return returnedSearchCharacters;
            
            
        }
        
    }
}
