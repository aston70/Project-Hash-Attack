using HashAttack_Domain;

namespace HashAttackProject
{

    class Program
    {
        static void Main(string[] args)
        {

            var runner = new ExperimentRunner();

            for(int trial = 1; trial <= 4; trial++)
            {                
                runner.RunCollisionTrials(trial);              
                runner.RunPreimageTrials(trial);                
            }

            Console.WriteLine("All experiments complete.");
        }
    }

}