using System.Collections;
using System.Collections.Generic;
using Entities;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EntityHealthTest
{
    [Test]
    public void WhenApplyDamage_AndArmorMoreThanDamage_ThenHealthIsNotChanged()
    {
        //Arrange
        var entityHealth = new EntityHealth(10, 10, 10);
        
        //Act
        int appliedDamage = entityHealth.ApplyDamage(5);

        //Assert
        entityHealth.Armor.Should().Be(5);
        entityHealth.Health.Should().Be(10);
        appliedDamage.Should().Be(5);
    }
    
    [Test]
    public void WhenApplyDamage_AndArmorLessThanDamage_ThenHealthIsChanged()
    {
        //Arrange
        var entityHealth = new EntityHealth(10, 10, 10);
        
        //Act
        int appliedDamage = entityHealth.ApplyDamage(15);

        //Assert
        entityHealth.Armor.Should().Be(0);
        entityHealth.Health.Should().Be(5);
        appliedDamage.Should().Be(15);
    }
    
    [Test]
    public void WhenApplyDamageThroughArmor_AndArmorMorThanZero_ThenArmorIsNotChanged()
    {
        //Arrange
        var entityHealth = new EntityHealth(10, 10, 10);
        
        //Act
        int appliedDamage = entityHealth.ApplyDamageThroughArmor(5);

        //Assert
        entityHealth.Armor.Should().Be(10);
        entityHealth.Health.Should().Be(5);
        appliedDamage.Should().Be(5);
    }
    
    [Test]
    public void WhenApplyHeal_AndHealthToMaxLessThanHeal_ThenHealIsFull()
    {
        //Arrange
        var entityHealth = new EntityHealth(3, 10, 10);
        
        //Act
        int appliedHeal = entityHealth.ApplyHeal(5);

        //Assert
        entityHealth.Armor.Should().Be(10);
        entityHealth.Health.Should().Be(8);
        appliedHeal.Should().Be(5);
    }
    
    [Test]
    public void WhenApplyHeal_AndHealthToMaxMoreThanHeal_ThenHealIsNotFull()
    {
        //Arrange
        var entityHealth = new EntityHealth(8, 10, 10);
        
        //Act
        int appliedHeal = entityHealth.ApplyHeal(5);

        //Assert
        entityHealth.Armor.Should().Be(10);
        entityHealth.Health.Should().Be(10);
        appliedHeal.Should().Be(2);
    }
    
    [Test]
    public void WhenApplyDamage_AndHealthIsLessThanDamage_ThenDamageIsNotFull()
    {
        //Arrange
        var entityHealth = new EntityHealth(8, 10, 0);
        
        //Act
        int appliedDamage = entityHealth.ApplyDamage(10);

        //Assert
        entityHealth.Armor.Should().Be(0);
        entityHealth.Health.Should().Be(0);
        appliedDamage.Should().Be(8);
    }
    
    [Test]
    public void WhenApplyDamage_AndHealthAndArmorIsLessThanDamage_ThenDamageIsNotFull()
    {
        //Arrange
        var entityHealth = new EntityHealth(10, 10, 5);
        
        //Act
        int appliedDamage = entityHealth.ApplyDamage(20);

        //Assert
        entityHealth.Armor.Should().Be(0);
        entityHealth.Health.Should().Be(0);
        appliedDamage.Should().Be(15);
    }
    
    [Test]
    public void WhenAddArmor_AndArmorIsZero_ThenDamageIsAdded()
    {
        //Arrange
        var entityHealth = new EntityHealth(8, 10, 0);
        
        //Act
        entityHealth.AddArmor(10);

        //Assert
        entityHealth.Armor.Should().Be(10);
    }
}
