using CharacterDatabase.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CharacterDatabase
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IDbConnection _conn;
        public CharacterRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public IEnumerable<Character> GetAllCharacters()
        {
            return _conn.Query<Character>("SELECT * FROM CHARACTERS;");
        }
            
        public Character GetCharacter(long id)
        {
            return _conn.QuerySingle<Character>("SELECT * FROM CHARACTERS WHERE CHARACTERID = @id",
                new { id = id });
        }
        public void UpdateCharacter(Character character)
        {
            _conn.Execute("UPDATE characters SET" +
                " name = @name," +
                " species = @species," +
                " gender = @gender," +
                " class1 = @class1," +
                " subclass1 = @subclass1," +
                " class1level = @class1level," +
                " class2 = @class2," +
                " subclass2 = @subclass2," +
                " class2level = @class2level," +
                " class3 = @class3," +
                " subclass3 = @subclass3," +
                " class3level = @class3level," +
                " height = @height," +
                " weight = @weight," +
                " age = @age," +
                " haircolor = @haircolor," +
                " eyecolor = @eyecolor," +
                " strength = @strength," +
                " dexterity = @dexterity," +
                " constitution = @constitution," +
                " intelligence = @intelligence," +
                " wisdom = @wisdom," +
                " charisma = @charisma," +
                " characternotes = @characternotes," +
                " userid = @userid," +
                " lastupdated = @lastupdated WHERE CharacterID = @id",
            new
            {
                name = character.Name,
                species = character.Species,
                gender = character.Gender,
                class1 = character.Class1,
                subclass1 = character.Subclass1,
                class1level = character.Class1Level,
                class2 = character.Class2,
                subclass2 = character.Subclass2,
                class2level = character.Class2Level,
                class3 = character.Class3,
                subclass3 = character.Subclass3,
                class3level = character.Class3Level,
                height = character.Height,
                weight = character.Weight,
                age = character.Age,
                haircolor = character.HairColor,
                eyecolor = character.EyeColor,
                strength = character.Strength,
                dexterity = character.Dexterity,
                constitution = character.Constitution,
                intelligence = character.Intelligence,
                wisdom = character.Wisdom,
                charisma = character.Charisma,
                characternotes = character.CharacterNotes,
                lastupdated = character.LastUpdated,
                userid = character.UserId,
                id = character.CharacterID,
            });
        }
        public void InsertCharacter(Character characterToInsert)
        {
            _conn.Execute("INSERT INTO characters" +
                "(NAME, SPECIES, GENDER, SPEED," +
                " CLASS1, SUBCLASS1, CLASS1LEVEL," +
                " CLASS2, SUBCLASS2, CLASS2LEVEL," +
                " CLASS3, SUBCLASS3, CLASS3LEVEL," +
                " HEIGHT, WEIGHT, AGE," +
                " HAIRCOLOR, EYECOLOR," +
                " STRENGTH, DEXTERITY, CONSTITUTION," +
                " INTELLIGENCE, WISDOM, CHARISMA," +
                " CHARACTERNOTES, LASTUPDATED, USERID) " +
            "VALUES (@name, @species, @gender, @speed, @class1, @subclass1, @class1level," +
            " @class2, @subclass2, @class2level," +
            " @class3, @subclass3, @class3level," +
            " @height, @weight, @age," +
            " @haircolor, @eyecolor," +
            " @strength, @dexterity, @constitution," +
            " @intelligence, @wisdom, @charisma," +
            " @characternotes, @lastupdated," +
            " @userid);",
                new
                {
                    name = characterToInsert.Name,
                    species = characterToInsert.Species,
                    gender = characterToInsert.Gender,
                    speed = characterToInsert.Speed,
                    class1 = characterToInsert.Class1,
                    subclass1 = characterToInsert.Subclass1,
                    class1level = characterToInsert.Class1Level,
                    class2 = characterToInsert.Class2,
                    subclass2 = characterToInsert.Subclass2,
                    class2level = characterToInsert.Class2Level,
                    class3 = characterToInsert.Class3,
                    subclass3 = characterToInsert.Subclass3,
                    class3level = characterToInsert.Class3Level,
                    height = characterToInsert.Height,
                    weight = characterToInsert.Weight,
                    age = characterToInsert.Age,
                    haircolor = characterToInsert.HairColor,
                    eyecolor = characterToInsert.EyeColor,
                    strength = characterToInsert.Strength,
                    dexterity = characterToInsert.Dexterity,
                    constitution = characterToInsert.Constitution,
                    intelligence = characterToInsert.Intelligence,
                    wisdom = characterToInsert.Wisdom,
                    charisma = characterToInsert.Charisma,
                    characternotes = characterToInsert.CharacterNotes,
                    lastupdated = characterToInsert.LastUpdated,
                    userid = characterToInsert.UserId,
                });
        }
        [Authorize]
        public void DeleteCharacter(Character character)
        {
            _conn.Execute("DELETE FROM Characters WHERE CharacterID = @id;",
                new { id = character.CharacterID });
        }

        public IEnumerable<Character> SearchForCharacter(List<string> searchParameters)
        {
            List<Character>? locatedMatches = new List<Character>();

            for (int i = 0; i < searchParameters.Count; i++)
            {
                string searchTerm = searchParameters[i].Trim();
                var characterMatch = _conn.Query<Character>($"SELECT * FROM characters WHERE Name LIKE '%{searchTerm}%'" +
                    $"UNION " +
                    $"SELECT * FROM characters WHERE Species LIKE '%{searchTerm}%'" +
                    $"UNION " +
                    $"SELECT * FROM characters WHERE Gender LIKE '%{searchTerm}%'" +
                    $"UNION " +
                    $"SELECT * FROM characters WHERE Class1 LIKE '%{searchTerm}%'" +
                    $"UNION " +
                    $"SELECT * FROM characters WHERE Subclass1 LIKE '%{searchTerm}%'" +
                    $"UNION " +
                    $"SELECT * FROM characters WHERE Class2 LIKE '%{searchTerm}%'" +
                    $"UNION " +
                    $"SELECT * FROM characters WHERE Subclass2 LIKE '%{searchTerm}%'" +
                    $"UNION " +
                    $"SELECT * FROM characters WHERE Class3 LIKE '%{searchTerm}%'" +
                    $"UNION " +
                    $"SELECT * FROM characters WHERE Subclass3 LIKE '%{searchTerm}%'" +
                    $"UNION " +
                    $"SELECT * FROM characters WHERE HairColor LIKE '%{searchTerm}%'" +
                    $"UNION " +
                    $"SELECT * FROM characters WHERE EyeColor LIKE '%{searchTerm}%'");
                if (characterMatch.Any())
                {
                    locatedMatches.AddRange(characterMatch);
                }
            }
            return locatedMatches; 
        }
    }

}
