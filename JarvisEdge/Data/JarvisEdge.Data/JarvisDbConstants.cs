namespace JarvisEdge.Data
{
    public static class JarvisDbConstants
    {
        private const string debugConnectionString = "Data Source =.\\SQLEXPRESS;Initial Catalog = Homes; Integrated Security = True; MultipleActiveResultSets=True";

        public static string GetConnectionString()
        {
            return debugConnectionString;
        }
    }
}
