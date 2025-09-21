using HashAttack_Domain;
using System.Security.Cryptography;

namespace HashAttack_UnitTests
{

    [TestClass]
    public class CollisionAttackTests
    {

        [TestMethod]
        public void RunTrial_FindsCollision_ForSmallBitSize()
        {
            var truncator = new SHA1_Truncator();
            var randomNumberGenerator = RandomNumberGenerator.Create();

            var attack = new CollisionAttack(truncator, randomNumberGenerator);

            int bits = 2; // very small, collisions guaranteed quickly
            long attempts = attack.RunTrial(bits);

            // With 2 bits, max unique hashes = 4, so attempts should never exceed 5
            Assert.IsTrue(attempts >= 2 && attempts <= 5, $"Expected a collision in 2-5 attempts, got {attempts}");
        }

    }

}