using Entities;
using Entities.Enemies;
using FluentAssertions;
using Infrastructure.StaticData.Enemies;
using NUnit.Framework;
using UnityEngine;

namespace Editor
{
    public class AttackProcessorTest
    {
        [Test]
        public void Test()
        {
            //Arrange
            Enemy target = new GameObject().AddComponent<TestEnemy>();
            var targetStaticData = Substitute.For<TestEnemyStaticData>();
            targetStaticData.MaxHealth.Returns(20);
            target.Init(targetStaticData);

            //Act

            //Assert
            target.EntityHealth.Health.Should().Be(20);
        }
    }
}