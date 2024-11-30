using Fire_Emblem.Models.Characters;
using System.Collections;
using Fire_Emblem.Models.Exceptions;

namespace Fire_Emblem.Models.Collections
{
    public class CharacterList : IEnumerable<Character>
    {
        private readonly List<Character> _characters;

        public CharacterList()
        {
            _characters = new List<Character>();
        }
        public void AddCharacter(Character character)
        {
            if (character == null)
            {
                throw new CharacterCannotBeNullException(nameof(character));
            }

            _characters.Add(character);
        }


        public bool Contains(Character character)
        {
            return character != null && _characters.Contains(character);
        }

        public void Remove(Character character)
        {
             _characters.Remove(character);
        }
        
        public int IndexOf(Character character)
        {

            for (int i = 0; i < _characters.Count; i++)
            {
                if (_characters[i] == character)
                {
                    return i;
                }
            }
            return -1;
        }

        public IReadOnlyList<Character> GetCharacters()
        {
            return _characters.AsReadOnly();
        }
        
        public IEnumerator<Character> GetEnumerator()
        {
            return _characters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}