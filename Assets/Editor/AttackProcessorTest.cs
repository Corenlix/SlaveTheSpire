using Entities;
using Entities.Enemies;
using FluentAssertions;
using Infrastructure.Factories;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Enemies;
using NSubstitute;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace Editor
{
    public class AttackProcessorTest
    {
        [Test]
        public void WhenAttack_AndSetBonusDamageAndMultiplier_ThenDamageShouldBeMultiplied()
        {
            //Arrange
            var target = new GameObject().AddComponent<TestEnemy>();
            target.Init(100, "test", 10, 0, 0);

            var attackProcessor = new AttackProcessor
            {
                BonusDamage = 5,
                DamageMultiplier = 2f
            };

            //Act
            int totalDamage = attackProcessor.Attack(10, target);

            //Assert
            target.EntityHealth.Health.Should().Be(80);
            target.EntityHealth.Armor.Should().Be(0);
            totalDamage.Should().Be(30);
        }

        [Test]
        public void WhenAttack_AndSetBonusDamageAndDamageProcessor_ThenDamageShouldBeProcessedWithDamageProcessor()
        {
            //Arrange
            var target = new GameObject().AddComponent<TestEnemy>();
            target.Init(100, "test", 10, 0, 0);

            var attackProcessor = new AttackProcessor
            {
                BonusDamage = 5,
                DamageMultiplier = 2f
            };

            attackProcessor.AddDamageProcessor(new MultiplyDamageProcessor(2));

            //Act
            int totalDamage = attackProcessor.Attack(10, target);

            //Assert
            target.EntityHealth.Health.Should().Be(50);
            target.EntityHealth.Armor.Should().Be(0);
            totalDamage.Should().Be(60);
        }
    }
}