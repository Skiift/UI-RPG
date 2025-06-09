The player can attack, use a shield and heal. After the enemy dies, a new one appears and the game continues. If the player dies, the game ends.
The project uses all four main principles of object-oriented programming:

Inheritance
An abstract base class Character is created, from which the following are inherited:
Player.
Enemy, as well as a subclass Berserk, which extends the behavior of Enemy.

Encapsulation
Used getter and setter:
For example, in the Character class, the Health and ActiveWeapon properties are encapsulated via get and protected set.
In the Enemy class, the EnemyName and EnemySprite properties with get are added.

Polymorphism
Used override:
The Attack() method is overridden in Enemy and in Berserk.
Used overload:
The GetHit(int damage) method is overloaded by the GetHit(Weapon weapon) method in the Character class.

Abstraction
An abstract class Weapon is created with:
one abstract function (ApplyEffect(Character character)).
one regular function (for example, GetDamage()).
It inherits specific weapons, Sword.
