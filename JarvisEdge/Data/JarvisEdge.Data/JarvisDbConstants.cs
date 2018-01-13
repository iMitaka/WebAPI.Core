namespace JarvisEdge.Data
{
    public static class JarvisDbConstants
    {
        private const string debugConnectionString = "Data Source =.\\SQLEXPRESS;Initial Catalog = JarvisDb; Integrated Security = True; MultipleActiveResultSets=True";

        public static string GetConnectionString()
        {
            return debugConnectionString;
        }
    }
}
