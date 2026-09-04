using System;
using System.Collections.Generic;
using System.Text;

namespace ChipSecuritySystem
{
    class Program
    {
        /// <summary>
        /// Runs a few sample bags and prints the results.
        /// </summary>
        static void Main()
        {
            // Empty bag, can't unlock.
            var exampleBagEmpty = new List<ColorChip>();

            // Readme example. Orange/Purple isn't used.
            var exampleBagReadmeExample = new List<ColorChip>()
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple),
            };

            // Longer bag with two blues.
            var exampleBagSevenChips = new List<ColorChip>()
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Orange, Color.Red),
                new ColorChip(Color.Orange, Color.Purple),
                new ColorChip(Color.Purple, Color.Blue),
                new ColorChip(Color.Yellow, Color.Orange),
                new ColorChip(Color.Blue, Color.Orange)
            };

            // Longer bag with two blues and two greens.
            var exampleBagEightChips = new List<ColorChip>()
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Orange, Color.Red),
                new ColorChip(Color.Orange, Color.Purple),
                new ColorChip(Color.Purple, Color.Blue),
                new ColorChip(Color.Yellow, Color.Orange),
                new ColorChip(Color.Blue, Color.Orange),
                new ColorChip(Color.Red, Color.Green)
            };

            SolveChain(exampleBagEmpty);
            SolveChain(exampleBagReadmeExample);
            SolveChain(exampleBagSevenChips);
            SolveChain(exampleBagEightChips);
        }

        /// <summary>
        /// Prints the chain, or the error if it can't unlock.
        /// </summary>
        static void SolveChain(List<ColorChip> exampleBag)
        {
            var chipChainSolver = new ChipChainSolver();

            IList<ColorChip> chain = chipChainSolver.FindLongestChain(exampleBag);

            if (chain == null || chain.Count == 0)
            {
                Console.WriteLine(Constants.ErrorMessage + "\n");
            }
            else
            {
                Console.WriteLine(FormatChain(chain));

                // On successful chain, print leftover chips if there are any.
                IList<ColorChip> unusedChips = chipChainSolver.GetUnusedChips(exampleBag, chain);
                Console.WriteLine(FormatUnusedChips(unusedChips));
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints the chain the same way as the readme.
        /// </summary>
        static string FormatChain(IList<ColorChip> chain)
        {
            return "The result that successfully links the blue and green markers is:\r\n\r\nBlue"
                + FormatChipList(chain)
                + " Green\n";
        }

        /// <summary>
        /// Prints leftover chips.
        /// </summary>
        static string FormatUnusedChips(IList<ColorChip> unusedChips)
        {
            if (unusedChips == null || unusedChips.Count == 0)
            {
                return "Unused chips: none";
            }

            return "Unused chips:" + FormatChipList(unusedChips);
        }

        /// <summary>
        /// Formats each chip in brackets.
        /// </summary>
        static string FormatChipList(IList<ColorChip> chips)
        {
            var builder = new StringBuilder();

            foreach (ColorChip chip in chips)
            {
                builder.Append(" [");
                builder.Append(chip);
                builder.Append("]");
            }

            return builder.ToString();
        }
    }
}
