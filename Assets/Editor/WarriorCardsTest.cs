using System.Collections.Generic;
using Cards.CardActions;
using Cards.CardActions.Warrior;
using Entities;
using Entities.Enemies;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Assets;
using Infrastructure.Factories;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Buffs;
using Infrastructure.StaticData.Buffs.Warrior;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using Utilities;
using Zenject;

namespace Editor
{
    public class WarriorCardsTest
    {
        [Test]
        public void WhenDefenseCard_AndAttackLikeDefenseCardAction_ThenDamageShouldBeLikeArmor()
        {
            //Arrange
            var target = new GameObject().AddComponent<TestEnemy>();
            target.Init(100, "test", 10, 0, 0);

            var player = new GameObject().AddComponent<Player>();
            player.Init(10, 10, 10, 10, "player", 5, 0, 0);

            var defenseCardAction = new DefenseAction(5);
            var attackLikeDefenseCardAction = new AttackLikeDefenseCardAction();

            //Act
            defenseCardAction.Use(null, player);
            attackLikeDefenseCardAction.Use(new List<Entity>{target}, player);

            //Assert
            player.EntityHealth.Armor.Should().Be(10);
            target.EntityHealth.Armor.Should().Be(0);
            target.EntityHealth.Health.Should().Be(100);
        }
        
        [Test]
        public void WhenBeerCard_AndAoeAction_ThenDamageShouldBeMore()
        {
            //Arrange
            var target1 = new GameObject().AddComponent<TestEnemy>();
            target1.Init(100, "test", 0, 0, 0);
            
            var target2 = new GameObject().AddComponent<TestEnemy>();
            target2.Init(100, "test", 0, 0, 0);

            var player = new GameObject().AddComponent<Player>();
            player.Init(10, 10, 10, 10, "player", 0, 0, 0);

            var enemiesHolder = Substitute.For<IEnemiesHolder>();
            enemiesHolder.Enemies.Returns(new List<Enemy> {target1, target2});
            
            var beerCardAction = new DrinkBeerCardAction(4, 8);
            var aoeCardAction = new AoeCardAction(enemiesHolder, 2);
            var damageCardAction = new DefaultAttackAction(5);

            //Act
            beerCardAction.Use(null, player);
            aoeCardAction.Use(null, player);
            damageCardAction.Use(new List<Entity>{target1}, player);
            
            //Assert
            target1.EntityHealth.Health.Should().Be(85);
            target2.EntityHealth.Health.Should().Be(90);
            player.EntityHealth.Health.Should().Be(6);
        }

        [Test] 
        public void WhenSaloCard_AndEatCardAction_ThenShouldBeHealed()
        {
            //Arrange
            var container = new DiContainer();
            container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            container.Bind<IGameFactory>().FromInstance(new GameFactory(container, new AssetProvider(container), new StaticDataService(),
                Substitute.For<IPrefabFactory>(), Substitute.For<IPlayerHolder>(), Substitute.For<IEnemiesHolder>(),
                Substitute.For<FinderUnderCursor>(), Substitute.For<LocationHolder>(), Substitute.For<UIHolder>())).AsSingle();

            var player = container.InstantiatePrefabForComponent<Player>(Resources.Load<Player>(AssetPath.PlayerPath));
            player.Init(3, 3, 20, 50, "player", 0, 0, 0);

            var saloCard = new SaloCardAction(5, 2);
            var eatCard = new EatCardAction(4, 3);

            var eatBuffStaticData = (EatBuffStaticData) new StaticDataService().ForBuff(BuffId.WarriorEat);
            
            //Act
            saloCard.Use(null, player);
            eatCard.Use(null, player);
            
            player.Step();
            player.Step();

            //Assert
            player.EntityHealth.Health.Should().Be(eatBuffStaticData.Heal * 2 + 22);
            player.EntityHealth.Armor.Should().Be(5);
        }

        [Test]
        public void WhenBegaPunch_ThenDamageShouldBeTaken()
        {
            //Arrange
            var player = new GameObject().AddComponent<Player>();
            player.Init(10, 10, 40, 40, "player", 0, 0, 0);

            var target1 = new GameObject().AddComponent<TestEnemy>();
            target1.Init(100, "test", 0, 0, 0);
            var megaPunch = new MegaUltraPunchCardAction(2, 0.5f);
            var damageCard = new DefaultAttackAction(5);
            
            //Act
            megaPunch.Use(null, player);
            damageCard.Use(new List<Entity> {target1}, player);
            damageCard.Use(new List<Entity> {target1}, player);

            //Assert
            target1.EntityHealth.Health.Should().Be(85);
            player.EntityHealth.Health.Should().Be(35);
        }

        [Test]
        public void WhenValor_AndAttacked_ThenShouldBeBonusDamage()
        {
            //Arrange
            var target1 = new GameObject().AddComponent<TestEnemy>();
            target1.Init(100, "test", 0, 0, 0);
            
            var target2 = new GameObject().AddComponent<TestEnemy>();
            target2.Init(100, "test", 0, 0, 0);

            var player = new GameObject().AddComponent<Player>();
            player.Init(10, 10, 100, 100, "player", 0, 0, 0);

            var enemiesHolder = Substitute.For<IEnemiesHolder>();
            enemiesHolder.Enemies.Returns(new List<Enemy> {target1, target2});
            
            var valorCard = new ValorCardAction(enemiesHolder, 2);
            var damageCard = new DefaultAttackAction(5);
            
            //Act
            valorCard.Use(null, player);
            target1.AttackProcessor.Attack(6, player);
            target2.AttackProcessor.Attack(6, player);
            damageCard.Use(new List<Entity>{target1}, player);

            //Assert
            target1.EntityHealth.Health.Should().Be(89);
        }
    }
}