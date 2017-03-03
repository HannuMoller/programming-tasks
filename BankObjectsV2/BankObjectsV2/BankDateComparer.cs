using System.Collections;

namespace BankObjects
{
    class BankDateComparer : IComparer
    {
        int IComparer.Compare(object f1, object f2)
        {
            int i = ((string)f2).CompareTo((string)f1);
            if (i == 0) i = 1; // must not return 0, as SortedList won't add such entry

            return i;
        }
    }
}
