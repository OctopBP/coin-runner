using System;
using System.Collections.Generic;
using System.Linq;
using CoinRunner.Effects;
using CoinRunner.Helpers;
using Game;
using UniRx;
using UnityEngine;
using Random = System.Random;

namespace CoinRunner.Track
{
    /// <summary> Responsible for creating different effects on the track. </summary>
    public class EffectsFactory
    {
        readonly Config config;
        readonly Random random;
        readonly List<EffectSpawner> effectSpawners;
        
        // Ho many effects was spawned
        readonly ReactiveProperty<int> spawnedEffectsCountRx = new();
        // How many active effects on the fields
        readonly ReactiveProperty<int> activeEffectsOnTheFieldRx = new();

        public EffectsFactory(List<Effect> effects, Config config, Random random)
        {
            this.config = config;
            this.random = random;

            effectSpawners = effects.Select(effect => new EffectSpawner(effect)).ToList();
        }

        /// <summary>
        /// Starts spawning effects.
        /// New effects will spawn with a delay until the maximum number is reached.
        /// </summary>
        public void Launch()
        { 
            var spawning = activeEffectsOnTheFieldRx
                .Where(count => count < config._effectsInField)
                .Delay(TimeSpan.FromSeconds(config._spawnDelay))
                .Subscribe(_ => SpawnRandomEffect());
            
            // Dispose spawning when reach maxEffectsCount
            spawnedEffectsCountRx
                .Where(count => count > config._maxEffectsCount)
                .Subscribe(_ => spawning.Dispose());
        }
        
        void SpawnRandomEffect()
        {
            // Get random spawner
            var spawner = effectSpawners.GetRandomElement(random);
            
            // Create new effect
            var xPosition = config._startOffset + spawnedEffectsCountRx.Value * config._spaceBetweenEffects;
            var spawnPosition = new Vector3(0, 1, xPosition);
            var newEffect = spawner.Create(spawnPosition);
            
            // When effect will be collected we will release it and change activeEffectsOnTheFieldRx
            newEffect.onCollect.Subscribe(effect =>
            {
                activeEffectsOnTheFieldRx.Value--;
                spawner.Release(effect);
            });
            
            // Increase counters
            spawnedEffectsCountRx.Value++;
            activeEffectsOnTheFieldRx.Value++;
        }
    }
}