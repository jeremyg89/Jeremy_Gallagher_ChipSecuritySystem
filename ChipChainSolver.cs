using System.Collections.Generic;

namespace ChipSecuritySystem
{
    /// <summary>
    /// Finds the longest chain from Blue to Green. Doesn't reuse chips.
    /// </summary>
    public class ChipChainSolver
    {
        /// <summary>
        /// Looks for the longest Blue to Green chain.
        /// </summary>
        public IList<ColorChip> FindLongestChain(IList<ColorChip> chips)
        {
            // No chips, can't unlock.
            if (chips == null || chips.Count == 0)
            {
                return null;
            }

            List<ColorChip> optimalChain = null;
            var chain = new List<ColorChip>();
            var used = new bool[chips.Count];

            Search(chips, Color.Blue, used, chain, ref optimalChain);
            return optimalChain;
        }

        /// <summary>
        /// Gets chips that didn't get used in the chain.
        /// </summary>
        public IList<ColorChip> GetUnusedChips(IList<ColorChip> chips, IList<ColorChip> chain)
        {
            var unusedChips = new List<ColorChip>();

            if (chips == null || chips.Count == 0)
            {
                return unusedChips;
            }

            foreach (ColorChip chip in chips)
            {
                bool wasUsed = false;
                if (chain != null)
                {
                    foreach (ColorChip usedChip in chain)
                    {
                        if (object.ReferenceEquals(chip, usedChip))
                        {
                            wasUsed = true;
                            break;
                        }
                    }
                }

                if (!wasUsed)
                {
                    unusedChips.Add(chip);
                }
            }

            return unusedChips;
        }

        /// <summary>
        /// Tries unused chips from the current color and keeps the longest chain that ends on Green.
        /// </summary>
        private static void Search(
            IList<ColorChip> chips,
            Color currentColor,
            bool[] used,
            List<ColorChip> chain,
            ref List<ColorChip> optimalChain)
        {
            // Checks to see if it ends in green. Keep looking in case there's a longer one.
            if (chain.Count > 0 && currentColor == Color.Green)
            {
                if (optimalChain == null || chain.Count > optimalChain.Count)
                {
                    optimalChain = new List<ColorChip>(chain);
                }

                if (optimalChain.Count == chips.Count)
                {
                    return;
                }
            }

            // Try chips that aren't used yet and match the current color.
            for (int i = 0; i < chips.Count; i++)
            {
                if (used[i] || chips[i].StartColor != currentColor)
                {
                    continue;
                }

                used[i] = true;
                chain.Add(chips[i]);

                Search(chips, chips[i].EndColor, used, chain, ref optimalChain);

                chain.RemoveAt(chain.Count - 1);
                used[i] = false;

                if (optimalChain != null && optimalChain.Count == chips.Count)
                {
                    return;
                }
            }
        }
    }
}
