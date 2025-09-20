using System.Security.Cryptography;

namespace HashAttack_Domain
{

    /// <summary>
    /// This class implements a preimage attack on a truncated SHA-1 hash function.
    /// </summary>
    public class PreimageAttack : HashAttack
    {
        /// <summary>
        /// Initializes with a SHA1_Truncator and a RandomNumberGenerator.
        /// </summary>
        /// <param name="truncator"></param>
        /// <param name="rng"></param>
        public PreimageAttack(SHA1_Truncator truncator, RandomNumberGenerator rng)
            : base(truncator, rng) { }

        public override long RunTrial(int bits)
        {
            string basePrefix = RandomBasePrefix();
            string m1 = basePrefix + ":target";
            uint target = _truncator.ComputeSHA1TruncatedHash(m1, bits);

            long attempts = 0, counter = 0;
            while (true)
            {
                string candidate = basePrefix + ":preimg:" + counter;
                if (candidate == m1) { counter++; continue; }

                attempts++;

                // Compute hash and check for match
                uint h = _truncator.ComputeSHA1TruncatedHash(candidate, bits);
                if (h == target) {
                    return attempts;
                }

                counter++;
            }
        }
    }

}