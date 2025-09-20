using System.Security.Cryptography;

namespace HashAttack_Domain
{

    /// <summary>
    /// This class implements a collision attack on a truncated SHA-1 hash function.
    /// </summary>
    public class CollisionAttack : HashAttack
    {
        public CollisionAttack(SHA1_Truncator truncator, RandomNumberGenerator rng)
            : base(truncator, rng) { }

        /// <summary>
        /// Implements the base class method to run a single trial of the collision attack.
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        public override long RunTrial(int bits)
        {
            string basePrefix = RandomBasePrefix();
            var seen = new Dictionary<uint, long>();
            long attempts = 0, counter = 0;

            while (true)
            {
                string candidate = basePrefix + ":" + counter;
                attempts++;
                uint h = _truncator.ComputeSHA1TruncatedHash(candidate, bits);
                if (seen.ContainsKey(h))
                    return attempts;
                seen[h] = attempts;
                counter++;
            }
        }
    }

}