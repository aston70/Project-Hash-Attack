using System.Security.Cryptography;

namespace HashAttack_Domain
{

    /// <summary>
    /// This class orchestrates the execution of collision and preimage attacks.
    /// </summary>
    public class ExperimentRunner
    {

        const string _csvHeaderLine = "BitSize,Trial,Sample,NumberOfAttempts\n";

        // You should gather at least 50 samples at each bit size.
        const int _samplesPerSize = 50;

        // I recommend you use the following bit sizes: 8, 10, 12, 14, 16, 18, 20, 22.
        private int[] _bitSizes = { 8, 10, 12, 14, 16, 18, 20, 22 };

        private readonly SHA1_Truncator _truncator;
        private readonly RandomNumberGenerator _randomNumberGenerator;

        public ExperimentRunner()
        {
            _truncator = new SHA1_Truncator();
            _randomNumberGenerator = RandomNumberGenerator.Create();
        }

        /// <summary>
        /// Run collission attack trials for each bit size in _bitSizes.
        /// </summary>
        public void RunCollisionTrials(int trial)
        {

            Console.WriteLine($"Starting Trial {trial} for Collision Attacks...");

            string csvFilePath = $"collision_results_trial_{trial}.csv";
            var collision = new CollisionAttack(_truncator, _randomNumberGenerator);
            int totalTasks = _bitSizes.Length * _samplesPerSize;
            int progress = 0;

            // csv header
            File.WriteAllText(csvFilePath, _csvHeaderLine);

            foreach (int bits in _bitSizes)
            {

                Console.WriteLine($"=== Collision trials: {bits} bits ===");

                // for each trial size run the specified number of trials
                for (int sample = 1; sample <= _samplesPerSize; sample++)
                {
                    long iterations = collision.RunTrial(bits);
                    File.AppendAllText(csvFilePath, $"{bits},{trial},{sample},{iterations}\n");

                    // Periodically display progress.
                    progress++;
                    if (progress % 10 == 0) {
                        Console.WriteLine($"Collision Progress: {progress}/{totalTasks}");
                    }
                }
            }

            Console.WriteLine($"Completed Trial {trial} for Collision Attacks.");

        }

        /// <summary>
        /// Run preimage attack trials for each bit size in _bitSizes.
        /// </summary>
        public void RunPreimageTrials(int trial)
        {

            Console.WriteLine($"Starting Trial {trial} for Preimage Attacks...");

            string csvFilePath = $"preimage_results_trial_{trial}.csv";
            var preimage = new PreimageAttack(_truncator, _randomNumberGenerator);
            int totalTasks = _bitSizes.Length * _samplesPerSize;
            int progress = 0;

            //csv header
            File.WriteAllText(csvFilePath, _csvHeaderLine);

            foreach (int bits in _bitSizes)
            {

                Console.WriteLine($"=== Preimage trials: {bits} bits ===");

                // for each trial size run the specified number of trials
                for (int sample = 1; sample <= _samplesPerSize; sample++)
                {
                    long iterations = preimage.RunTrial(bits);
                    File.AppendAllText(csvFilePath, $"{bits},{trial},{sample},{iterations}\n");

                    // Periodically display progress.
                    progress++;
                    if (progress % 10 == 0) {
                        Console.WriteLine($"Preimage Progress: {progress}/{totalTasks}");
                    }
                }

            }

            Console.WriteLine($"Completed Trial {trial} for Preimage Attacks.");

        }


    }

}