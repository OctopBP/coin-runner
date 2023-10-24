using System.Collections.Generic;
using CoinRunner.Effects;
using CoinRunner.Helpers;
using Game;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace CoinRunner.Track
{
    /// <summary> Builds the track view. </summary>
    public class TrackBuilder : MonoBehaviour
    {
        [SerializeField] Transform trackTile;
        [SerializeField] List<Effect> effectPrefabs;
	
        Config config;
        Random random;
        
        [Inject]
        public void Construct(Config config, Random random)
        {
            this.config = config;
            this.random = random;
        }
        
        public Model CreateTrackBuilder() => new(this, config, random);

        public class Model
        {
            readonly Transform parent;
            readonly EffectsFactory effectsFactory;

            readonly Pool<Transform> tiles;
            
            public Model(TrackBuilder backing, Config config, Random random) {
                effectsFactory = new(backing.effectPrefabs, config, random);
                tiles = new(() => Instantiate(backing.trackTile), t => t.gameObject.SetActive(false));
            }
            
            public void Build()
            {
                tiles.Borrow();
                effectsFactory.Launch();
            }
        }
    } 
}