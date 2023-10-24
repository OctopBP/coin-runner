using UnityEngine;
using Zenject;

namespace CoinRunner.Character
{
    /// <summary>
    /// Responsible for spawning character from prefab on the spawn point.
    /// Depends on the <see cref="Game.Config"/>
    /// </summary>
    public class CharacterFactory : MonoBehaviour
    {
        [SerializeField] Character characterPrefab;
        [SerializeField] Transform spawnPoint;

        Game.Config config;
        
        [Inject]
        public void Construct(Game.Config config)
        {
            this.config = config;
        }

        public Character.Model CreateCharacter()
        {
            var characterView = Instantiate(characterPrefab, spawnPoint.position, Quaternion.identity);
            var characterModel = new Character.Model(backing: characterView, config);
            return characterModel;
        }
    }
}
