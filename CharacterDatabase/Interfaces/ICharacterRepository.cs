using CharacterDatabase.Models;

namespace CharacterDatabase
{
    public interface ICharacterRepository
    {
        public IEnumerable<Character> GetAllCharacters();        
        public Character GetCharacter(long id);
        public void UpdateCharacter(Character character);
        public void InsertCharacter(Character characterToInsert);        
        public void DeleteCharacter(Character character);
        public IEnumerable<Character> SearchForCharacter(List<string> searchParameters);
    }
}
