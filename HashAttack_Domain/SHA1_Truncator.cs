using System.Text;
using System.Security.Cryptography;

namespace HashAttack_Domain
{

    /// <summary>
    /// this class provides functionality to compute truncated SHA-1 hashes.
    /// This is using SHA1 from System.Security.Cryptography.
    /// </summary>
    public class SHA1_Truncator
    {

        /// <summary>
        /// SHA-1 wrapper that takes a string and a number of bits n, and returns the SHA-1 digest truncated to n bits.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="bits"></param>
        /// <returns>32 bit unsigned integer</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public uint ComputeSHA1TruncatedHash(string message, int bits)
        {

            uint truncatedHash;

            // validate bits
            if (bits < 1 || bits > 32) {
                throw new ArgumentOutOfRangeException(nameof(bits));
            }

            // Compute the SHA-1 hash of the input message, take the first 32 bits, 
            // and truncate it down to the requested number of bits (n). 
            // This provides a reduced-size hash suitable for collision/preimage experiments.
            using var sha1 = SHA1.Create();
            {

                byte[] data = Encoding.UTF8.GetBytes(message);
                byte[] digest = sha1.ComputeHash(data);

                Console.WriteLine($"Message: {message}");
                Console.WriteLine($"Full SHA-1 (first 32 bits shown): {BitConverter.ToString(digest, 0, 4)}");

                // Extract the first 32 bits from the SHA-1 digest
                truncatedHash = 
                    ((uint)digest[0] << 24) | 
                    ((uint)digest[1] << 16) |
                    ((uint)digest[2] << 8) |
                     (uint)digest[3];

                // If bits < 32, right shift to truncate to the desired number of bits
                if (bits < 32) {
                    truncatedHash >>= (32 - bits);
                }

            }

            Console.WriteLine($"Truncated to {bits} bits: 0x{truncatedHash:X}");

            return truncatedHash;
        }

    }

}