using HashAttack_Domain;
using System.Security.Cryptography;

namespace HashAttack_UnitTests
{

    [TestClass]
    public class PreimageAttackTests
    {

        [TestMethod]
        public void RunTrial_FindsPreimage_ForSmallBitSize()
        {
            var truncator = new SHA1_Truncator();
            using var randomNumberGenerator = RandomNumberGenerator.Create();

            var attack = new PreimageAttack(truncator, randomNumberGenerator);

            int bits = 2; // very small, preimage guaranteed quickly
            long attempts = attack.RunTrial(bits);

            // With 2 bits, max unique hashes = 4, so attempts should never exceed 4
            Assert.IsTrue(attempts >= 1 && attempts <= 4,
                $"Expected a preimage in 1-4 attempts, got {attempts}");
        }

    }

}