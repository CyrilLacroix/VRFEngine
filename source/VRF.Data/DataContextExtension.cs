using System.Runtime.CompilerServices;

namespace VRFEngine.Data
{
    public static class DataContextExtension
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void EnsureSeedDataForContext(this DataContext context)
        {
            // Seed databse here
        }
    }
}
