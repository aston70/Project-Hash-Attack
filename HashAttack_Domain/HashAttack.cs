using System.Security.Cryptography;

namespace HashAttack_Domain
{

    /// <summary>
    /// This class serves as a base for specific hash attack implementations.
    /// It can only be inherited.
    /// </summary>
    public abstract class HashAttack
    {
        protected readonly SHA1_Truncator _truncator;
        protected readonly RandomNumberGenerator _randNumGenerator;

        protected HashAttack(SHA1_Truncator truncator, RandomNumberGenerator randNumGenerator)
        {
            _truncator = truncator;
            _randNumGenerator = randNumGenerator;
        }

        /// <summary>
        /// Generates a random base prefix using 8 random bytes converted to a hexadecimal string.
        /// </summary>
        /// <returns></returns>
        protected string RandomBasePrefix()
        {
            byte[] b = new byte[8];

            _randNumGenerator.GetBytes(b);

            return BitConverter.ToString(b).Replace("-", "");
        }

        /// <summary>
        /// Contract for running a single trial of the attack in derived classes.
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        public abstract long RunTrial(int bits);
    }

}